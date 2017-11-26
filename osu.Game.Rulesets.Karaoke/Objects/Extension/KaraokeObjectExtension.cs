using System;
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
                return karaokeObject.ListProgressPoint.Find(x => x.RelativeTime > nowRelativeTime);
            }

            return null;
        }

        /// <summary>
        /// Times the is in time.
        /// </summary>
        /// <returns><c>true</c>, if is in time was timed, <c>false</c> otherwise.</returns>
        /// <param name="karaokeObject">Karaoke object.</param>
        /// <param name="nowRelativeTime">Now time.</param>
        public static bool IsInTime(this KaraokeObject karaokeObject, double nowRelativeTime)
        {
            if (nowRelativeTime > 0 && nowRelativeTime <= karaokeObject.Duration)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// will filter if has same languate
        /// </summary>
        /// <param name="karaokeObject">Karaoke object.</param>
        public static bool AddNewTranslate(this KaraokeObject karaokeObject,KaraokeTranslateString translateString)
        {
            return false;
        }

        /// <summary>
        /// will check if this progress point is valid
        /// </summary>
        /// <returns><c>true</c>, if progress point was added, <c>false</c> otherwise.</returns>
        /// <param name="karaokeObject">Karaoke object.</param>
        public static bool AddProgressPoint(this KaraokeObject karaokeObject, ProgressPoint point)
        {
            //TODO : filter
            if (karaokeObject.ListProgressPoint.Any(x => x.CharIndex == point.CharIndex))
                return false;
            if (karaokeObject.ListProgressPoint.Any(x => x.RelativeTime == point.RelativeTime))
                return false;

            karaokeObject.ListProgressPoint.Add(point);
            karaokeObject.SortProgressPoint();
            return true;
        }

        /// <summary>
        /// sorting by position and time should be higher
        /// </summary>
        public static void SortProgressPoint(this KaraokeObject karaokeObject)
        {
            // from small to large
            karaokeObject.ListProgressPoint = karaokeObject.ListProgressPoint.OrderBy(x => x.RelativeTime).ToList();
        }
    }
}
