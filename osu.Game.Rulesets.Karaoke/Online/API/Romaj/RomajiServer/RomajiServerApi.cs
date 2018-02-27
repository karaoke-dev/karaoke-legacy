// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Threading.Tasks;
using osu.Framework.IO.Network;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Extension;

namespace osu.Game.Rulesets.Karaoke.Online.API.Romaj.RomajiServer
{
    /// <summary>
    /// this translate server is provided by : 
    /// https://romaji-translator.herokuapp.com/
    /// </summary>
    public class RomajiServerApi : BaseApi
    {
        protected override string Host => "http://localhost:1337/translate/jp/list";


        /// <summary>
        /// translte list 
        /// 
        /// TODO : 或許之後考慮使用皮皮牌的
        /// <see cref="WebRequest"/>
        /// 
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<List<TranslateResult>> Translate(TranslateCode code,List<string> translateListString)
        {
            //split the translate
            var listTranslate = translateListString.Split(10);

            var listCollection = new List<TranslateResult>();

            foreach (var singleTranslateProcess in listTranslate)
            {
                var parameter = new List<KeyValuePair<string, string>>();
                foreach (var singleLyric in singleTranslateProcess)
                {
                    parameter.Add(new KeyValuePair<string, string>("str", singleLyric));
                }
                var result =await GetObjectApi<List<TranslateResult>>("/translate/jp/list", parameter);

                listCollection.AddRange(result);
            }

            return listCollection;
        }


    }

    /// <summary>
    /// translate result
    /// </summary>
    public class TranslateResult
    {
        /// <summary>
        /// text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Hiragana
        /// </summary>
        public string Hiragana { get; set; }

        /// <summary>
        /// Katakana
        /// </summary>
        public string Katakana { get; set; }

        /// <summary>
        /// romaji
        /// </summary>
        public string Romaji { get; set; }
    }
}
