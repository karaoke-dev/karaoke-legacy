using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Service.Lyric
{
    public class LyricServiceProvider
    {
        /// <summary>
        /// Get translator by providerType
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
        /// The romaji server just for osu!Karaoke
        /// <see cref="MusixMatchLyric"/>
        /// </summary>
        MusixMatch,
    }
}
