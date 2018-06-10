// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Service.Lyric
{
    public class LyricServiceProvider
    {
        /// <summary>
        ///     Get translator by providerType
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static ILyricService GetLyricServiceByProvider(LyricServiceProviderType providerType)
        {
            switch (providerType)
            {
                case LyricServiceProviderType.MusixMatch:
                    return new MusixMatchLyric();
                default:
                    return null;
            }
        }
    }

    public enum LyricServiceProviderType
    {
        /// <summary>
        ///     The romaji server just for osu!Karaoke
        ///     <see cref="MusixMatchLyric" />
        /// </summary>
        MusixMatch = 1
    }
}
