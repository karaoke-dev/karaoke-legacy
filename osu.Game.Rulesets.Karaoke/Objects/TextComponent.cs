// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class TextComponent : IHasText
    {
        public TextComponent()
        {

        }

        public TextComponent(string str)
        {
            Text = str;
        }

        /// <summary>
        /// text
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// operator
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static TextComponent operator +(TextComponent object1, TextComponent object2)
        {
            return new TextComponent()
            {
                Text = object1.Text + object2.Text,
            };
        }
    }
}
