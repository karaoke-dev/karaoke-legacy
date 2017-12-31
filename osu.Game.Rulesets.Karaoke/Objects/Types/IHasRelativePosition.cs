// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// relative position
    /// </summary>
    public interface IHasRelativePosition
    {
        /// <summary>
        /// relative position can be null
        /// </summary>
        Vector2? RelativePosition { get; set; }
    }
}
