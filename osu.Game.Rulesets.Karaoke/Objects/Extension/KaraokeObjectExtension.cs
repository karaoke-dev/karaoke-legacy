// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using System.Collections.Generic;

namespace osu.Game.Rulesets.Karaoke.Objects.Extension
{
    public static class KaraokeObjectExtension
    {
        public static KeyValuePair<int, LyricProgressPoint> GetFirstProgressPointByTime(this Lyric lyric, double nowRelativeTime)
        {
            if (lyric.IsInTime(nowRelativeTime) && lyric.ProgressPoints.Count > 0)
            {
                var progressPoints = lyric.ProgressPoints;
                var index = progressPoints.FirstOrDefault(x => x.Value.RelativeTime > nowRelativeTime).Key;

                LyricProgressPoint result;
                progressPoints.TryGetValue(index - 1,out result);

                if(result ==null)
                    return new KeyValuePair<int, LyricProgressPoint>(-1, new LyricProgressPoint(0));

                return new KeyValuePair<int, LyricProgressPoint>(index - 1, result);
            }

            return new KeyValuePair<int, LyricProgressPoint>(-1,new LyricProgressPoint(0)) ;
        }

        public static KeyValuePair<int, LyricProgressPoint>? GetLastProgressPointByTime(this Lyric lyric, double nowRelativeTime)
        {
            if (lyric.IsInTime(nowRelativeTime) && lyric.ProgressPoints.Count > 0)
            {
                var point = lyric.ProgressPoints.FirstOrDefault(x => x.Value.RelativeTime > nowRelativeTime);

                if (point.Equals(default(KeyValuePair<string, int>)))
                    return lyric.ProgressPoints.Last();

                return point;
            }

            return null;
        }

        public static KeyValuePair<int, LyricProgressPoint> GetFirstProgressPointByIndex(this Lyric lyric, int charIndex)
        {
            var index = lyric.ProgressPoints.FirstOrDefault(x => x.Key > charIndex).Key;

            LyricProgressPoint result;
            lyric.ProgressPoints.TryGetValue(index - 1, out result);

            if (result == null)
                return new KeyValuePair<int, LyricProgressPoint>(-1, new LyricProgressPoint(0));

            return new KeyValuePair<int, LyricProgressPoint>(index - 1, result);
        }

        public static KeyValuePair<int, LyricProgressPoint> GetLastProgressPointByIndex(this Lyric lyric, int charIndex)
        {
            var point = lyric.ProgressPoints.FirstOrDefault(x => x.Key > charIndex);
            return point; //?? lyric.ProgressPoints.Last();
        }

        /// <summary>
        /// Times the is in time.
        /// </summary>
        /// <returns><c>true</c>, if is in time was timed, <c>false</c> otherwise.</returns>
        /// <param name="lyric">Karaoke object.</param>
        /// <param name="nowRelativeTime">Now time.</param>
        public static bool IsInTime(this Lyric lyric, double nowRelativeTime)
        {
            if (nowRelativeTime > -lyric.PreemptiveTime && nowRelativeTime <= lyric.Duration + lyric.EndPreemptiveTime)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// will filter if has same languate
        /// </summary>
        /// <param name="lyric">Karaoke object.</param>
        public static bool AddNewTranslate(this Lyric lyric, LyricTranslate translate)
        {
            return false;
        }
    }
}
