using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    public interface IHasObjectType
    {
        KaraokeHitObjectType Type { get; }
    }

    /// <summary>
    /// Types
    /// </summary>
    public enum KaraokeHitObjectType
    {
        Lyric,
        Commerial,
    }
}
