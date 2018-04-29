// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// BaseLyric Dictionary
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public class LyricDictionary<Key, Value> : Dictionary<Key, Value> where Key : IComparable
    {
        /// <summary>
        /// if key already exist, 'won;t be replaced
        /// </summary>
        /// <param name="sourceDictionary"></param>
        public void Fill(Dictionary<Key, Value> sourceDictionary)
        {
            foreach (var single in sourceDictionary)
            {
                if (!ContainsKey(single.Key))
                {
                    Add(single.Key, single.Value);
                }
            }
        }

        /// <summary>
        /// if key already exist, will be replaced
        /// </summary>
        /// <param name="sourceDictionary"></param>
        public void Replace(Dictionary<Key, Value> sourceDictionary)
        {
            foreach (var single in sourceDictionary)
            {
                //if exist then remove
                if (ContainsKey(single.Key))
                    Remove(single.Key);

                Add(single.Key, single.Value);
            }
        }

        /// <summary>
        /// clean all value and fill all the value from sourceDictionary
        /// </summary>
        /// <param name="sourceDictionary"></param>
        public void CleanAndReplace(Dictionary<Key, Value> sourceDictionary)
        {
            Clear();

            //fill
            Fill(sourceDictionary);
        }

        public KeyValuePair<Key, Value>? Find(Key key)
        {
            return this.FirstOrDefault(x => x.Key.CompareTo(key) == 0);
        }

        /// <summary>
        /// find previous by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValuePair<Key, Value>? FindPrevioud(Key key)
        {
            var result = Keys.Where(x => x.CompareTo(key) < 0);
            if (result.Count() < 2)
                return this.First();

            var previousKey = result.Max();
            return Find(previousKey);
        }

        /// <summary>
        /// find next from key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public KeyValuePair<Key, Value>? FindNext(Key key)
        {
            var result = Keys.Where(x => x.CompareTo(key) > 0);
            if (result.Count() < 2)
                return this.Last();
            var nextKey = result.Min();
            return Find(nextKey);
        }

        /// <summary>
        /// add new remaji
        /// </summary>
        /// <param name="value"></param>
        public new void Add(Key key, Value value)
        {
            //filter
            if (this.Any(x => x.Key.Equals(key)))
                return;

            //Add
            base.Add(key, value);

            Sort();
        }

        /// <summary>
        /// sorting by position and time should be higher
        /// </summary>
        public void Sort()
        {
            //sort
            var sortedDic = this.OrderBy(x => x.Key).ToDictionary(keyvalue => keyvalue.Key, keyvalue => keyvalue.Value);

            //re-add
            Clear();
            foreach (var single in sortedDic)
            {
                base.Add(single.Key, single.Value);
            }
        }
    }
}
