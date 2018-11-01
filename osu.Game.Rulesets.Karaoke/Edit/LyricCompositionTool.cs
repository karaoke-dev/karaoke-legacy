using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Karaoke.Edit.Masks.LyricMasks;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class LyricCompositionTool : HitObjectCompositionTool
    {
        public LyricCompositionTool()
            : base(nameof(BaseLyric))
        {
        }

        public override PlacementMask CreatePlacementMask() => new LyricPlacementMask();
    }
}
