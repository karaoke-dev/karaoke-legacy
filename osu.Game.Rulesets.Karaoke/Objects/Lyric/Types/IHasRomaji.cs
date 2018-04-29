using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Lyric.Types
{
    /// <summary>
    /// most non-english Lyric , might need romaji for foreign people
    /// </summary>
    public interface IHasRomaji
    {
        /// <summary>
        /// Romaji
        /// </summary>
        RomajiTextList Romaji { get;}
    }
}
