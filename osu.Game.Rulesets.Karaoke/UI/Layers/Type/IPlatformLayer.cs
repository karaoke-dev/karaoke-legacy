// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Type
{
    public interface IPlatformLayer : ILayer
    {
        /// <summary>
        /// Platform
        /// </summary>
        PlatformType PlatformType { get; set; }
    }
}
