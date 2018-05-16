using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    public class KaraokeStageDefinition
    {
        /// <summary>
        /// The number of <see cref="Column"/>s which this stage contains.
        /// </summary>
        public int Columns;

        /// <summary>
        /// Whether the column index is a special column for this stage.
        /// </summary>
        /// <param name="column">The 0-based column index.</param>
        /// <returns>Whether the column is a special column.</returns>
        public bool IsSpecialColumn(int column) => Columns % 2 == 1 && column == Columns / 2;
    }
}
