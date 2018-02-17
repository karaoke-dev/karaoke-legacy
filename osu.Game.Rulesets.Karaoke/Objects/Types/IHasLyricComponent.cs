﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// define the basic class of karaoke object
    /// </summary>
    public interface IHasLyricComponent
    {
        FormattedText MainText { get; set; }

        List<SubText> ListSubTextObject { get; set; }

        LyricProgressPointList ListLyricProgressPoint { get; set; }

        ListKaraokeTranslateString ListTranslate { get; set; }

        int? TemplateIndex { get; set; }

        int? SingerIndex { get; set; }
    }
}
