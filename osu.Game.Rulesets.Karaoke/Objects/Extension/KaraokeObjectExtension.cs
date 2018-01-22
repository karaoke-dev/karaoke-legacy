// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Objects.Extension
{
    public static class KaraokeObjectExtension
    {
        public static ProgressPoint GetFirstProgressPointByTime(this KaraokeObject karaokeObject, double nowRelativeTime)
        {
            if (karaokeObject.IsInTime(nowRelativeTime) && karaokeObject.ListProgressPoint.Count > 0)
            {
                var index = karaokeObject.ListProgressPoint.FindIndex(x => x.RelativeTime > nowRelativeTime);
                return index > 0 ? karaokeObject.ListProgressPoint[index - 1] : new ProgressPoint(0, -1);
            }

            return new ProgressPoint(0, -1);
        }

        public static ProgressPoint GetLastProgressPointByTime(this KaraokeObject karaokeObject, double nowRelativeTime)
        {
            if (karaokeObject.IsInTime(nowRelativeTime) && karaokeObject.ListProgressPoint.Count > 0)
            {
                var point = karaokeObject.ListProgressPoint.Find(x => x.RelativeTime > nowRelativeTime);
                return point ?? karaokeObject.ListProgressPoint.Last();
            }

            return null;
        }

        public static ProgressPoint GetFirstProgressPointByIndex(this KaraokeObject karaokeObject, int charIndex)
        {
            var index = karaokeObject.ListProgressPoint.FindIndex(x => x.CharIndex > charIndex);
            if (index == 0)
                return new ProgressPoint(0, -1);

            //if -1 , means last
            return index > 0 ? karaokeObject.ListProgressPoint[index - 1] : (karaokeObject.ListProgressPoint.LastOrDefault() ?? new ProgressPoint(0, -1));
        }

        public static ProgressPoint GetLastProgressPointByIndex(this KaraokeObject karaokeObject, int charIndex)
        {
            var point = karaokeObject.ListProgressPoint.Find(x => x.CharIndex > charIndex);
            return point; //?? karaokeObject.ListProgressPoint.Last();
        }

        /// <summary>
        /// Times the is in time.
        /// </summary>
        /// <returns><c>true</c>, if is in time was timed, <c>false</c> otherwise.</returns>
        /// <param name="karaokeObject">Karaoke object.</param>
        /// <param name="nowRelativeTime">Now time.</param>
        public static bool IsInTime(this KaraokeObject karaokeObject, double nowRelativeTime)
        {
            if (nowRelativeTime > -karaokeObject.PreemptiveTime && nowRelativeTime <= karaokeObject.Duration + karaokeObject.EndPreemptiveTime)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// will filter if has same languate
        /// </summary>
        /// <param name="karaokeObject">Karaoke object.</param>
        public static bool AddNewTranslate(this KaraokeObject karaokeObject, KaraokeTranslateString translateString)
        {
            return false;
        }
    }
}
