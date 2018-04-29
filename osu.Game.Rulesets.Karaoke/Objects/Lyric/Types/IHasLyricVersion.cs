using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Lyric.Types
{
    public interface IHasLyricVersion
    {
        /// <summary>
        /// Get the version , for json decoder to select the class
        /// </summary>
        int Ver { get; }
    }
}
