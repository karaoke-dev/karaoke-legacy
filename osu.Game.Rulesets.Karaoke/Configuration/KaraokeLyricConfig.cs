// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using Newtonsoft.Json;
using osu.Game.Rulesets.Karaoke.Configuration.Types;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    ///     karaoke lyric config
    /// </summary>
    public class KaraokeLyricConfig : ICloneable, IEquatable<KaraokeLyricConfig>, IJsonString
    {
        /// <summary>
        ///     show subText
        /// </summary>
        public bool SubTextVislbility { get; set; } = true;

        /// <summary>
        ///     show romaji
        /// </summary>
        public bool RomajiVislbility { get; set; } = true;

        /// <summary>
        ///     romaji first
        /// </summary>
        public bool RomajiFirst { get; set; }

        /// <summary>
        ///     Show translate
        /// </summary>
        public bool ShowTranslate { get; set; } = true;

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(KaraokeLyricConfig other)
        {
            return SubTextVislbility == other.SubTextVislbility
                   && RomajiVislbility == other.RomajiVislbility
                   && RomajiFirst == other.RomajiFirst
                   && ShowTranslate == other.ShowTranslate;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KaraokeLyricConfig)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = SubTextVislbility.GetHashCode();
                hashCode = (hashCode * 397) ^ RomajiVislbility.GetHashCode();
                hashCode = (hashCode * 397) ^ RomajiFirst.GetHashCode();
                hashCode = (hashCode * 397) ^ ShowTranslate.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
