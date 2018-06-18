// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

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
    public class LyricTimeLineList : LyricDictionary<int, LyricTimeLine>
    {
        [JsonIgnore]
        public double MinimumTime { get; set; } = 100;
       

        /// <summary>
        ///     get first Progress by char index
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public KeyValuePair<int, LyricTimeLine> GetFirstProgressPointByIndex(int charIndex)
        {
            var result = FindPrevioud(charIndex).Value;
            return result;
        }

        /// <summary>
        ///     get last Progress by char index
        /// </summary>
        /// <param name="charIndex"></param>
        /// <returns></returns>
        public KeyValuePair<int, LyricTimeLine> GetLastProgressPointByIndex(int charIndex)
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
        public new void Add(int key, LyricTimeLine point)
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
    }
}
