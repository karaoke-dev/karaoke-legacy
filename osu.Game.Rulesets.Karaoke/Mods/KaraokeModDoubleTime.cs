// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Timing;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// just make faster
    /// </summary>
    public class KaraokeModDoubleTime : ModDoubleTime
    {
        public override string Name => "KaraokeHard";
        public override string ShortenedName => "HD";
        public override double ScoreMultiplier => 1;
        public override string Description => "just make defult song speed faster.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_doubletime;
        public override bool Ranked => true;

        public override void ApplyToClock(IAdjustableClock clock)
        {
            clock.Rate = 1.25;
        }
    }
}
