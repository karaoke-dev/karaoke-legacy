using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Type
{
    /// <summary>
    /// This layer can control the layer that inhit <see cref="IAcceptControlLayer"/>
    /// </summary>
    public interface IControlLayer : ILayer
    {
        /// <summary>
        /// Key action
        /// </summary>
        BindableObject<KeyAction> KeyAction { get; set; }

        /// <summary>
        /// Tap action
        /// </summary>
        BindableObject<TapAction> TapAction { get; set; }

        /// <summary>
        /// Scroll action
        /// </summary>
        BindableObject<ScrollAction> ScrollAction { get; set; }
    }
}
