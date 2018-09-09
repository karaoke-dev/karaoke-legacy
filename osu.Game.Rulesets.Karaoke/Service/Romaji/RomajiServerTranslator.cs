﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Localization;
using osu.Game.Rulesets.Karaoke.Objects.Localization.Types;
using osu.Game.Rulesets.Karaoke.Online.API.Romaj.RomajiServer;

namespace osu.Game.Rulesets.Karaoke.Service.Romaji
{
    /// <summary>
    ///     can translate any langlage to romaji
    /// </summary>
    public class RomajiServerTranslator : IRomajiTranslator
    {
        private readonly RomajiServerApi romajiServerApi = new RomajiServerApi();

        /// <summary>
        ///     translte list
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<BaseLyric> Translate(TranslateCode code, BaseLyric translateListString)
        {
            return (await Translate(code, new List<BaseLyric> { translateListString })).FirstOrDefault();
        }


        /// <summary>
        ///     translte list
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<List<BaseLyric>> Translate(TranslateCode code, List<BaseLyric> translateListString)
        {
            var listTranslate = new List<BaseLyric>();
            var result = await romajiServerApi.Translate(code, translateListString.Select(x => x.Lyric.Text).ToList());

            //convert each sentence
            foreach (var single in result)
            {
                var singleTranslate = new BaseLyric();

                //convert from Translatersult to lyruc
                for (var i = 0; i < single.Result.Count; i++)
                {
                    var character = single.Result[i];

                    //romaji
                    if (singleTranslate is IHasRomaji romajiLyric)
                        romajiLyric.Romaji.Add(i, new RomajiText
                        {
                            Text = character.Romaji
                        });

                    //means it is kanji
                    if (character.Type == 0)
                        if (singleTranslate is IHasFurigana furiganaLyric)
                            furiganaLyric.Furigana.Add(i, new FuriganaText
                            {
                                Text = character.Katakana
                            });
                }

                listTranslate.Add(singleTranslate);
            }

            return listTranslate;
        }
    }
}
