using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    /// X or Y-anix can be use as...
    /// </summary>
    public enum MobileScrollAnix
    {
        /// <summary>
        /// Time
        /// </summary>
        Time,

        /// <summary>
        /// Volumn
        /// </summary>
        Volumn,

        /// <summary>
        /// Dim
        /// </summary>
        BackgroundDim,

        /// <summary>
        /// Tone
        /// </summary>
        Tome,

        /// <summary>
        /// Speed
        /// </summary>
        Speed,
    }

    /// <summary>
    /// Config
    /// </summary>
    public class MobileScrollAnixConfig
    {
        /// <summary>
        /// Anix
        /// </summary>
        public MobileScrollAnix MobileScrollAnix { get; set; }

        /// <summary>
        /// Sensitive
        /// </summary>
        public double Sensitive { get; set; }
    }
}
