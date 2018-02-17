// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Objects.Extension
{
    public static class KaraokeObjectExtension
    {
        public static LyricProgressPoint GetFirstProgressPointByTime(this Lyric lyric, double nowRelativeTime)
        {
            if (lyric.IsInTime(nowRelativeTime) && lyric.ProgressPoints.Count > 0)
            {
                var index = lyric.ProgressPoints.FindIndex(x => x.RelativeTime > nowRelativeTime);
                return index > 0 ? lyric.ProgressPoints[index - 1] : new LyricProgressPoint(0, -1);
            }

            return new LyricProgressPoint(0, -1);
        }

        public static LyricProgressPoint GetLastProgressPointByTime(this Lyric lyric, double nowRelativeTime)
        {
            if (lyric.IsInTime(nowRelativeTime) && lyric.ProgressPoints.Count > 0)
            {
                var point = lyric.ProgressPoints.Find(x => x.RelativeTime > nowRelativeTime);
                return point ?? lyric.ProgressPoints.Last();
            }

            return null;
        }

        public static LyricProgressPoint GetFirstProgressPointByIndex(this Lyric lyric, int charIndex)
        {
            var index = lyric.ProgressPoints.FindIndex(x => x.CharIndex > charIndex);
            if (index == 0)
                return new LyricProgressPoint(0, -1);

            //if -1 , means last
            return index > 0 ? lyric.ProgressPoints[index - 1] : (lyric.ProgressPoints.LastOrDefault() ?? new LyricProgressPoint(0, -1));
        }

        public static LyricProgressPoint GetLastProgressPointByIndex(this Lyric lyric, int charIndex)
        {
            var point = lyric.ProgressPoints.Find(x => x.CharIndex > charIndex);
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
