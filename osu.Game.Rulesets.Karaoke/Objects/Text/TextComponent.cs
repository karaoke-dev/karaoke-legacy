// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects.Text
{
    /// <summary>
    ///     TextComponent
    /// </summary>
    public class TextComponent : IHasText, ICloneable, IEquatable<TextComponent>
    {
        /// <summary>
        ///     text
        /// </summary>
        public virtual string Text { get; set; }

        public TextComponent()
        {
        }

        public TextComponent(string str)
        {
            Text = str;
        }

        /// <summary>
        ///     operator
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static TextComponent operator +(TextComponent object1, TextComponent object2)
        {
            return new TextComponent
            {
                Text = object1.Text + object2.Text
            };
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(TextComponent other)
        {
            return string.Equals(Text, other.Text);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TextComponent)obj);
        }

        public override int GetHashCode()
        {
            return Text != null ? Text.GetHashCode() : 0;
        }
    }
}
