using osu.Game.Rulesets.Karaoke.Objects.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class MainTextList : List<MainText> , IHasText
    {
        [JsonIgnore]
        public string Delimiter = "";

        public string Text
        {
            get
            {
                string result = this.Select(i => i.Text).Aggregate((i, j) => i + Delimiter + j);
                return result;
            }
        }

        public static MainTextList SetJapaneseLyric(string str)
        {
            MainTextList returnList = new MainTextList();
            int startCharIndex = 0;
            foreach (var singleCharacter in str)
            {
                returnList.Add(new MainText()
                {
                    CharIndex = startCharIndex,
                    Text = singleCharacter.ToString(),
                });
                startCharIndex++;
            }

            return returnList;
        }

        public static MainTextList SetEnglishLyric(string str)
        {
            MainTextList returnList = new MainTextList();

            //TODO : implement

            return returnList;
        }
    }
}
