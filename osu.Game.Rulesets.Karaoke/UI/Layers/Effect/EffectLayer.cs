// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Mods.Types;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Effect
{
    /// <summary>
    ///     Effect layer
    /// </summary>
    public class EffectLayer : Container, IModLayer
    {
        public Bindable<IEnumerable<Mod>> Mods { get; set; } = new Bindable<IEnumerable<Mod>>(new Mod[] { });

        public EffectLayer()
        {
            Mods.ValueChanged += newMods =>
            {
                //clean all mod layer
                Clear();

                //create all layer if contains in mod
                foreach (var singleMod in Mods.Value)
                    if (singleMod is IHasLayer iHasLayer)
                        Add(iHasLayer.CreateNewLayer());
            };
        }
    }
}
