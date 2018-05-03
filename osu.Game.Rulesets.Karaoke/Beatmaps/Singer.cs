// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Database;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    public class Singer : IHasPrimaryKey
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

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
