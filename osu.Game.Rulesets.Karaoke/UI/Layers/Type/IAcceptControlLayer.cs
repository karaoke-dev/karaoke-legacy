// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Type
{
    /// <summary>
    ///     The Layer is accept Control by <see cref="IControlLayer" />
    /// </summary>
    public interface IAcceptControlLayer : ILayer
    {
        /// <summary>
        ///     Key action
        /// </summary>
        BindableObject<BaseAction> InputAction { get; set; }
    }
}
