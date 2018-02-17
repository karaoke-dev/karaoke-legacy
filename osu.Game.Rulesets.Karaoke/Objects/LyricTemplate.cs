// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Newtonsoft.Json;
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
        public TextObject SubText { get; set; } = new TextObject()
        {
            FontSize = 20, //default Main text Size is 50
            Position = new Vector2(0, 15), //default position
        };

        /// <summary>
        /// main text
        /// </summary>
        public TextObject MainText { get; set; } = new TextObject()
        {
            FontSize = 50, //default Main text Size is 50
            Position = new Vector2(0, 50), //default position
        };

        /// <summary>
        /// translate text
        /// </summary>
        public TextObject TranslateText { get; set; } = new TextObject()
        {
            FontSize = 20, //default Main text Size is 50
            Position = new Vector2(0, 65), //default position
        };

        /// <summary>
        /// translate terxt color
        /// </summary>
        public Color4 TranslateTextColor { get; set; } = Color4.White;

        public float Scale { get; set; } = 1;

        /// <summary>
        /// width
        /// </summary>
        [JsonIgnore]
        public float Width { get; set; } = 700;

        /// <summary>
        /// height
        /// </summary>
        [JsonIgnore]
        public float Height { get; set; } = 100;
    }
}
