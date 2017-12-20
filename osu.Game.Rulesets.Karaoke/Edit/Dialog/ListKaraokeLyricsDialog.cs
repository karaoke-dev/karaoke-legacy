using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Database;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static osu.Game.Overlays.Music.FilterControl;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Dialog
{
    /// <summary>
    /// list karaoke lytrics
    /// click the unlock can edit it
    /// </summary>
    class ListKaraokeLyricsDialog : DialogContainer
    {
        protected KaraokeEditPlayfield PlayField;
        protected ListLyricsScrollContainer ItemsScrollContainer { get; set; }
        public FilterTextBox Search;

        public ListKaraokeLyricsDialog(KaraokeEditPlayfield playField)
        {
            PlayField = playField;

            //initial karaoke objects to set
            InitialItemsScrollContainerItems();
        }

        public override void InitialDialog()
        {
            //
            MainContext.Add(Search = new FilterTextBox
            {
                RelativeSizeAxes = Axes.X,
                Height = 40,
                //Exit = () => ExitRequested?.Invoke(),
                
            });

            //if search new word
            Search.Current.ValueChanged += (newString) =>
              {
                  ItemsScrollContainer.SearchTerm = newString;
              };

            MainContext.Add(ItemsScrollContainer = new ListLyricsScrollContainer()
            {
                Position = new OpenTK.Vector2(0, 40),
                Width = 600,
                Height = 260,
                
                Sets=new List<KaraokeObject>()
                {
                    new KaraokeObject(){ ID=0},
                    new KaraokeObject(){ ID=1},
                    new KaraokeObject(){ ID=2},
                    new KaraokeObject(){ ID=3},
                    new KaraokeObject(){ ID=4},
                    new KaraokeObject(){ ID=5},
                    new KaraokeObject(){ ID=6},
                    new KaraokeObject(){ ID=7},
                    new KaraokeObject(){ ID=8},
                    new KaraokeObject(){ ID=9},
                    new KaraokeObject(){ ID=10},
                }
                
            });
            
            base.InitialDialog();

            
        }

        void InitialItemsScrollContainerItems()
        {
            var listObjects = PlayField?.ListDrawableKaraokeObject?? new List<Objects.Drawables.IAmDrawableKaraokeObject>();
            var listKaraokeObjects = new List<KaraokeObject>();
            foreach (var single in listObjects)
                listKaraokeObjects.Add(single.KaraokeObject);

            ItemsScrollContainer.Sets = listKaraokeObjects;
        }
    }

    public class ListLyricsScrollContainer : ItemsScrollContainer<KaraokeObject, LyricsCell>
    {
        public ListLyricsScrollContainer()
        {

        }
    }

    public class LyricsCell : DrawableItems<KaraokeObject>
    {
        public OsuTextBox LyricsTextbox { get; set; }
        public TimeTextBox StartTimeTextbox { get; set; }
        public OsuSpriteText ToLabel { get; set; }
        public TimeTextBox EndTimeTextbox { get; set; }

        public FillFlowContainer<Drawable> FillFlowContainer { get; set; }

        //TODO : Get or set the value
        public override KaraokeObject BeatmapSetInfo
        {
            get => base.BeatmapSetInfo;
            set
            {
                base.BeatmapSetInfo = value;
                LyricsTextbox.Text = BeatmapSetInfo?.MainText?.Text;
                StartTimeTextbox.TimeValue = BeatmapSetInfo?.StartTime ?? 0;
                EndTimeTextbox.TimeValue = BeatmapSetInfo?.EndTime ?? 0;
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
            this.Add(FillFlowContainer = new FillFlowContainer<Drawable>()
            {
                Direction = FillDirection.Horizontal,
                Position = new OpenTK.Vector2(20, 0),
                Spacing = new OpenTK.Vector2(10, 0),
                Children = new Drawable[]
                {
                    LyricsTextbox=new FocusedTextBox()
                    {
                        Width=350,
                        Height=35,
                    },
                    StartTimeTextbox=new TimeTextBox()
                    {
                        Width=80,
                        Height=35,
                    },
                    ToLabel=new OsuSpriteText()
                    {
                        //Width=10,
                        Text=" ~ "
                    },
                    EndTimeTextbox=new TimeTextBox()
                    {
                        Width=80,
                        Height=35,
                    },
                }
            });
        }
    }
}
