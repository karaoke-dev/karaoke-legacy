using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
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
    }
}
