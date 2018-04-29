// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// Has char end index.
    /// </summary>
    public interface IHasEndIndex
    {
        /// <summary>
        /// length
        /// compare of end index , char length is more comvenent to record
        /// </summary>
        /// <value>The length of the char.</value>
        int? Length { get; set; }
    }
}
