// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    /// Karaoke note config.
    /// </summary>
    public class KaraokeNoteConfig
    {
        public KaraokeNoteConfig()
        {
        }

        /// <summary>
        /// show subText
        /// </summary>
        public bool SubTextVislbility { get; set; }

        /// <summary>
        /// show romaji
        /// will replace mainText
        /// </summary>
        public bool RomajiVislbility { get; set; }
    }
}
