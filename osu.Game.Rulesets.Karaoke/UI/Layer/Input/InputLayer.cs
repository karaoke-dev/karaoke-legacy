using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layer.ControlPanel.Desktop;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Keyboard;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.TouchScreen;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Input
{
    /// <summary>
    /// Input layer
    /// </summary>
    public class InputLayer
    {
        /// <summary>
        /// Keyboard input layer
        /// </summary>
        private KaraokeHotkeyPanel _karaokeHotkeyPanel { get; set; }

        /// <summary>
        /// Keyboard input layer
        /// </summary>
        private TouchScreenInput _touchScreenInput { get; set; }

        
        public InputLayer()
        {

        }
    }
}
