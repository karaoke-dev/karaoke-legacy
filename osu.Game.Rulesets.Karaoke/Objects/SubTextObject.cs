// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// sub text
    /// </summary>
    public class SubTextObject : TextObject, IHasCharIndex , IHasCharEndIndex
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
