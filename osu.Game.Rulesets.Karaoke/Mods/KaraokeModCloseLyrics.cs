// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// not even shows any lyrics
    /// </summary>
    public class KaraokeModCloseLyrics : Mod
    {
        public override string Name => "CloseLyrics";
        public override string ShortenedName => "Cl";
        public override double ScoreMultiplier => 1;
        public override string Description => "not even shows any lyrics.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_suddendeath;
        public override bool Ranked => true;
    }
}
