using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace osu.Game.Rulesets.Karaoke.Objects.TimeLine
{
    public class TimeLinePoint : IComparable , IEquatable<TimeLinePoint>
    {
        [JsonIgnore]
        public static int MaxCharNumber = 10;

        public int Index { get; set; }

        public float? Percentage { get; set; }

        public TimeLinePoint()
        {

        }

        public TimeLinePoint(int index, float? percentage = null)
        {
            Index = index;
            Percentage = percentage;
        }

        /// <summary>
        /// Comrare
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is TimeLinePoint point)
            {
                return this.GetHashCode() - point.GetHashCode();
            }
            throw new InvalidCastException(nameof(obj) + " is not "+ nameof(TimeLinePoint));
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TimeLinePoint other)
        {
            if (other?.Index == Index && other.Percentage == Index)
                return true;

            return false;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TimeLinePoint)obj);
        }

        /// <summary>
        /// Generate hash code
        /// Used to compare value
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Index * MaxCharNumber) + (int)((Percentage ?? 1) * MaxCharNumber);
            }
        }
    }
}
