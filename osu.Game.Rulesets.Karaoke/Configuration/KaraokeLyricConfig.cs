// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    /// karaoke lyric config
    /// </summary>
    public class KaraokeLyricConfig : RecordChangeObject, ICopyable
    {
        /// <summary>
        /// show subText
        /// </summary>
        public bool SubTextVislbility { get; set; } = true;

        /// <summary>
        /// show romaji
        /// </summary>
        public bool RomajiVislbility { get; set; } = true;

        /// <summary>
        /// romaji first
        /// </summary>
        public bool RomajiFirst { get; set; } = true;

        /// <summary>
        /// Show translate
        /// </summary>
        public bool ShowTranslate { get; set; } = true;

        /// <summary>
        /// Copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Copy<T>() where T : class, ICopyable, new()
        {
            T result = new T();
            if (result is KaraokeLyricConfig karaokeLyricConfig)
            {
                karaokeLyricConfig.SubTextVislbility = SubTextVislbility;
                karaokeLyricConfig.RomajiVislbility = RomajiVislbility;
                karaokeLyricConfig.RomajiFirst = RomajiFirst;
                karaokeLyricConfig.ShowTranslate = ShowTranslate;
                karaokeLyricConfig.Initialize();
            }
            return result;
        }
    }
}
