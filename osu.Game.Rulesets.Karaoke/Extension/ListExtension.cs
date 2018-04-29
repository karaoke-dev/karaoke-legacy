// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Extension
{
    public static class ListExtension
    {
        /// <summary>
        /// 把list 拆分
        /// 暴力解決法
        /// http://www.jscto.net/html/31946.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="splitNumber"></param>
        /// <returns></returns>
        public static List<List<T>> Split<T>(this List<T> list, int splitNumber)
        {
            List<List<T>> listGroup = new List<List<T>>();
            int j = splitNumber;
            for (int i = 0; i < list.Count; i += splitNumber)
            {
                List<T> cList = new List<T>();
                cList = list.Take(j).Skip(i).ToList();
                j += splitNumber;
                listGroup.Add(cList);
            }

            return listGroup;
        }
    }
}
