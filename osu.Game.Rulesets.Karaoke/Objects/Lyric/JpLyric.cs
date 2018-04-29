using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects.Lyric
{
    public class JpLyric : RomajiLyric , IHasFurigana
    {
        /// <summary>
        /// Furigana
        /// </summary>
        public Dictionary<int, FuriganaText> Furigana { get; set; } = new Dictionary<int, FuriganaText>();
    }

    /// <summary>
    /// sub text
    /// </summary>
    public class FuriganaText : TextComponent, IHasEndIndex
    {
        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int? Length { get; set; }
    }
}
