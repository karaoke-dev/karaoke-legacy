using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
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
        protected ItemsScrollContainer ItemsScrollContainer { get; set; }
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

            MainContext.Add(ItemsScrollContainer = new ItemsScrollContainer()
            {
                Position=new OpenTK.Vector2(0,40),
                Width=550,
                Height=300,
            });
            
            base.InitialDialog();
        }

        public class LyricsViewCell
        {

        }

        public class LyricsEditCell
        {

        }
    }
}
