﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using osu.Framework.Threading;
using osu.Game.Online.API;
using osu.Game.Online.API.Requests;
using osu.Game.Users;

namespace osu.Game.Online.Chat
{
    /// <summary>
    /// Manages everything channel related
    /// </summary>
    public class ChannelManager : Component, IOnlineComponent
    {
        /// <summary>
        /// The channels the player joins on startup
        /// </summary>
        private readonly string[] defaultChannels =
        {
            @"#lazer",
            @"#osu",
            @"#lobby"
        };

        /// <summary>
        /// The currently opened channel
        /// </summary>
        public Bindable<Channel> CurrentChannel { get; } = new Bindable<Channel>();

        /// <summary>
        /// The Channels the player has joined
        /// </summary>
        public ObservableCollection<Channel> JoinedChannels { get; } = new ObservableCollection<Channel>(); //todo: should be publicly readonly

        /// <summary>
        /// The channels available for the player to join
        /// </summary>
        public ObservableCollection<Channel> AvailableChannels { get; } = new ObservableCollection<Channel>(); //todo: should be publicly readonly

        private IAPIProvider api;
        private ScheduledDelegate fetchMessagesScheduleder;

        public ChannelManager()
        {
            CurrentChannel.ValueChanged += currentChannelChanged;
        }

        /// <summary>
        /// Opens a channel or switches to the channel if already opened.
        /// </summary>
        /// <exception cref="ChannelNotFoundException">If the name of the specifed channel was not found this exception will be thrown.</exception>
        /// <param name="name"></param>
        public void OpenChannel(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            CurrentChannel.Value = AvailableChannels.FirstOrDefault(c => c.Name == name) ?? throw new ChannelNotFoundException(name);
        }

        /// <summary>
        /// Opens a new private channel.
        /// </summary>
        /// <param name="user">The user the private channel is opened with.</param>
        public void OpenPrivateChannel(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            CurrentChannel.Value = JoinedChannels.FirstOrDefault(c => c.Type == ChannelType.PM && c.Users.Count == 1 && c.Users.Any(u => u.Id == user.Id))
                                   ?? new Channel { Name = user.Username, Users = { user }, Type = ChannelType.PM };
        }

        private void currentChannelChanged(Channel channel) => JoinChannel(channel);

        /// <summary>
        /// Ensure we run post actions in sequence, once at a time.
        /// </summary>
        private readonly Queue<Action> postQueue = new Queue<Action>();

        /// <summary>
        /// Posts a message to the currently opened channel.
        /// </summary>
        /// <param name="text">The message text that is going to be posted</param>
        /// <param name="isAction">Is true if the message is an action, e.g.: user is currently eating </param>
        public void PostMessage(string text, bool isAction = false)
        {
            if (CurrentChannel.Value == null)
                return;

            var currentChannel = CurrentChannel.Value;

            void dequeueAndRun()
            {
                if (postQueue.Count > 0)
                    postQueue.Dequeue().Invoke();
            }

            postQueue.Enqueue(() =>
            {
                if (!api.IsLoggedIn)
                {
                    currentChannel.AddNewMessages(new ErrorMessage("Please sign in to participate in chat!"));
                    return;
                }

                var message = new LocalEchoMessage
                {
                    Sender = api.LocalUser.Value,
                    Timestamp = DateTimeOffset.Now,
                    ChannelId = CurrentChannel.Value.Id,
                    IsAction = isAction,
                    Content = text
                };

                currentChannel.AddLocalEcho(message);

                // if this is a PM and the first message, we need to do a special request to create the PM channel
                if (currentChannel.Type == ChannelType.PM && !currentChannel.Joined)
                {
                    var createNewPrivateMessageRequest = new CreateNewPrivateMessageRequest(currentChannel.Users.First(), message);

                    createNewPrivateMessageRequest.Success += createRes =>
                    {
                        currentChannel.Id = createRes.ChannelID;
                        currentChannel.ReplaceMessage(message, createRes.Message);
                        dequeueAndRun();
                    };

                    createNewPrivateMessageRequest.Failure += exception =>
                    {
                        Logger.Error(exception, "Posting message failed.");
                        currentChannel.ReplaceMessage(message, null);
                        dequeueAndRun();
                    };

                    api.Queue(createNewPrivateMessageRequest);
                    return;
                }

                var req = new PostMessageRequest(message);

                req.Success += m =>
                {
                    currentChannel.ReplaceMessage(message, m);
                    dequeueAndRun();
                };

                req.Failure += exception =>
                {
                    Logger.Error(exception, "Posting message failed.");
                    currentChannel.ReplaceMessage(message, null);
                    dequeueAndRun();
                };

                api.Queue(req);
            });

            // always run if the queue is empty
            if (postQueue.Count == 1)
                dequeueAndRun();
        }

