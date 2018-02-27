using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static List<List<T>> Split<T>(this List<T> list,int splitNumber)
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
