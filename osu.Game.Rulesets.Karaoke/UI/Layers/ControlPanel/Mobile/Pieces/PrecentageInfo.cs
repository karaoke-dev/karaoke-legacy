using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile.Pieces
{
    /// <summary>
    /// use to show precentage
    /// </summary>
    public class PrecentageInfo : Info
    {
        /// <summary>
        /// value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// min value
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// max value
        /// </summary>
        public double MaxValue { get; set; }
    }
}
