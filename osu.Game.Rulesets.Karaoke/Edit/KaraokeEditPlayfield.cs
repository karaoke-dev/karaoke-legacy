using osu.Game.Rulesets.Karaoke.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditPlayfield : KaraokePlayfield
    {
        public KaraokeEditPlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container) : base(ruleset, beatmap, container)
        {
        }
    }
}
