using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Dialog
{
    class ListKaraokeTranslateDialog : DialogContainer
    {
        protected KaraokeEditPlayfield PlayField;
        protected ListTranslateScrollContainer ItemsScrollContainer { get; set; }
        protected EnumDropdown<TranslateCode> Dropdown { get; set; }

        public ListKaraokeTranslateDialog(KaraokeEditPlayfield playField)
        {
            PlayField = playField;

            //initial karaoke objects to set
            InitialItemsScrollContainerItems();
        }

        public override void InitialDialog()
        {

            MainContext.Add(Dropdown = new EnumDropdown<TranslateCode>()
            {
                Position = new OpenTK.Vector2(20, 10),
                Width = 200,
                Scale=new OpenTK.Vector2(0.6f),
                Depth=-10,
                //RelativeSizeAxes = Axes.Y,
                //MaximumSize=new OpenTK.Vector2(200,200)
                
            });
            Dropdown.DropdownContainer.MaxHeight = 250;
            Dropdown.Current.ValueChanged += (translateCode) =>
              {
                  //TODO : value changed
                  ItemsScrollContainer.SetCurrentLanguage(translateCode);
              };

            //
            MainContext.Add(ItemsScrollContainer = new ListTranslateScrollContainer()
            {
                Position = new OpenTK.Vector2(0, 40),
                Width = 600,
                Height = 260,
            });

            base.InitialDialog();
        }

        void InitialItemsScrollContainerItems()
        {
            var listObjects = PlayField?.ListDrawableKaraokeObject ?? new List<Objects.Drawables.IAmDrawableKaraokeObject>();
            var listKaraokeObjects = new List<KaraokeObject>();
            foreach (var single in listObjects)
                listKaraokeObjects.Add(single.KaraokeObject);

            ItemsScrollContainer.Sets = listKaraokeObjects;
        }
    }

    public class ListTranslateScrollContainer : ItemsScrollContainer<KaraokeObject, TranslateCell>
    {
        public ListTranslateScrollContainer()
        {

        }

        public void SetCurrentLanguage(TranslateCode code)
        {
            //1. change lang to string

            //2. change show translage type
        }
    }

    public class TranslateCell : DrawableItems<KaraokeObject>
    {
        public OsuTextBox LyricsTextbox { get; set; }//Lyric
        public OsuTextBox TranslateTextbox { get; set; }//Translate

        public FillFlowContainer<Drawable> FillFlowContainer { get; set; }

        public override KaraokeObject BeatmapSetInfo
        {
            get => base.BeatmapSetInfo;
            set
            {
                base.BeatmapSetInfo = value;
                LyricsTextbox.Text = BeatmapSetInfo?.MainText?.Text;
            }
        }

        public TranslateCell() 
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
                        Width=310,
                        Height=35,
                    },
                    TranslateTextbox=new TimeTextBox()
                    {
                        Width=250,
                        Height=35,
                    },
                }
            });
        }
    }
}
