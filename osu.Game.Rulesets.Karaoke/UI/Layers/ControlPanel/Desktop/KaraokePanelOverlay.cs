// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Overlays;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Type;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop
{
    /// <summary>
    /// to show the Karaoke panel on Playfield 
    /// </summary>
    public partial class KaraokePanelOverlay : WaveOverlayContainer, IControlPanel
    {
        public BindableObject<KeyAction> KeyAction { get; set; } = new BindableObject<KeyAction>(null);
        public BindableObject<TapAction> TapAction { get; set; } = new BindableObject<TapAction>(null);
        public BindableObject<ScrollAction> ScrollAction { get; set; } = new BindableObject<ScrollAction>(null);

        private readonly IAmKaraokeField _playField;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playField"></param>
        public KaraokePanelOverlay(IAmKaraokeField playField = null)
        {
            _playField = playField;

            InitialPanel();

            //key changed
            KeyAction.ValueChanged += OnKeyAction;
        }
    }
}
