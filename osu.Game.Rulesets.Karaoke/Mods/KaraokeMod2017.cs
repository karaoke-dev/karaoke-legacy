using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// Event mod in 2017
    /// </summary>
    public class ChristmasMod : SnowMod //, IApplicableMod<KaraokeObject>
    {
        public override string Name => "Happy Christmas!";

        public override string ShortenedName => "HPCris2017";

        public override string Description => "Singing karaoke at home and nobody give you a shit.";

        public override double ScoreMultiplier => 1;

    }
}
