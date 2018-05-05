using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics.Containers;
using osu.Game.Overlays.Settings;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections.Pieces
{
    /// <summary>
    /// create multi selection 
    /// Filter cannot select same
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultiSettingsEnumDropdown<T> : FillFlowContainer<SettingsEnumDropdown<T>>
    {
        public MultiSettingsEnumDropdown()
        {

        }
    }
}
