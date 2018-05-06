// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Tools.Translator;

namespace osu.Game.Rulesets.Karaoke.Service.Translator
{
    public class TranslatorServiceProvider
    {
        /// <summary>
        /// Get translator by providerType
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static ITranslator GetTranslatorByProvider(TranslatorProviderType providerType)
        {
            switch (providerType)
            {
                case TranslatorProviderType.Github:
                    return null;
                case TranslatorProviderType.Google:
                    return new GoogleTranslator();
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// Translator providor type
    /// </summary>
    public enum TranslatorProviderType
    {
        /// <summary>
        /// Google.
        /// <see cref="GoogleTranslator"/>
        /// </summary>
        Google = 1,

        /// <summary>
        /// The github.
        /// </summary>
        Github = 2,
    }
}
