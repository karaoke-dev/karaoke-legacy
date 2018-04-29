// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Mods.Mod2017
{
    /// <summary>
    /// Event mod in 2017
    /// </summary>
    public class KaraokeMod2017_Christmas : KaraokeModSnow
    {
        public override string Name => "Happy Christmas!";

        public override string ShortenedName => "HPCris2017";

        public override string Description => "Singing karaoke at home and nobody give you a shit.";

        public override double ScoreMultiplier => 1;
    }
}
