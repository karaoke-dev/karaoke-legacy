// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Type;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile
{
    /// <summary>
    /// this is the panel designed for mobile
    /// it is only just show time and time slider 
    /// </summary>
    public partial class KaraokeLightPanel : Container, IControlPanel
    {
        private IAmKaraokeField _playField;

        public BindableObject<KeyAction> KeyAction { get; set; } = new BindableObject<KeyAction>(null);
        public BindableObject<TapAction> TapAction { get; set; } = new BindableObject<TapAction>(null);
        public BindableObject<ScrollAction> ScrollAction { get; set; } = new BindableObject<ScrollAction>(null);

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playField"></param>
        public KaraokeLightPanel(IAmKaraokeField playField)
        {
            _playField = playField;

            KeyAction.ValueChanged += PrepareKeyInfoPanel;
            TapAction.ValueChanged += PrepareTapInfoPanel;
            ScrollAction.ValueChanged += PrepareScrollInfoPanel;
        }

        /// <summary>
        /// Change the visivility
        /// </summary>
        public void ToggleVisibility()
        {
            //throw new System.NotImplementedException();
        }

       
    }
}
