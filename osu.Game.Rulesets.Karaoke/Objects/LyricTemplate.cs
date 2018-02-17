﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// define the position of karaoke
    /// </summary>
    public class LyricTemplate
    {
        /// <summary>
        /// sub text
        /// </summary>
        public FormattedText TopText { get; set; } = new FormattedText()
        {
            FontSize = 20, //default Main text Size is 20
            Position = new Vector2(0, 15), //default position
        };

        /// <summary>
        /// main text
        /// </summary>
        public FormattedText MainText { get; set; } = new FormattedText()
        {
            FontSize = 50, //default Main text Size is 50
            Position = new Vector2(0, 50), //default position
        };

        /// <summary>
        /// main text
        /// </summary>
        public FormattedText BottomText { get; set; } = new FormattedText()
        {
            FontSize = 20, //default Main text Size is 20
            Position = new Vector2(0, 50), //default position
        };

        /// <summary>
        /// translate text
        /// </summary>
        public FormattedText TranslateText { get; set; } = new FormattedText()
        {
            FontSize = 20, //default Main text Size is 50
            Position = new Vector2(0, 65), //default position
        };

        /// <summary>
        /// translate terxt color
        /// </summary>
        public Color4 TranslateTextColor { get; set; } = Color4.White;

        /// <summary>
        /// Scale
        /// </summary>
        public float Scale { get; set; } = 1;
    }
}
