using osu.Framework.Allocation;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop;

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
        /// <param name="karaokePanelOverlay"></param>
        public InputLayer(KaraokePanelOverlay karaokePanelOverlay)
        {
            
        }


        [BackgroundDependencyLoader(true)]
        private void load(KaraokeConfigManager manager)
        {
            var scrollConfig = manager.GetObjectBindable<MobileScrollAnixConfig>(KaraokeSetting.TouchScreen);
            MobileScrollAnixConfig.BindTo(scrollConfig);
        }
    }
}
