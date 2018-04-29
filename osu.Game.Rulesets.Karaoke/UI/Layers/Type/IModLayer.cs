// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Type
{
    public interface IModLayer : ILayer
    {
        Bindable<IEnumerable<Mod>> Mods { get; set; }
    }
}
