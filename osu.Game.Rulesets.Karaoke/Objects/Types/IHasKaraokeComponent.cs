// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// define the basic class of karaoke object
    /// </summary>
    public interface IHasKaraokeComponent
    {
        TextObject MainText { get; set; }

        List<SubTextObject> ListSubTextObject { get; set; }

        ListProgressPoint ListProgressPoint { get; set; }

        ListKaraokeTranslateString ListTranslate { get; set; }

        int? TemplateIndex { get; set; }

        int? SingerIndex { get; set; }
    }
}
