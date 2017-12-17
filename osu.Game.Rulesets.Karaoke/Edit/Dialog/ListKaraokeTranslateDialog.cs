using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Dialog
{
    class ListKaraokeTranslateDialog : DialogContainer
    {
        protected ItemsScrollContainer ItemsScrollContainer { get; set; }

        public ListKaraokeTranslateDialog(KaraokeEditPlayfield playField)
        {

        }

        public override void InitialDialog()
        {
            //
            MainContext.Add(ItemsScrollContainer = new ItemsScrollContainer()
            {
                Width = 550,
                Height = 300,
            });

            base.InitialDialog();
        }

        public class TranslateViewCell
        {

        }

        public class TranslateEditCell
        {

        }
    }
}
