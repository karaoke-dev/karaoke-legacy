// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Configuration;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Types
{
    public interface IDrawableLyricParameter
    {

        /// <summary>
        /// Object
        /// </summary>
        Objects.Lyric Lyric { get; }

        /// <summary>
        /// singer
        /// </summary>
        Singer Singer { get; set; }

        /// <summary>
        /// set preemptive time
        /// </summary>
        double PreemptiveTime { get; set; }

        /// <summary>
        /// translate text
        /// </summary>
        TranslateString TranslateText { get; set; }
    }
}
