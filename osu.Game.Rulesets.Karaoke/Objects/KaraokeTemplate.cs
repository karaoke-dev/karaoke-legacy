using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// define the position of karaoke
    /// </summary>
    public class KaraokeTemplate
    {
        /// <summary>
        /// sub text
        /// </summary>
        public TextObject SubText { get; set; } = new TextObject()
        {
            FontSize = 20, //default Main text Size is 50
            Position = new Vector2(0, 20), //default position
        };

        /// <summary>
        /// main text
        /// </summary>
        public TextObject MainText { get; set; } = new TextObject()
        {
            FontSize = 50, //default Main text Size is 50
            Position = new Vector2(0, 30), //default position
        };

        /// <summary>
        /// translate text
        /// </summary>
        public TextObject TranslateText { get; set; } = new TextObject()
        {
            FontSize = 20, //default Main text Size is 50
            Position = new Vector2(0, 75), //default position
        };

        /// <summary>
        /// translate terxt color
        /// </summary>
        public Color4 TranslateTextColor { get; set; } = Color4.White;

        /// <summary>
        /// width
        /// </summary>
        public float Width { get; set; } = 700;

        /// <summary>
        /// height
        /// </summary>
        public float Height { get; set; } = 100;
    }
}
