// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// list Progress point
    /// [Note : key is mapped in Char index, not Dictionary's index]
    /// </summary>
    public class LyricTimeLineList : LyricDictionary<int, LyricTimeLine>
    {
        [JsonIgnore]
        public double MinimumTime { get; set; } = 100;


        /// <summary>
        /// get first progress point by time
        /// </summary>
        /// <param name="nowRelativeTime"></param>
        /// <returns></returns>
        public KeyValuePair<int, LyricTimeLine> GetFirstProgressPointByTime(double nowRelativeTime)
        {
            /*
            var index = this.FirstOrDefault(x => x.Value.RelativeTime > nowRelativeTime).Key;

            LyricProgressPoint result;
            this.TryGetValue(index - 1, out result);

            if (result == null)
                return new KeyValuePair<int, LyricProgressPoint>(-1, new LyricProgressPoint(0));

            return new KeyValuePair<int, LyricProgressPoint>(index - 1, result);
            */


            var result = this.Where(x => x.Value.RelativeTime <= nowRelativeTime).ToDictionary(x => x.Key, x => x.Value);

            if (result.Count() < 2)
                return this.First();

            var maxResult = Find(result.Keys.Max());

            if (maxResult.Equals(default(KeyValuePair<int, LyricTimeLine>)))
                return new KeyValuePair<int, LyricTimeLine>(-1, new LyricTimeLine(0));

            return maxResult.Value;
        }

        /// <summary>
        /// get last progress point by time
        /// </summary>
        /// <param name="lyric"></param>
        /// <param name="nowRelativeTime"></param>
        /// <returns></returns>
        public KeyValuePair<int, LyricTimeLine>? GetLastProgressPointByTime(double nowRelativeTime)
        {
            /*
            var point = this.FirstOrDefault(x => x.Value.RelativeTime > nowRelativeTime);

            if (point.Equals(default(KeyValuePair<string, int>)))
                return this.Last();

            return point;
            */

            if (Count == 0)
                return null;

            //result
            var result = this.Where(x => x.Value.RelativeTime > nowRelativeTime).ToDictionary(x => x.Key, x => x.Value);

            if (result.Count() < 2)
                return this.Last();

            var maxResult = Find(result.Keys.Max());

            if (maxResult.Equals(default(KeyValuePair<int, LyricTimeLine>)))
            {
                var key = Keys.Max();
                return Find(key);
            }

            return maxResult;
        }

        /// <summary>
        /// get first Progress by char index
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public KeyValuePair<int, LyricTimeLine> GetFirstProgressPointByIndex(int charIndex)
        {
            var result = FindPrevioud(charIndex).Value;
            return result;
        }

        /// <summary>
        /// get last Progress by char index
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public KeyValuePair<int, LyricTimeLine> GetLastProgressPointByIndex(int charIndex)
        {
            /*
            var point = this.FirstOrDefault(x => x.Key > charIndex);
            return point; //?? lyric.ProgressPoints.Last();
            */
            var result = FindNext(charIndex).Value;

            return result;
        }

        /// <summary>
        /// will check if this progress point is valid
        /// </summary>
        /// <returns><c>true</c>, if progress point was added, <c>false</c> otherwise.</returns>
        /// <param name="karaokeObject">Karaoke object.</param>
        public new void Add(int key, LyricTimeLine point)
        {
            if (this.Any(x => x.Value.RelativeTime == point.RelativeTime))
                return;

            base.Add(key, point);

            FixTime();
        }

        /// <summary>
        /// fix the delta time
        /// </summary>
        public void FixTime()
        {
            double time = 0;

            foreach (var single in this)
            {
                if (single.Value.RelativeTime < time + MinimumTime)
                {
                    single.Value.RelativeTime = time + MinimumTime;
                }

                time = single.Value.RelativeTime;
            }
        }
    }
}
