using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Type
{
    /// <summary>
    /// App control panel should inherit this
    /// </summary>
    public interface IControlPanel : IAcceptControlLayer
    {
        /// <summary>
        /// Change show / hide of the panel
        /// </summary>
        void ToggleVisibility();
    }
}
