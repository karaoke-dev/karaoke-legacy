using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Service.Translator;
using osu.Game.Rulesets.Karaoke.Tools.Translator;

namespace osu.Game.Rulesets.Karaoke.Service.Translate
{
    public class Translateor
    {
        /// <summary>
        /// Get translator by provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static TranslatorBase GetTranslatorByProvider(TranslatorProvider provider)
        {
            switch (provider)
            {
                case TranslatorProvider.Github:
                    return null;
                case TranslatorProvider.Google:
                    return new GoogleTranslator();
                    default:
                        return null;
            }
        }
    }
}
