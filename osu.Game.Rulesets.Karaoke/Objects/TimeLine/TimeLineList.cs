// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;

namespace osu.Game.Rulesets.Karaoke.Objects.TimeLine
{
    /// <summary>
    ///     list Progress point
    ///     [Note : key is mapped in Char index, not Dictionary's index]
    /// </summary>
    public class TimeLineList : LyricDictionary<TimeLineIndex, TimeLine>
    {
        [JsonIgnore]
        public double MinimumTime { get; set; } = 100;

        /// <summary>
        ///     get first Progress by char index
        /// </summary>
        /// <param name="relativeTime"></param>
        /// <returns></returns>
<<<<<<< HEAD:osu.Game.Rulesets.Karaoke/Objects/TimeLine/LyricTimeLineList.cs
        public KeyValuePair<int, LyricTimeLine>? GetFirstProgressPointByTime(double relativeTime)
=======
        public KeyValuePair<TimeLineIndex, TimeLine> GetFirstProgressPointByTime(double nowRelativeTime)
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db:osu.Game.Rulesets.Karaoke/Objects/TimeLine/TimeLineList.cs
        {
            double totalDuration = 0;

<<<<<<< HEAD:osu.Game.Rulesets.Karaoke/Objects/TimeLine/LyricTimeLineList.cs
            foreach (var value in this)
            {
                totalDuration = totalDuration + value.Value.Duration;

                if (totalDuration > relativeTime)
                    return this.FindPrevioud(value.Key);
            }

            return null;
=======
            var result = this.Where(x => x.Value.RelativeTime <= nowRelativeTime).ToDictionary(x => x.Key, x => x.Value);

            if (!result.Any())
                return new KeyValuePair<TimeLineIndex, TimeLine>(new TimeLineIndex(-1,0), new TimeLine(0));

            var maxResult = Find(result.Keys.Max());

            if(maxResult!=null)
                return maxResult.Value;

            return new KeyValuePair<TimeLineIndex, TimeLine>(new TimeLineIndex(-1, 0), new TimeLine(0));
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db:osu.Game.Rulesets.Karaoke/Objects/TimeLine/TimeLineList.cs
        }

        /// <summary>
        ///     get first Progress by char index
        /// </summary>
        /// <param name="relativeTime"></param>
        /// <returns></returns>
<<<<<<< HEAD:osu.Game.Rulesets.Karaoke/Objects/TimeLine/LyricTimeLineList.cs
        public double GetFirstProgressDuration(int untilKey)
=======
        public KeyValuePair<TimeLineIndex, TimeLine>? GetLastProgressPointByTime(double nowRelativeTime)
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db:osu.Game.Rulesets.Karaoke/Objects/TimeLine/TimeLineList.cs
        {
            double totalDuration = 0;

            foreach (var value in this)
            {
                totalDuration = totalDuration + value.Value.Duration;

                if (value.Key == untilKey)
                    return totalDuration;
            }

            return 0;
        }

<<<<<<< HEAD:osu.Game.Rulesets.Karaoke/Objects/TimeLine/LyricTimeLineList.cs
        /// <summary>
        ///     get last Progress by char index
        /// </summary>
        /// <param name="relativeTime"></param>
        /// <returns></returns>
        public KeyValuePair<int, LyricTimeLine>? GetLastProgressPointByTime(double relativeTime)
        {
            double totalDuration = 0;

            foreach (var value in this)
=======
            var maxResult = Find(result.Keys.Min());

            if (maxResult.Equals(default(KeyValuePair<TimeLineIndex, TimeLine>)))
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db:osu.Game.Rulesets.Karaoke/Objects/TimeLine/TimeLineList.cs
            {
                totalDuration = totalDuration + value.Value.Duration;

                if (totalDuration > relativeTime)
                    return value;
            }

            return null;
        }


        /// <summary>
        ///     get first Progress by char index
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public KeyValuePair<TimeLineIndex, TimeLine> GetFirstProgressPointByIndex(TimeLineIndex charIndex)
        {
            var result = FindPrevioud(charIndex).Value;
            return result;
        }

        /// <summary>
        ///     get last Progress by char index
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public KeyValuePair<TimeLineIndex, TimeLine> GetLastProgressPointByIndex(TimeLineIndex charIndex)
        {
            /*
            var point = this.FirstOrDefault(x => x.Key > charIndex);
            return point; //?? lyric.TimeLines.Last();
            */
            var result = FindNext(charIndex).Value;

            return result;
        }

        /// <summary>
        ///     will check if this progress point is valid
        /// </summary>
<<<<<<< HEAD:osu.Game.Rulesets.Karaoke/Objects/TimeLine/LyricTimeLineList.cs
        public new void Add(int key, LyricTimeLine point)
=======
        /// <returns><c>true</c>, if progress point was added, <c>false</c> otherwise.</returns>
        /// <param name="karaokeObject">Karaoke object.</param>
        public new void Add(TimeLineIndex key, TimeLine point)
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db:osu.Game.Rulesets.Karaoke/Objects/TimeLine/TimeLineList.cs
        {
            //cannot add same key
            if (this.Any(x => x.Key == key))
                return;

            base.Add(key, point);

            FixTime();
        }

        /// <summary>
        ///     fix the delta time
        /// </summary>
        public void FixTime()
        {
            foreach (var single in this)
            {
                if (single.Value.Duration < MinimumTime)
                    single.Value.Duration = MinimumTime;
            }
        }

        //just change value
        public event Action<TimeLineList> ValueChangerd;

        //change number of time point
        public event Action<TimeLineList> SizeChanged;
    }
}
