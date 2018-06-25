using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace osu.Game.Rulesets.Karaoke.Objects.TimeLine
{
    public class TimeLineIndex : IComparable , IEquatable<TimeLineIndex>
    {
        [JsonIgnore]
        public static int MaxCharNumber = 10;

        public int Index { get; set; }

        public float? Percentage { get; set; }

        public TimeLineIndex()
        {

        }

        public TimeLineIndex(int index, float? percentage = null)
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
            if (obj is TimeLineIndex point)
            {
                return this.GetHashCode() - point.GetHashCode();
            }
            throw new InvalidCastException(nameof(obj) + " is not "+ nameof(TimeLineIndex));
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TimeLineIndex other)
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
            return Equals((TimeLineIndex)obj);
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
