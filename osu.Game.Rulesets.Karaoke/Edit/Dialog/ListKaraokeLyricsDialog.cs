using osu.Framework.Graphics;
using osu.Game.Database;
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
        protected ListLyricsScrollContainer ItemsScrollContainer { get; set; }
        public FilterTextBox Search;

        public ListKaraokeLyricsDialog(KaraokeEditPlayfield playField)
        {

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
                Position=new OpenTK.Vector2(0,40),
                Width=550,
                Height=300,
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
    }

    public class ListLyricsScrollContainer : ItemsScrollContainer<KaraokeObject, TranslateCell>
    {
        public ListLyricsScrollContainer()
        {

        }
    }

    public class LyricsCell : DrawableItems<KaraokeObject>
    {
        public LyricsCell() 
        {

        }
    }
}
