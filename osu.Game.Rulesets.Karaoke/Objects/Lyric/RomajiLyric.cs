using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;

namespace osu.Game.Rulesets.Karaoke.Objects.Lyric
{
    public class RomajiLyric : BaseLyric , IHasRomaji
    {
        /// <summary>
        /// list romaji text
        /// </summary>
        // TODO : [set] cannot set here
        // TODO : [get] get the value is combine from list
        public RomajiTextList Romaji { get; set; } = new RomajiTextList();
    }
}
