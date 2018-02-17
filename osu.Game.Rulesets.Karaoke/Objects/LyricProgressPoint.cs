﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// record what time the 
    /// </summary>
    public class LyricProgressPoint : IHasCharIndex
    {
        public LyricProgressPoint()
        {
        }

        public LyricProgressPoint(double time, int charIndex)
        {
            RelativeTime = time;
            CharIndex = charIndex;
        }

        /// <summary>
        /// relative to word's strt time
        /// </summary>
        public double RelativeTime { get; set; }

        /// <summary>
        /// position at that time
        /// </summary>
        public int CharIndex { get; set; }
    }
}
