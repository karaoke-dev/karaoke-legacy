// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Online.API.Romaj.RomajiServer;

namespace osu.Game.Rulesets.Karaoke.Tools.Romaji
{
    /// <summary>
    /// can translate any langlage to romaji
    /// </summary>
    public class RomajiTranslatorBase
    {
        private RomajiServerApi RomajiServerApi = new RomajiServerApi();

        /// <summary>
        /// translte list 
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<Lyric> Translate(TranslateCode code,Lyric translateListString)
        {
            return (await Translate(code,new List<Lyric>() { translateListString })).FirstOrDefault();
        }


        /// <summary>
        /// translte list 
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<List<Lyric>> Translate(TranslateCode code,List<Lyric> translateListString)
        {
            List <Lyric> listTranslate =new List<Lyric>();
            var result = await RomajiServerApi.Translate(code,translateListString.Select(x => x.MainText.Text).ToList());

            //convert each sentence
            foreach (var single in result)
            {
                var singleTranslate = new Lyric();

                //convert from Translatersult to lyruc
                for (int i = 0; i < single.Result.Count; i++)
                {
                    var character = single.Result[i];

                    //romaji
                    singleTranslate.RomajiTextListRomajiTexts.Add(i,new RomajiText()
                    {
                        Text = character.Romaji,
                    });

                    //means it is kanji
                    if (character.Type == 0)
                    {
                        singleTranslate.SubTexts.Add(i, new SubText()
                        {
                            Text = character.Katakana,
                        });
                    }
                }

                listTranslate.Add(singleTranslate);
            }
            return listTranslate;
        }
    }
}
