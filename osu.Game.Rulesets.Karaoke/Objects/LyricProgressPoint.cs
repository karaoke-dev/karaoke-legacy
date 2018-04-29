// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// record what time the 
    /// </summary>
    public class LyricProgressPoint
    {
        public LyricProgressPoint()
        {
        }

        public LyricProgressPoint(double time)
        {
            RelativeTime = time;
        }

        /// <summary>
        /// relative to word's strt time
        /// </summary>
        public double RelativeTime { get; set; }
    }
}
