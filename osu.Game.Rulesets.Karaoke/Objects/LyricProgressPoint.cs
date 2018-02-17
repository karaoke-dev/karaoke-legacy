// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
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

    /// <summary>
    /// list Progress point
    /// </summary>
    public class ListProgressPoint : List<LyricProgressPoint>
    {
        [JsonIgnore]
        public double MinimumTime { get; set; } = 100;

        /// <summary>
        /// will check if this progress point is valid
        /// </summary>
        /// <returns><c>true</c>, if progress point was added, <c>false</c> otherwise.</returns>
        /// <param name="karaokeObject">Karaoke object.</param>
        public bool AddProgressPoint(LyricProgressPoint point)
        {
            //TODO : filter
            if (this.Any(x => x.CharIndex == point.CharIndex))
                return false;
            if (this.Any(x => x.RelativeTime == point.RelativeTime))
                return false;

            Add(point);
            SortProgressPoint();
            FixTime();
            return true;
        }

        /// <summary>
        /// sorting by position and time should be higher
        /// </summary>
        public void SortProgressPoint()
        {
            // from small to large
            var orderByRelativeTimeList = this.OrderBy(x => x.RelativeTime).ToList();
            Clear();
            AddRange(orderByRelativeTimeList);
            //sort
            var orderByCharList = this.OrderBy(x => x.CharIndex).ToList();
            Clear();
            AddRange(orderByCharList);
        }

        /// <summary>
        /// fix the delta time
        /// </summary>
        public void FixTime()
        {
            double time = 0;
            foreach (var single in this)
            {
                if (single.RelativeTime < time + MinimumTime)
                {
                    single.RelativeTime = time + MinimumTime;
                }
                time = single.RelativeTime;
            }
        }
    }
}
