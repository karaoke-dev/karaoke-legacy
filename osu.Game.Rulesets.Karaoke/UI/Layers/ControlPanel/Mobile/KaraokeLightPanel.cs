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
    /// this is the panel designed for mobile
    /// it is only just show time and time slider 
    /// </summary>
    public class KaraokeLightPanel : Container, IControlPanel
    {
        private IAmKaraokeField _playField;

        public KaraokeLightPanel(IAmKaraokeField playField)
        {
            _playField = playField;
        }

        public BindableObject<KeyAction> KeyAction { get; set; } = new BindableObject<KeyAction>(null);
        public BindableObject<TapAction> TapAction { get; set; } = new BindableObject<TapAction>(null);
        public BindableObject<ScrollAction> ScrollAction { get; set; } = new BindableObject<ScrollAction>(null);


        public void ToggleVisibility()
        {
            //throw new System.NotImplementedException();
        }
    }
}
