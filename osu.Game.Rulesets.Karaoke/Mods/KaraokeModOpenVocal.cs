// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    ///     if sound trach has two parts, open the vocal part
    /// </summary>
    public class KaraokeModOpenVocal : Mod
    {
        public override string Name => "OpenVocal";
        public override string ShortenedName => "OpenVocal";
        public override double ScoreMultiplier => 1;
        public override string Description => "if sound trach has two parts, open the vocal part.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_relax;
        public override bool Ranked => true;
    }
}
