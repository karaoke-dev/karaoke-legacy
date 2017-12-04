using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditRulesetContainer : KaraokeRulesetContainer
    {
        public KaraokeEditRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset) : base(ruleset, beatmap, isForCurrentRuleset)
        {
        }

        protected override Playfield CreatePlayfield() => new KaraokeEditPlayfield(Ruleset, WorkingBeatmap, this);
    }
}
