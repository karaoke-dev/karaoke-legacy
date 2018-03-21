// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Input;

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
        /// double-tap
        /// </summary>
        public TapConfig HoldConfig { get; set; }

        /// <summary>
        /// SingleAnixConfig
        /// </summary>
        public class SingleAnixConfig
        {
            /// <summary>
            /// Anix
            /// </summary>
            public KaraokeScrollAction KaraokeScrollAction { get; set; }

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
            public KaraokeTapAction KaraokeTapAction { get; set; }
        }
    }
}
