// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel
{
    public partial class ControlPanelLayer : Container, IAcceptControlLayer, IPlatformLayer
    {
        public BindableObject<BaseAction> InputAction { get; set; } = new BindableObject<BaseAction>(null);
        public Bindable<PlatformType> PlatformType { get; set; } = new Bindable<PlatformType>();

        private readonly IAmKaraokeField _playField;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="playField"></param>
        public ControlPanelLayer(IAmKaraokeField playField = null)
        {
            _playField = playField;
            PlatformType.ValueChanged += OnPlatformChanged;
        }
    }
}
