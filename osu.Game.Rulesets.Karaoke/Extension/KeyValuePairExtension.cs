// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;

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
