// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// use to record romaji
    /// </summary>
    public class RomajiText : TextObject, IHasCharIndex, IHasCharEndIndex
    {
        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int CharIndex { get; set; }

        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int? CharEndIndex { get; set; }
    }
}
