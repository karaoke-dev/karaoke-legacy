// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Game.Rulesets.Objects.Types;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class TextObject
    {
        // <inheritdoc />
        /// <summary>
        /// if template !=null will relative to template's position
        /// else, will be absolute position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// X position
        /// </summary>
        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Y);
        }

        /// <inheritdoc />
        /// <summary>
        /// Y position
        /// </summary>
        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(X, value);
        }

        /// <summary>
        /// text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// size of the font
        /// </summary>
        public virtual int? FontSize { get; set; }



        public static TextObject operator +(TextObject object1, TextObject object2)
        {
            if (object1 == null && object2 == null)
                return null;

            if (object1 == null)
                return object2;

            if (object2 == null)
                return object1;

            return new TextObject()
            {
                Position = object1.Position + object2.Position,
                Text= object1.Text + object2.Text,
                FontSize= object2?.FontSize?? object1.FontSize,
            };
        }

        
    }
}
