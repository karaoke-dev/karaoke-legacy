using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// Lyric Dictionary
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public class LyricDictionary<Key,Value> : Dictionary<Key, Value>
    {
        /// <summary>
        /// if key already exist, 'won;t be replaced
        /// </summary>
        /// <param name="sourceDictionary"></param>
        public void Fill(Dictionary<Key, Value> sourceDictionary)
        {
            foreach (var single in sourceDictionary)
            {
                if(!this.ContainsKey(single.Key))
                {
                    Add(single.Key,single.Value);
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
                if (this.ContainsKey(single.Key))
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
    }
}
