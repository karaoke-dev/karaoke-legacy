// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    ///     Config
    /// </summary>
    public class MobileScrollAnixConfig : ICloneable, IEquatable<MobileScrollAnixConfig>
    {
        /// <summary>
        ///     Tap Action
        /// </summary>
        public Dictionary<TouchScreenTapInteractive, TapConfig> TagConfigs { get; set; } = new Dictionary<TouchScreenTapInteractive, TapConfig>();


        /// <summary>
        ///     Scroll Action
        /// </summary>
        public Dictionary<TouchScreenScrollInteractive, SingleAnixConfig> ScrollConfigs { get; set; } = new Dictionary<TouchScreenScrollInteractive, SingleAnixConfig>();

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(MobileScrollAnixConfig other)
        {
            return Equals(TagConfigs, other.TagConfigs) && Equals(ScrollConfigs, other.ScrollConfigs);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MobileScrollAnixConfig)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((TagConfigs != null ? TagConfigs.GetHashCode() : 0) * 397) ^ (ScrollConfigs != null ? ScrollConfigs.GetHashCode() : 0);
            }
        }
    }

    /// <summary>
    ///     SingleAnixConfig
    /// </summary>
    public class SingleAnixConfig : ICopyable
    {
        /// <summary>
        ///     Anix
        /// </summary>
        public KaraokeScrollAction KaraokeScrollAction { get; set; }

        /// <summary>
        ///     Sensitive
        /// </summary>
        public double Sensitive { get; set; }

        /// <summary>
        ///     Copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Copy<T>() where T : class, ICopyable, new()
        {
            var result = new T();
            if (result is SingleAnixConfig singleAnixConfig)
            {
                singleAnixConfig.KaraokeScrollAction = KaraokeScrollAction;
                singleAnixConfig.Sensitive = Sensitive;
            }

            return result;
        }
    }

    /// <summary>
    ///     TapConfig
    /// </summary>
    public class TapConfig : ICopyable
    {
        public KaraokeTapAction KaraokeTapAction { get; set; }

        /// <summary>
        ///     Copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Copy<T>() where T : class, ICopyable, new()
        {
            var result = new T();
            if (result is TapConfig tapConfig)
                tapConfig.KaraokeTapAction = KaraokeTapAction;

            return result;
        }
    }

    /// <summary>
    ///     Tap action
    /// </summary>
    public enum TouchScreenTapInteractive
    {
        SingleTap,
        DoubleTap,
        Hold
    }

    /// <summary>
    ///     Scroll action
    /// </summary>
    public enum TouchScreenScrollInteractive
    {
        XAnix,
        YAnix,
        TwoFingerXAnix,
        TwoFingerYAnix
    }
}
