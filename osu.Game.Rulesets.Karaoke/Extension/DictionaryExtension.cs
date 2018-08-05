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

        /// <summary>
        /// Get key if value is exist
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryGetKey<K, V>(this Dictionary<K, V> dictionary, V value, out K key)
        {
            
            foreach (var keyValuePair in dictionary)
            {
                if (keyValuePair.Value.Equals(value))
                {
                    key = keyValuePair.Key;
                    return true;
                }
            }

            //if not found
            key = default;
            return false;
        }

        public static bool TryToRemove<K, V>(this Dictionary<K, V> dictionary, K key)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
                return true;
            }
            return false;
        }

        public static bool ReassignKey<K, V>(this Dictionary<K, V> dictionary, K oldKey, K newKey)
        {
            if (dictionary.ContainsKey(oldKey) && !dictionary.ContainsKey(newKey))
            {
                var item = dictionary[oldKey];
                dictionary.Add(newKey, item);
                return true;
            }

            return false;
        }

        public static bool AddOrReplace<K, V>(this Dictionary<K, V> dictionary, K key, V value, bool replaceIfExist = true)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
                return true;
            }
            else if(replaceIfExist)
            {
                dictionary[key] = value;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
