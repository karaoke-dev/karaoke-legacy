// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// use to record romaji
    /// </summary>
    public class RomajiTextObject : TextObject, IHasCharIndex, IHasCharEndIndex
    {
        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int CharIndex { get; set; }

        /// <summary>
        /// relativa to textIndex
        /// </summary>
        public int? CharEndIndex { get; set; }
    }

    public class ListRomajiTextObject : List<RomajiTextObject>
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
                var list = this.Select(x => x.Text);
                string result = string.Join(SeperateText, list);
                return result;
            }
        }

        /// <summary>
        /// collect list 
        /// </summary>
        private List<int> listRomajiTextCount => this.Select(x => x.Text.Length).ToList();

        /// <summary>
        /// add new remaji
        /// </summary>
        /// <param name="value"></param>
        public new void Add(RomajiTextObject value)
        {
            //Add
            base.Add(value);
        }
    }
}
