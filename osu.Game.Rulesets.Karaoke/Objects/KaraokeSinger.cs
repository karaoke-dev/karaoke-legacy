﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Game.Database;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class KaraokeSinger : IHasPrimaryKey
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
