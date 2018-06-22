using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions;

namespace osu.Game.Rulesets.Karaoke.Extension
{
    public static class DictionaryExtension
    {
        /// <summary>
        ///     Get pervious
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static KeyValuePair<K, V>? GetPrevious<K, V>(this Dictionary<K, V> dictionary, K key) where K : IComparable where V : class
        {
            var list = dictionary.ToList();

            var value = dictionary.FirstOrDefault(x => x.Key.Equals(key));

            var index = list.IndexOf(value);
            var targetIndex = index - 1;

            if (list.IsValidIndex(targetIndex))
                return list[targetIndex];

            return null;
        }

        /// <summary>
        ///     Get next
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static KeyValuePair<K, V>? GetNext<K, V>(this Dictionary<K, V> dictionary, K key) where K : IComparable where V : class
        {
            var list = dictionary.ToList();

            var value = dictionary.FirstOrDefault(x => x.Key.Equals(key));

            var index = list.IndexOf(value);
            var targetIndex = index + 1;

            if (list.IsValidIndex(targetIndex))
                return list[targetIndex];

            return null;
        }
    }
}
