// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// if this mod has new layer
    /// use this
    /// </summary>
    public interface IHasLayer
    {
        Container CreateNewLayer();
    }
}
