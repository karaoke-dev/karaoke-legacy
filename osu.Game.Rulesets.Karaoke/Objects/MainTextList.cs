// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using Newtonsoft.Json;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    public class MainTextList : LyricDictionary<int, MainText>, IHasText
    {
        [JsonIgnore] public string Delimiter = "";

        public string Text
        {
            get
            {
                string result = this.Select(i => i.Value.Text).Aggregate((i, j) => i + Delimiter + j);
                return result;
            }
        }

        public static MainTextList SetJapaneseLyric(string str)
        {
            MainTextList returnList = new MainTextList();
            int startCharIndex = 0;
            foreach (var singleCharacter in str)
            {
                returnList.Add(startCharIndex, new MainText()
                {
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
