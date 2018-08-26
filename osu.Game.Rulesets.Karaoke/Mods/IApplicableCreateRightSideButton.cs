using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.UI.Layers.Input;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    public interface IApplicableCreateRightSideButton : IApplicableMod
    {
        Button CreateButton(InputLayer inputLayer);
    }
}
