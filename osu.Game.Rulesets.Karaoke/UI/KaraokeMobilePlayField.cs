using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    /// Add Mobile PlayField support
    /// </summary>
    public class KaraokeMobilePlayField : KaraokePlayfield
    {
        public KaraokeMobilePlayField(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container)
            : base(ruleset, beatmap, container)
        {
        }
    }
}
