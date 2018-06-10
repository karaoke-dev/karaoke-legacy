using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Note
{
    public class KaraokeToneEditPlayfield : KaraokeTonePlayfield
    {
        public KaraokeToneEditPlayfield(List<KaraokeStageDefinition> stageDefinitions)
            : base(stageDefinitions)
        {
        }
    }
}
