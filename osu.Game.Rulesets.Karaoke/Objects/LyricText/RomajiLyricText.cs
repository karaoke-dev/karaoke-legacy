using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Objects.LyricText.Type;

namespace osu.Game.Rulesets.Karaoke.Objects.LyricText
{
    public class RomajiLyricText : LyricText, IHasRomaji
    {
        public string Romaji { get; set; }

        public bool CombineWithNext { get; set; }
    }
}
