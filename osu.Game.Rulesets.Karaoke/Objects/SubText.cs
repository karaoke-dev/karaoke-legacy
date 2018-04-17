// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// sub text
    /// </summary>
    public class SubText : TextComponent, IHasEndIndex
    {
        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int? Length { get; set; }
    }
}
