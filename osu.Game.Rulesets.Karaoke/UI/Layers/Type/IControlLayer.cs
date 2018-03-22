using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Type
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
