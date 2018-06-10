// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.Objects.Translate;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    ///     define the basic class of karaoke object
    /// </summary>
    public interface ILyric : IHasLangCode, IHasLyricVersion
    {
        /// <summary>
        ///     Main Text list
        /// </summary>
        MainTextList Lyric { get; set; }

        /// <summary>
        ///     list progress point
        /// </summary>
        LyricTimeLineList TimeLines { get; set; }

        /// <summary>
        ///     list translate
        /// </summary>
        LyricTranslateList Translates { get; set; }


        /// <summary>
        ///     template index
        /// </summary>
        int? TemplateIndex { get; set; }

        /// <summary>
        ///     singer index
        /// </summary>
        int? SingerIndex { get; set; }
    }
}
