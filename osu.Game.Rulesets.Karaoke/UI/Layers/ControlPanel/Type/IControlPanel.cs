// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Type
{
    /// <summary>
    /// App control panel should inherit this
    /// </summary>
    public interface IControlPanel : IAcceptControlLayer
    {
        /// <summary>
        /// Change show / hide of the panel
        /// </summary>
        void ToggleVisibility();
    }
}
