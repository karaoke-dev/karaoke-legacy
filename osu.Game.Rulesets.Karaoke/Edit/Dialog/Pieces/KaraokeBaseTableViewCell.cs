// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Database;

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
    public class KaraokeBaseTableViewCell<TItem> : TableViewCell<TItem> where TItem : IHasPrimaryKey
    {
        public KaraokeBaseTableViewCell()
        {
        }

        //call to revert value
        public virtual void Revert()
        {
        }

        public virtual void Save()
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
