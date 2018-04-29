// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Input;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Karaoke.Replays
{
    public class KaraokeReplayInputHandler : FramedReplayInputHandler<KaraokeReplayFrame>
    {
        public KaraokeReplayInputHandler(Replay replay)
            : base(replay)
        {
        }

        public override List<InputState> GetPendingStates()
        {
            List<KaraokeKeyAction> actions = new List<KaraokeKeyAction>();

            //if (CurrentFrame?.MouseLeft ?? false) actions.Add(KaraokeAction.LeftButton);
            //if (CurrentFrame?.MouseRight ?? false) actions.Add(KaraokeAction.RightButton);

            return new List<InputState>
            {
                new ReplayState<KaraokeKeyAction>
                {
                    //Mouse = new ReplayMouseState(ToScreenSpace(Position ?? Vector2.Zero)),
                    //PressedActions = actions
                }
            };
        }
    }
}
