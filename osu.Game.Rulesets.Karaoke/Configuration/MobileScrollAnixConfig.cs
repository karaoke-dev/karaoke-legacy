// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    /// Config
    /// </summary>
    public class MobileScrollAnixConfig : RecordChangeObject, ICopyable
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
        public class SingleAnixConfig : ICopyable
        {
            /// <summary>
            /// Anix
            /// </summary>
            public KaraokeScrollAction KaraokeScrollAction { get; set; }

            /// <summary>
            /// Sensitive
            /// </summary>
            public double Sensitive { get; set; }

            /// <summary>
            /// Copy
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public T Copy<T>() where T : class, ICopyable, new()
            {
                T result = new T();
                if (result is SingleAnixConfig singleAnixConfig)
                {
                    singleAnixConfig.KaraokeScrollAction = KaraokeScrollAction;
                    singleAnixConfig.Sensitive = Sensitive;
                }
                return result;
            }
        }

        /// <summary>
        /// TapConfig
        /// </summary>
        public class TapConfig : ICopyable
        {
            public KaraokeTapAction KaraokeTapAction { get; set; }

            /// <summary>
            /// Copy
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public T Copy<T>() where T : class, ICopyable, new()
            {
                T result = new T();
                if (result is TapConfig tapConfig)
                {
                    tapConfig.KaraokeTapAction = KaraokeTapAction;
                }
                return result;
            }
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Copy<T>() where T : class, ICopyable, new()
        {
            T result = new T();
            if (result is MobileScrollAnixConfig mobileScrollAnixConfig)
            {
                mobileScrollAnixConfig.XAnixConfig = XAnixConfig.Copy<SingleAnixConfig>();
                mobileScrollAnixConfig.YAnixConfig = YAnixConfig.Copy<SingleAnixConfig>();
                mobileScrollAnixConfig.TwoFingerXAnixConfig = TwoFingerXAnixConfig.Copy<SingleAnixConfig>();
                mobileScrollAnixConfig.TwoFingerYAnixConfig = TwoFingerYAnixConfig.Copy<SingleAnixConfig>();
                mobileScrollAnixConfig.SingleTapConfig = SingleTapConfig.Copy<TapConfig>();
                mobileScrollAnixConfig.DoubleTapConfig = DoubleTapConfig.Copy<TapConfig>();
                mobileScrollAnixConfig.HoldConfig = HoldConfig.Copy<TapConfig>();
                mobileScrollAnixConfig.Initialize();
            }
            return result;
        }
    }
}
