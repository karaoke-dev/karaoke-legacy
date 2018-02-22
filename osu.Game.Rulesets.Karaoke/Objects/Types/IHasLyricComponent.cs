// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Tools.Translator;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// define the basic class of karaoke object
    /// </summary>
    public interface IHasLyricComponent
    {
        /// <summary>
        /// Main Text list 
        /// </summary>
        MainTextList MainText { get; set; }

        /// <summary>
        /// subText list
        /// </summary>
        Dictionary<int, SubText> SubTexts { get; set; }

        /// <summary>
        /// romaji text list
        /// </summary>
        RomajiTextList RomajiTextListRomajiTexts { get; set; }

        /// <summary>
        /// list progress point
        /// </summary>
        LyricProgressPointList ProgressPoints { get; set; }

        /// <summary>
        /// list translate
        /// </summary>
        ListKaraokeTranslateString Translates { get; set; }


        /// <summary>
        /// template index
        /// </summary>
        int? TemplateIndex { get; set; }

        /// <summary>
        /// singer index
        /// </summary>
        int? SingerIndex { get; set; }

        /// <summary>
        /// translate code
        /// </summary>
        TranslateCode TranslateCode { get; set; }
    }
}
