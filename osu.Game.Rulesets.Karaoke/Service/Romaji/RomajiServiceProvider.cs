// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Service.Romaji;

namespace osu.Game.Rulesets.Karaoke.Tools.Romaji
{
    public class RomajiServiceProvider
    {
        /// <summary>
        ///     Get translator by providerType
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IRomajiTranslator GetTranslatorByProvider(RomajiServiceProviderType providerType)
        {
            switch (providerType)
            {
                case RomajiServiceProviderType.Github:
                    return new GithubTranslator();
                case RomajiServiceProviderType.KaraokeRomajiServer:
                    return new RomajiServerTranslator();
                default:
                    return null;
            }
        }
    }

    public enum RomajiServiceProviderType
    {
        /// <summary>
        ///     The romaji server just for osu!Karaoke
        ///     <see cref="RomajiServerTranslator" />
        /// </summary>
        KaraokeRomajiServer = 1,

        /// <summary>
        ///     The service that upload by othe people.
        ///     <see cref="GithubTranslator" />
        /// </summary>
        Github = 2
    }
}
