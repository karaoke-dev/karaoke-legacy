using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Dialog
{
    class ListKaraokeTranslateDialog : DialogContainer
    {
        protected ListTranslateScrollContainer ItemsScrollContainer { get; set; }

        public ListKaraokeTranslateDialog(KaraokeEditPlayfield playField)
        {

        }

        public override void InitialDialog()
        {
            //
            MainContext.Add(ItemsScrollContainer = new ListTranslateScrollContainer()
            {
                Width = 550,
                Height = 300,
            });

            base.InitialDialog();
        }


    }

    public class ListTranslateScrollContainer : ItemsScrollContainer<KaraokeObject, TranslateCell>
    {
        public ListTranslateScrollContainer()
        {

        }
    }

    public class TranslateCell : DrawableItems<KaraokeObject>
    {
        public TranslateCell(KaraokeObject beatmapSetInfo) : base(beatmapSetInfo)
        {

        }

        public TranslateCell() : base(null)
        {

        }
    }
}
