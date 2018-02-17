// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Overlays.Music;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog
{
    /// <summary>
    /// list karaoke lytrics
    /// click the unlock can edit it
    /// </summary>
    public class ListKaraokeLyricsDialog : DialogContainer
    {
        protected KaraokeEditPlayfield PlayField;
        protected ListLyricsScrollContainer ItemsScrollContainer { get; set; }
        public FilterControl.FilterTextBox Search;

        public ListKaraokeLyricsDialog(KaraokeEditPlayfield playField)
        {
            Title = "Lyrics";
            PlayField = playField;

            Width = 600;
            Height = 400;

            //initial karaoke objects to set
            initialItemsScrollContainerItems();
        }

        public override void InitialDialog()
        {
            //
            MainContext.Add(Search = new FilterControl.FilterTextBox
            {
                RelativeSizeAxes = Axes.X,
                Height = 40,
                //Exit = () => ExitRequested?.Invoke(),
            });

            //if search new word
            Search.Current.ValueChanged += (newString) => { ItemsScrollContainer.SearchTerm = newString; };

            MainContext.Add(ItemsScrollContainer = new ListLyricsScrollContainer()
            {
                Position = new Vector2(0, 40),
                Width = 600,
                Height = 300,
            });

            base.InitialDialog();
        }

        private void initialItemsScrollContainerItems()
        {
            var listObjects = PlayField?.ListDrawableKaraokeObject ?? new List<IAmDrawableKaraokeObject>();
            var listKaraokeObjects = new List<Lyric>();
            foreach (var single in listObjects)
                listKaraokeObjects.Add(single.Lyric);

            ItemsScrollContainer.Sets = listKaraokeObjects;
        }
    }

    public class ListLyricsScrollContainer : TableView<Lyric, LyricsCell>
    {
        public ListLyricsScrollContainer()
        {
        }
    }

    public class LyricsCell : KaraokeBaseTableViewCell<Lyric>
    {
        public RevertableTextbox LyricsTextbox { get; set; }
        public TimeTextBox StartTimeTextbox { get; set; }
        public OsuSpriteText ToLabel { get; set; }
        public TimeTextBox EndTimeTextbox { get; set; }

        //加上isCombo 和 singer dropdownbox

        public FillFlowContainer<Drawable> FillFlowContainer { get; set; }

        //TODO : Get or set the value
        public override Lyric BeatmapSetInfo
        {
            get => base.BeatmapSetInfo;
            set
            {
                base.BeatmapSetInfo = value;
                LyricsTextbox.OldValue = BeatmapSetInfo?.MainText?.Text;
                StartTimeTextbox.OldValue = BeatmapSetInfo?.StartTime ?? 0;
                EndTimeTextbox.OldValue = BeatmapSetInfo?.EndTime ?? 0;
            }
        }

        public LyricsCell()
        {
            Height = 40;
        }

        protected override void InitialView()
        {
            //Initial view
            base.InitialView();
            //add
            Add(FillFlowContainer = new FillFlowContainer<Drawable>()
            {
                Direction = FillDirection.Horizontal,
                Position = new Vector2(20, 0),
                Spacing = new Vector2(10, 0),
                Children = new Drawable[]
                {
                    LyricsTextbox = new RevertableTextbox()
                    {
                        Width = 350,
                        Height = 35,
                    },
                    StartTimeTextbox = new TimeTextBox()
                    {
                        Width = 80,
                        Height = 35,
                    },
                    ToLabel = new OsuSpriteText()
                    {
                        //Width=10,
                        Text = " ~ "
                    },
                    EndTimeTextbox = new TimeTextBox()
                    {
                        Width = 80,
                        Height = 35,
                    },
                }
            });
        }
    }
}
