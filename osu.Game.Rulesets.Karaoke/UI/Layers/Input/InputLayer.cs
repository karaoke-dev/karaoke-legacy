// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input
{
    /// <summary>
    /// Input layer
    /// </summary>
    public partial class InputLayer : Container, IKeyBindingHandler<KaraokeKeyAction>, IControlLayer
    {
        public BindableObject<BaseAction> InputAction { get; set; } = new BindableObject<BaseAction>(null);

        /// <summary>
        /// Ctor
        /// </summary>
        public InputLayer()
        {
            initialUi();
            InitialTouchScreen();
        }

        [BackgroundDependencyLoader(true)]
        private void load(KaraokeConfigManager manager)
        {
            var scrollConfig = manager.GetObjectBindable<MobileScrollAnixConfig>(KaraokeSetting.TouchScreen);
            MobileScrollAnixConfig.BindTo(scrollConfig);
        }
    }
}
