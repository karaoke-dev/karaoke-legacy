using osu.Game.Rulesets.Karaoke.Objects.Note;
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
        /// Set default tone
        /// </summary>
        public Tone DefaultTone { get; set; }
    }
}
