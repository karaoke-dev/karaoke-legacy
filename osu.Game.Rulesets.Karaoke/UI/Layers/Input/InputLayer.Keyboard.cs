// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input
{
    /// <summary>
    ///     this container is use for detect HotKey pressing
    /// </summary>
    public partial class InputLayer
    {
        /// <summary>
        ///     On Press
        /// </summary>
        /// <returns><c>true</c>, if pressed was oned, <c>false</c> otherwise.</returns>
        /// <param name="action">Action.</param>
        public bool OnPressed(KaraokeKeyAction action)
        {
            var keyAction = new KeyAction
            {
                KaraokeKeyAction = action,
                Press = true
            };
            InputAction.Value = keyAction;

            return true;
        }

        /// <summary>
        ///     OnRelease
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool OnReleased(KaraokeKeyAction action)
        {
            var keyAction = new KeyAction
            {
                KaraokeKeyAction = action,
                Press = false
            };
            InputAction.Value = keyAction;

            return true;
        }
    }
}
