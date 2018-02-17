using System.Collections.Generic;

namespace osu.Game.Rulesets.Karaoke.Helps
{
    public class DIctionaryHelper
    {
        /// <summary>
        /// use this method to re-assign new ID(Key),sort from small to large
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="sourceDictionary"></param>
        /// <param name="targetDictionary"></param>
        /// <returns></returns>
        public static bool ResharpDictionaryId<U,V>(Dictionary<int, U> sourceDictionary, Dictionary<int, V> targetDictionary) where U: struct where V : struct 
        {
            return false;
        }
    }
}
