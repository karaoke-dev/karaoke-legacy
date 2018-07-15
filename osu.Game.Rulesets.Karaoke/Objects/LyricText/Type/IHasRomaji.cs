using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.LyricText.Type
{
    public interface IHasRomaji
    {
        string Romaji { get; set; }

        bool CombineWithNext { get; set; }
    }
}
