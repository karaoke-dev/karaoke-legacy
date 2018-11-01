// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Objects.Localization.Types;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects.Localization
{
    public class JpLyric : RomajiLyric, IHasFurigana
    {
        /// <summary>
        ///     Furigana
        /// TODO : Remove
        /// </summary>
        public Dictionary<int, FuriganaText> Furigana { get; set; } = new Dictionary<int, FuriganaText>();

        /// <summary>
        /// Combine
        /// </summary>
        public Dictionary<int, CombineText> Combine { get; set; } = new Dictionary<int, CombineText>();
    }

    /// <summary>
    ///     sub text TODO : Remove
    /// </summary>
    public class FuriganaText : IHasText, IHasEndIndex
    {
        /// <summary>
        ///     relativa to textIndex
        /// </summary>
        public int? Length { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    ///     Combine some text into one main text
    ///     Combined text will become to furiganatext
    /// </summary>
    public class CombineText
    {
        public int Take { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReplaceText { get; set; }
    }
}
