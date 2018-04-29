using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;

namespace osu.Game.Rulesets.Karaoke.Objects.Lyric
{
    public class JpLyric : RomajiLyric , IHasFurigana
    {
        /// <summary>
        /// Furigana
        /// </summary>
        public Dictionary<int, SubText> Furigana { get; set; } = new Dictionary<int, SubText>();
    }
}
