using osu.Game.Rulesets.Karaoke.UI.Layers.Note;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    /// <summary>
    ///     stage definition
    /// </summary>
    public class KaraokeStageDefinition
    {
        /// <summary>
        ///     The number of <see cref="KaraokeStage" />s which this stage contains.
        /// </summary>
        public int Columns;

        /// <summary>
        ///     if <see cref="LyricTimeLine" /> does not assign tone, use default
        /// </summary>
        public int DefaultIndex = 0;

        /// <summary>
        ///     Whether the column index is a special column for this stage.
        /// </summary>
        /// <param name="column">The 0-based column index.</param>
        /// <returns>Whether the column is a special column.</returns>
        public bool IsSpecialColumn(int column)
        {
            return Columns % 2 == 1 && column == Columns / 2;
        }
    }
}
