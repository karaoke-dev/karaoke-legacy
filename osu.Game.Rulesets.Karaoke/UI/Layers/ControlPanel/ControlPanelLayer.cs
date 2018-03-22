using osu.Framework.Configuration;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action;
using osu.Game.Rulesets.Karaoke.UI.Layer.Type;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel
{
    public partial class ControlPanelLayer : IAcceptControlLayer , IPlatformLayer
    {
        public BindableObject<KeyAction> KeyAction { get; set; }
        public BindableObject<TapAction> TapAction { get; set; }
        public BindableObject<ScrollAction> ScrollAction { get; set; }

        public Bindable<PlatformType> PlatformType { get; set; } = new Bindable<PlatformType>();

        public ControlPanelLayer()
        {
            PlatformType.ValueChanged += OnPlatformChanged;
        }
    }
}
