using osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Dialog
{
    /// <summary>
    /// show list singer
    /// </summary>
    public class ListSingerDialog : DialogContainer
    {

    }

    public class ListSingerScrollContainer : TableView<KaraokeSinger, SingerCell>
    {
        public ListSingerScrollContainer()
        {

        }
    }

    public class SingerCell : KaraokeBaseTableViewCell<KaraokeSinger>
    {
        public SingerCell()
        {
        }
    }
}
