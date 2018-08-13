// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Timing;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    ///     just make slower
    /// </summary>
    public class KaraokeModEasy : ModHalfTime
    {
        public override string Name => "KaraokeEasy";
        public override string ShortenedName => "EZ";
        public override double ScoreMultiplier => 1;
        public override string Description => "just make defult song speed slower.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_halftime;
        public override bool Ranked => true;
        public override Type[] IncompatibleMods => new[] { typeof(KaraokeModPractice),  typeof(ModDoubleTime)};

        public override void ApplyToClock(IAdjustableClock clock)
        {
            clock.Rate = 0.75;
        }
    }
}
