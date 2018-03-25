// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input
{
    /// <summary>
    /// Input layer
    /// </summary>
    public partial class InputLayer
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public InputLayer()
        {
            initialUi();
        }

        [BackgroundDependencyLoader(true)]
        private void load(KaraokeConfigManager manager)
        {
            var scrollConfig = manager.GetObjectBindable<MobileScrollAnixConfig>(KaraokeSetting.TouchScreen);
            MobileScrollAnixConfig.BindTo(scrollConfig);
        }
    }
}