        /// <summary>
        /// Posts a command locally. Commands like /help will result in a help message written in the current channel.
        /// </summary>
        /// <param name="text">the text containing the command identifier and command parameters.</param>
        public void PostCommand(string text)
        {
            if (CurrentChannel.Value == null)
                return;

            var parameters = text.Split(new[] { ' ' }, 2);
            string command = parameters[0];
            string content = parameters.Length == 2 ? parameters[1] : string.Empty;

            switch (command)
            {
                case "me":
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        CurrentChannel.Value.AddNewMessages(new ErrorMessage("Usage: /me [action]"));
                        break;
                    }

                    PostMessage(content, true);
                    break;

                case "help":
                    CurrentChannel.Value.AddNewMessages(new InfoMessage("Supported commands: /help, /me [action]"));
                    break;

                default:
                    CurrentChannel.Value.AddNewMessages(new ErrorMessage($@"""/{command}"" is not supported! For a list of supported commands see /help"));
                    break;
            }
        }

        private void handleChannelMessages(IEnumerable<Message> messages)
        {
            var channels = JoinedChannels.ToList();

            foreach (var group in messages.GroupBy(m => m.ChannelId))
                channels.Find(c => c.Id == group.Key)?.AddNewMessages(group.ToArray());
        }

        private void initializeChannels()
        {
            var req = new ListChannelsRequest();

            var joinDefaults = JoinedChannels.Count == 0;

            req.Success += channels =>
            {
                foreach (var channel in channels)
                {
                    var ch = getChannel(channel, addToAvailable: true);

                    // join any channels classified as "defaults"
                    if (joinDefaults && defaultChannels.Any(c => c.Equals(channel.Name, StringComparison.OrdinalIgnoreCase)))
                        JoinChannel(ch);
                }
            };
            req.Failure += error =>
            {
                Logger.Error(error, "Fetching channel list failed");
                initializeChannels();
            };

            api.Queue(req);
        }

        /// <summary>
        /// Fetches inital messages of a channel
        ///
        /// TODO: remove this when the API supports returning initial fetch messages for more than one channel by specifying the last message id per channel instead of one last message id globally.
        /// right now it caps out at 50 messages and therefore only returns one channel's worth of content.
        /// </summary>
        /// <param name="channel">The channel </param>
        private void fetchInitalMessages(Channel channel)
        {
            if (channel.Id <= 0) return;

            var fetchInitialMsgReq = new GetMessagesRequest(channel);
            fetchInitialMsgReq.Success += messages =>
            {
                handleChannelMessages(messages);
                channel.MessagesLoaded = true; // this will mark the channel as having received messages even if there were none.
            };

            api.Queue(fetchInitialMsgReq);
        }

        /// <summary>
        /// Find an existing channel instance for the provided channel. Lookup is performed basd on ID.
        /// The provided channel may be used if an existing instance is not found.
        /// </summary>
        /// <param name="lookup">A candidate channel to be used for lookup or permanently on lookup failure.</param>
        /// <param name="addToAvailable">Whether the channel should be added to <see cref="AvailableChannels"/> if not already.</param>
        /// <param name="addToJoined">Whether the channel should be added to <see cref="JoinedChannels"/> if not already.</param>
        /// <returns>The found channel.</returns>
        private Channel getChannel(Channel lookup, bool addToAvailable = false, bool addToJoined = false)
        {
            Channel found = null;

            bool lookupCondition(Channel ch) => lookup.Id > 0 ? ch.Id == lookup.Id : lookup.Name == ch.Name;

            var available = AvailableChannels.FirstOrDefault(lookupCondition);
            if (available != null)
                found = available;

            var joined = JoinedChannels.FirstOrDefault(lookupCondition);
            if (found == null && joined != null)
                found = joined;

            if (found == null)
            {
                found = lookup;

                // if we're using a channel object from the server, we want to remove ourselves from the users list.
                // this is because we check the first user in the channel to display a name/icon on tabs for now.
                var foundSelf = found.Users.FirstOrDefault(u => u.Id == api.LocalUser.Value.Id);
                if (foundSelf != null)
                    found.Users.Remove(foundSelf);
            }

            if (joined == null && addToJoined) JoinedChannels.Add(found);
            if (available == null && addToAvailable) AvailableChannels.Add(found);

            return found;
        }

        /// <summary>
        /// Joins a channel if it has not already been joined.
        /// </summary>
        /// <param name="channel">The channel to join.</param>
        /// <param name="alreadyJoined">Whether the channel has already been joined server-side. Will skip a join request.</param>
        /// <returns>The joined channel. Note that this may not match the parameter channel as it is a backed object.</returns>
        public Channel JoinChannel(Channel channel, bool alreadyJoined = false)
        {
            if (channel == null) return null;

            channel = getChannel(channel, addToJoined: true);

            // ensure we are joined to the channel
            if (!channel.Joined.Value)
            {
                if (alreadyJoined)
                    channel.Joined.Value = true;
                else
                {
                    switch (channel.Type)
                    {
                        case ChannelType.Public:
                            var req = new JoinChannelRequest(channel, api.LocalUser);
                            req.Success += () => JoinChannel(channel, true);
                            req.Failure += ex => LeaveChannel(channel);
                            api.Queue(req);
                            return channel;
                    }
                }
            }

            if (CurrentChannel.Value == null)
                CurrentChannel.Value = channel;

            if (!channel.MessagesLoaded)
            {
                // let's fetch a small number of messages to bring us up-to-date with the backlog.
                fetchInitalMessages(channel);
            }

            return channel;
        }

        public void LeaveChannel(Channel channel)
        {
            if (channel == null) return;

            if (channel == CurrentChannel.Value) CurrentChannel.Value = null;

            JoinedChannels.Remove(channel);

            if (channel.Joined.Value)
            {
                api.Queue(new LeaveChannelRequest(channel, api.LocalUser));
                channel.Joined.Value = false;
            }
        }

        public void APIStateChanged(APIAccess api, APIState state)
        {
            switch (state)
            {
                case APIState.Online:
                    fetchUpdates();
                    break;
                default:
                    fetchMessagesScheduleder?.Cancel();
                    fetchMessagesScheduleder = null;
                    break;
            }
        }

        private long lastMessageId;
        private const int update_poll_interval = 1000;

        private bool channelsInitialised;

        private void fetchUpdates()
        {
            fetchMessagesScheduleder?.Cancel();
            fetchMessagesScheduleder = Scheduler.AddDelayed(() =>
            {
                var fetchReq = new GetUpdatesRequest(lastMessageId);

                fetchReq.Success += updates =>
                {
                    if (updates?.Presence != null)
                    {
                        foreach (var channel in updates.Presence)
                        {
                            // we received this from the server so should mark the channel already joined.
                            JoinChannel(channel, true);
                        }

                        //todo: handle left channels

                        handleChannelMessages(updates.Messages);

                        foreach (var group in updates.Messages.GroupBy(m => m.ChannelId))
                            JoinedChannels.FirstOrDefault(c => c.Id == group.Key)?.AddNewMessages(group.ToArray());

                        lastMessageId = updates.Messages.LastOrDefault()?.Id ?? lastMessageId;
                    }

                    if (!channelsInitialised)
                    {
                        channelsInitialised = true;
                        // we want this to run after the first presence so we can see if the user is in any channels already.
                        initializeChannels();
                    }

                    fetchUpdates();
                };

                fetchReq.Failure += delegate { fetchUpdates(); };

                api.Queue(fetchReq);
            }, update_poll_interval);
        }

        [BackgroundDependencyLoader]
        private void load(IAPIProvider api)
        {
            this.api = api;
            api.Register(this);
        }
    }

    /// <summary>
    /// An exception thrown when a channel could not been found.
    /// </summary>
    public class ChannelNotFoundException : Exception
    {
        public ChannelNotFoundException(string channelName)
            : base($"A channel with the name {channelName} could not be found.")
        {
        }
    }
}
