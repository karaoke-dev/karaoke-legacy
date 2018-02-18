using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using osu.Game.Graphics;
using osu.Game.Rulesets.Karaoke.UI.Layer.ShowEffect;
using osu.Game.Rulesets.Mods;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// just make slower
    /// </summary>
    public class KaraokeModEasy : ModHalfTime
    {
        public override string Name => "KaraokeEasy";
        public override string ShortenedName => "EZ";
        public override double ScoreMultiplier => 1;
        public override string Description => "just make defult song speed slower.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_halftime;
        public override bool Ranked => true;

        public override void ApplyToClock(IAdjustableClock clock)
        {
            clock.Rate = 0.75;
        }
    }
}
