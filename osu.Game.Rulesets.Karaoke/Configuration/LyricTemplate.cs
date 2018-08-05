// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using Newtonsoft.Json;
using osu.Game.Rulesets.Karaoke.Configuration.Types;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    ///     define the position of karaoke
    /// </summary>
    public class LyricTemplate : ICloneable, IEquatable<LyricTemplate> ,IJsonString
    {
        /// <summary>
        ///     top text
        /// </summary>
        public FormattedText TopText { get; set; } = new FormattedText
        {
            FontSize = 20, //default Main text Size is 20
            Position = new Vector2(0, 15) //default position
        };

        /// <summary>
        ///     main text
        /// </summary>
        public FormattedText MainText { get; set; } = new FormattedText
        {
            FontSize = 50, //default Main text Size is 50
            Position = new Vector2(0, 50) //default position
        };

        /// <summary>
        ///     bottom text
        /// </summary>
        public FormattedText BottomText { get; set; } = new FormattedText
        {
            FontSize = 20, //default Main text Size is 20
            Position = new Vector2(0, 70) //default position
        };

        /// <summary>
        ///     translate text
        /// </summary>
        public FormattedText TranslateText { get; set; } = new FormattedText
        {
            FontSize = 20, //default Main text Size is 50
            Position = new Vector2(0, 85) //default position
        };

        /// <summary>
        ///     translate text color
        /// </summary>
        public Color4 TranslateTextColor { get; set; } = Color4.White;

        /// <summary>
        ///     Scale
        /// </summary>
        public float Scale { get; set; } = 1;

        public object Clone()
        {
            return new LyricTemplate()
            {
                TopText = TopText.Clone() as FormattedText,
                MainText = TopText.Clone() as FormattedText,
                BottomText = TopText.Clone() as FormattedText,
                TranslateText = TopText.Clone() as FormattedText,
                TranslateTextColor = TranslateTextColor
            };

        }

        public bool Equals(LyricTemplate other)
        {
            return Equals(TopText, other.TopText)
                && Equals(MainText, other.MainText)
                && Equals(BottomText, other.BottomText)
                && Equals(TranslateText, other.TranslateText)
                && TranslateTextColor.Equals(other.TranslateTextColor)
                && Scale.Equals(other.Scale);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LyricTemplate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (TopText != null ? TopText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (MainText != null ? MainText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BottomText != null ? BottomText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TranslateText != null ? TranslateText.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ TranslateTextColor.GetHashCode();
                hashCode = (hashCode * 397) ^ Scale.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
