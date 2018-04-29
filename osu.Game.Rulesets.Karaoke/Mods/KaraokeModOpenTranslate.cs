// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// will force open the translate for lyrics
    /// even you are not open it in the config
    /// </summary>
    public class KaraokeModOpenTranslate : Mod
    {
        public override string Name => "Translate";
        public override string ShortenedName => "Tr";
        public override double ScoreMultiplier => 1;
        public override string Description => "Will force open the translate for lyrics, even you are not open it in the config.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_nofail;
        public override bool Ranked => true;
    }
}
