using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Karaoke.Edit.Blueprints.Lyrics;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class LyricCompositionTool : HitObjectCompositionTool
    {
        public LyricCompositionTool()
            : base(nameof(Lyric))
        {
        }

        public override PlacementBlueprint CreatePlacementBlueprint() => new LyricPlacementBlueprint();
    }
}
