using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Mobile.Pieces
{
    /// <summary>
    /// use to show value 
    /// Format : (+/-) [Value] (unit)
    /// </summary>
    public class ValueInfo : Info
    {
        /// <summary>
        /// Format
        /// </summary>
        public Format Format { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public string Unit { get; set; }
    }

    /// <summary>
    /// Format
    /// </summary>
    public enum Format
    {
        Number,
        Time,//HH:MM:SS
    }
}
