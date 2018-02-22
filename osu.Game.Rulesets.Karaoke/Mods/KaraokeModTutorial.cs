// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE


using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// if click this mod
    /// will introduce how to use karaoke panel and other setting
    /// will ignore the songs you select
    /// maybe
    /// </summary>
    public class KaraokeModTutorial : ModNoFail
    {
        public override string Name => "Tutorial";
        public override string ShortenedName => "Tu";
        public override double ScoreMultiplier => 1;
        public override string Description => "Will introduce how to use karaoke panel and other setting.";
        public override bool Ranked => true;
    }
}
