// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// will hide the lyrics
    /// </summary>
    public class KaraokeModHidden : ModHidden
    {
        public override string Name => "Hidden";
        public override string ShortenedName => "HD";
        public override double ScoreMultiplier => 1;
        public override string Description => "Hidden the lyric at the start time.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_hidden;
        public override bool Ranked => true;
    }
}
