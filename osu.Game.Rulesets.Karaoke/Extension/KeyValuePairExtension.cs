using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Extension
{
    public static class KeyValuePairExtension
    {
        public static bool IsEqual<U, V>(this KeyValuePair<U, V> sourceDictionary, KeyValuePair<U, V> targetDictionary) where U : IComparable where V : IComparable
        {
            if (sourceDictionary.Key.Equals(targetDictionary.Key) && sourceDictionary.Value.Equals(targetDictionary.Value))
                return true;
            return false;
        }
    }
}
