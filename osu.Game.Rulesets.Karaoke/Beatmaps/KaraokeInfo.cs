// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    /// <summary>
    ///     Karaoke info
    /// </summary>
    public class KaraokeInfo
    {
        /// <summary>
        ///     override the origin Template
        /// </summary>
        public LyricTemplate LyricTemplate { get; set; } = new LyricTemplate();
    }
}
