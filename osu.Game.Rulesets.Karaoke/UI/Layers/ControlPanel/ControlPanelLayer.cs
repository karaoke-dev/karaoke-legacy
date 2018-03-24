// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Configuration;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel
{
    public partial class ControlPanelLayer : IAcceptControlLayer, IPlatformLayer
    {
        public BindableObject<KeyAction> KeyAction { get; set; }
        public BindableObject<TapAction> TapAction { get; set; }
        public BindableObject<ScrollAction> ScrollAction { get; set; }

        public Bindable<PlatformType> PlatformType { get; set; } = new Bindable<PlatformType>();

        public ControlPanelLayer()
        {
            PlatformType.ValueChanged += OnPlatformChanged;
        }
    }
}
