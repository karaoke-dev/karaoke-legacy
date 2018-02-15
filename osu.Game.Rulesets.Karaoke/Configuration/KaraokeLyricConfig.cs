using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// karaoke lyric config
    /// </summary>
    public class KaraokeLyricConfig
    {
        /// <summary>
        /// show subText
        /// </summary>
        public bool SubTextVislbility { get; set; }

        /// <summary>
        /// show romaji
        /// </summary>
        public bool RomajiVislbility { get; set; }

        /// <summary>
        /// romaji first
        /// </summary>
        public bool RomajiFirst { get; set; }
    }
}
