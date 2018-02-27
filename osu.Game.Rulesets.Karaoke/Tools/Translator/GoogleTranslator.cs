// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Online.API.Translate.Google;

namespace osu.Game.Rulesets.Karaoke.Tools.Translator
{
    public class GoogleTranslator : TranslateorBase
    {
        /// <summary>
        /// translate multi string
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="targetLangCode"></param>
        /// <param name="translateListString"></param>
        public override async void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, List<string> translateListString)
        {
            try
            {
                GoogleTranslateApi transpateApi = new GoogleTranslateApi();
                var result = await transpateApi.Translate(sourceLangeCode, targetLangCode, translateListString);

                List<string> returnString = new List<string>();
                foreach (var single in result)
                {
                    returnString.Add(single.TranslatedText);
                }

                OnTranslateMultiStringSuccess?.Invoke(this, returnString);
            }
            catch (Exception e)
            {
                OnTranslateFail?.Invoke(this, e.Message);
            }
        }

        public override async void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, string translateString)
        {
            try
            {
                GoogleTranslateApi transpateApi = new GoogleTranslateApi();
                var result = await transpateApi.Translate(sourceLangeCode, targetLangCode, new List<string>() { translateString });

                var translateText = result.FirstOrDefault()?.TranslatedText;

                OnTranslateSuccess?.Invoke(this, translateText);
            }
            catch (Exception e)
            {
                OnTranslateFail?.Invoke(this, e.Message);
            }
        }
    }
}
