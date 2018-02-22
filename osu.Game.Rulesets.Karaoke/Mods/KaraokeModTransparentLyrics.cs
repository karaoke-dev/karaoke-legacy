// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// make lytric Transparent.
    /// </summary>
    public class KaraokeModTransparentLyrics : Mod
    {
        public override string Name => "Transparent";
        public override string ShortenedName => "Transparent";
        public override double ScoreMultiplier => 1;
        public override string Description => "make lytric Transparent.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_flashlight;
        public override bool Ranked => true;
    }
}
