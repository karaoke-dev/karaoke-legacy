﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
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
        /// </summary>
        public Dictionary<int, FuriganaText> Furigana { get; set; } = new Dictionary<int, FuriganaText>();
    }

    /// <summary>
    ///     sub text
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
}
