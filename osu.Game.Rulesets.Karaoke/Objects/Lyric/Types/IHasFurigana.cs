// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;

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
