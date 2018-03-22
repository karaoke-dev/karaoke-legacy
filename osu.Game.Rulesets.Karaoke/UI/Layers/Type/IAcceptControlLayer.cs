// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Type
{
    /// <summary>
    /// The Layer is accept Control by <see cref="IControlLayer"/>
    /// </summary>
    public interface IAcceptControlLayer : ILayer
    {
        /// <summary>
        /// Key action
        /// </summary>
        BindableObject<KeyAction> KeyAction { get; set; }

        /// <summary>
        /// Tap action
        /// </summary>
        BindableObject<TapAction> TapAction { get; set; }

        /// <summary>
        /// Scroll action
        /// </summary>
        BindableObject<ScrollAction> ScrollAction { get; set; }
    }
}
