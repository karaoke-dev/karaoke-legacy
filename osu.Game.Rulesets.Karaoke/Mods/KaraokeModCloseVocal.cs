// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    ///     if sound trach has two parts, close the vocal part
    /// </summary>
    public class KaraokeModCloseVocal : Mod
    {
        public override string Name => "CloseVocal";
        public override string ShortenedName => "CloseVocal";
        public override double ScoreMultiplier => 1;
        public override string Description => "if sound trach has two parts, close the vocal part.";
        public override FontAwesome Icon => FontAwesome.fa_times;
        public override bool Ranked => true;
    }
}
