using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Input
{
    public partial class InputLayer
    {
        public BindableObject<TapAction> TapAction { get; set; } = new BindableObject<TapAction>(null);
        public BindableObject<ScrollAction> ScrollAction { get; set; } = new BindableObject<ScrollAction>(null);

        // Touch screen config
        public BindableObject<MobileScrollAnixConfig> MobileScrollAnixConfig { get; set; } = new BindableObject<MobileScrollAnixConfig>(new MobileScrollAnixConfig());

    }
}
