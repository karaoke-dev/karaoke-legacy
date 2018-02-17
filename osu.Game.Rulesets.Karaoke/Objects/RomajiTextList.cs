using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class RomajiTextList : List<RomajiText>
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
        public new void Add(RomajiText value)
        {
            //Add
            base.Add(value);
        }
    }
}
