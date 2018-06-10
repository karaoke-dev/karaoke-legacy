// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Type;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile
{
    /// <summary>
    ///     this is the panel designed for mobile
    ///     it is only just show time and time slider
    /// </summary>
    public partial class KaraokeLightPanel : Container, IControlPanel
    {
        public BindableObject<BaseAction> InputAction { get; set; } = new BindableObject<BaseAction>(null);

        private IAmKaraokeField _playField;

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="playField"></param>
        public KaraokeLightPanel(IAmKaraokeField playField)
        {
            _playField = playField;

            InputAction.ValueChanged += PrepareKeyInfoPanel;
        }

        /// <summary>
        ///     Change the visivility
        /// </summary>
        public void ToggleVisibility()
        {
            //throw new System.NotImplementedException();
        }
    }
}
