using osu.Game.Rulesets.Karaoke.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// will implement...
    /// 1. background box,to display hover , now pointed karaoke time and editing
    /// 2. 
    ///
    ///
    /// click : 
    /// 1. single click is navigation to time();
    /// 2. double click is edit
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class KaraokeBaseTableViewCell<TItem> : TableViewCell<TItem> where TItem : KaraokeObject
    {
        public KaraokeBaseTableViewCell()
        {

        }

        public CellTyle CellTyle { get; set; }
    }

    public enum CellTyle
    {
        View,
        New,
        Edit,
    }
}
