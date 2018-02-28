// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class RomajiTextList : LyricDictionary<int, RomajiText>
    {
        /// <summary>
        /// get romaji start position from main text's text index
        /// </summary>
        /// <param name="mainTextIndex"></param>
        /// <returns></returns>
        public int GetStartRomajiIndexFromMainTextIndex(int mainTextIndex)
        {
            return listRomajiTextCount.Take(mainTextIndex).Sum();
        }

        /// <summary>
        /// get romaji start position from main text's text index
        /// </summary>
        /// <param name="mainTextIndex"></param>
        /// <returns></returns>
        public int GetEndRomajiIndexFromMainTextIndex(int mainTextIndex)
        {
            var take = mainTextIndex >= listRomajiTextCount.Count ? mainTextIndex + 1 : mainTextIndex;
            return listRomajiTextCount.Take(take).Sum() - 1;
        }

        public string SeperateText { get; set; }

        /// <summary>
        /// collect all romaji in list
        /// </summary>
        public string Romaji
        {
            get
            {
                var list = this.Select(x => x.Value.Text);
                string result = string.Join(SeperateText, list);
                return result;
            }
        }

        /// <summary>
        /// collect list 
        /// </summary>
        private List<int> listRomajiTextCount => this.Select(x => x.Value.Text.Length).ToList(); 
    }
}
