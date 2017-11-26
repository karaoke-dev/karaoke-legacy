using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class KaraokeSinger
    {
        /// <summary>
        /// the color of lytic color
        /// </summary>
        public Color4 LytricColor { get; set; }

        /// <summary>
        /// the color of lytic color
        /// </summary>
        public Color4 LytricBackgroundColor { get; set; } = Color4.White;

        /// <summary>
        /// singer's name
        /// </summary>
        public string SingerName { get; set; }
    }
}
