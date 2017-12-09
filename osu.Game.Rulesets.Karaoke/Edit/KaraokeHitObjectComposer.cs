using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeHitObjectComposer : HitObjectComposer
    {
        public KaraokeHitObjectComposer(Ruleset ruleset)
            : base(ruleset)
        {
        }

        protected override RulesetContainer CreateRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap) => new KaraokeEditRulesetContainer(ruleset, beatmap, true);

        protected override IReadOnlyList<ICompositionTool> CompositionTools => new ICompositionTool[]
        {
            new HitObjectCompositionTool<KaraokeObject>(),//karaoke object
            new HitObjectCompositionTool<TextObject>(),//add subtext to karaoke Object
            new HitObjectCompositionTool<KaraokeTranslateString>(),//add translate to KaraokeObject
        };
    }
    
}
