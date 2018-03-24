// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Overlays;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop
{
    /// <summary>
    /// to show the Karaoke panel on Playfield 
    /// </summary>
    public partial class KaraokePanelOverlay : WaveOverlayContainer, IControlLayer
    {
        /// <summary>
        /// Key action
        /// </summary>
        public BindableObject<KeyAction> KeyAction { get; set; } = new BindableObject<KeyAction>(null);

        /// <summary>
        /// Tap action
        /// </summary>
        public BindableObject<TapAction> TapAction { get; set; } = new BindableObject<TapAction>(null);

        /// <summary>
        /// Scroll action
        /// </summary>
        public BindableObject<ScrollAction> ScrollAction { get; set; } = new BindableObject<ScrollAction>(null);

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playField"></param>
        public KaraokePanelOverlay(IAmKaraokeField playField = null)
        {
            PlayField = playField;

            InitialPanel();

            //key changed
            KeyAction.ValueChanged += OnKeyAction;
        }
    }
}
