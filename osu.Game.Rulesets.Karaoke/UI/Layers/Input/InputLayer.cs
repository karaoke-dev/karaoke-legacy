using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Touch screen config
        /// </summary>
        public BindableObject<MobileScrollAnixConfig> MobileScrollAnixConfig { get; set; } = new BindableObject<MobileScrollAnixConfig>(new MobileScrollAnixConfig());



        public InputLayer(KaraokePanelOverlay karaokePanelOverlay)
        {
            
        }
    }
}
