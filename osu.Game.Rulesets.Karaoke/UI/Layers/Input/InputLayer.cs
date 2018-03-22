using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.UI.Layer.ControlPanel.Desktop;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layer.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Input
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
