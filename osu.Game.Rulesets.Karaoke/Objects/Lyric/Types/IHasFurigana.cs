using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Lyric.Types
{
    /// <summary>
    /// In japanese lyric it is common has Furigana
    /// </summary>
    public interface IHasFurigana
    {
        /// <summary>
        /// Furigana
        /// </summary>
        Dictionary<int, FuriganaText> Furigana { get; }
    }
}
