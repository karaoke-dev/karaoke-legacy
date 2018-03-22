// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.UI.Layer.ControlPanel.Desktop;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layer.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Input
{
    /// <summary>
    /// this container is use for detect HotKey pressing
    /// </summary>
    public partial class InputLayer : Container, IKeyBindingHandler<KaraokeKeyAction>, IControlLayer
    {
        public BindableObject<KeyAction> KeyAction { get; set; } = new BindableObject<KeyAction>(null);

        /// <summary>
        /// On Press
        /// </summary>
        /// <returns><c>true</c>, if pressed was oned, <c>false</c> otherwise.</returns>
        /// <param name="action">Action.</param>
        public bool OnPressed(KaraokeKeyAction action)
        {
            KeyAction keyAction = new KeyAction()
            {
                KaraokeKeyAction = action,
                Press = true,
            };
            KeyAction.Value = keyAction;

            return true;
        }

        /// <summary>
        /// OnRelease
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool OnReleased(KaraokeKeyAction action)
        {
            KeyAction keyAction = new KeyAction()
            {
                KaraokeKeyAction = action,
                Press = false,
            };
            KeyAction.Value = keyAction;

            return true;
        }
    }
}
