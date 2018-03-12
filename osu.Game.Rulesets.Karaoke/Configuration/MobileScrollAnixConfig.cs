using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    /// Config
    /// </summary>
    public class MobileScrollAnixConfig
    {
        /// <summary>
        /// X Anix
        /// </summary>
        public SingleAnixConfig XAnixConfig { get; set; }

        /// <summary>
        /// Y Anix
        /// </summary>
        public SingleAnixConfig YAnixConfig { get; set; }

        /// <summary>
        /// X Anix(Two finger)
        /// </summary>
        public SingleAnixConfig TwoFingerXAnixConfig { get; set; }

        /// <summary>
        /// Y Anix(Two finger)
        /// </summary>
        public SingleAnixConfig TwoFingerYAnixConfig { get; set; }

        /// <summary>
        /// single-tap
        /// </summary>
        public TapConfig SingleTapConfig { get; set; }

        /// <summary>
        /// double-tap
        /// </summary>
        public TapConfig DoubleTapConfig { get; set; }

        /// <summary>
        /// SingleAnixConfig
        /// </summary>
        public class SingleAnixConfig
        {
            /// <summary>
            /// Anix
            /// </summary>
            public ScrollAction ScrollAction { get; set; }

            /// <summary>
            /// Sensitive
            /// </summary>
            public double Sensitive { get; set; }
        }

        /// <summary>
        /// TapConfig
        /// </summary>
        public class TapConfig
        {
            public TapAction TapAction { get; set; }
        }

        /// <summary>
        /// X or Y-anix can be use as...
        /// </summary>
        public enum ScrollAction
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
        /// action
        /// </summary>
        public enum TapAction
        {
            /// <summary>
            /// Tap to pause
            /// </summary>
            Pause,
        }
    }
}
