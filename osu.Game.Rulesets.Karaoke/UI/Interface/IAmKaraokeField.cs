﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Rulesets.Karaoke.UI.Tool;

namespace osu.Game.Rulesets.Karaoke.UI.Interface
{
    /// <summary>
    /// if it is karaoke GameField, need to add this for Externsion use
    /// </summary>
    public interface IAmKaraokeField
    {
        /// <summary>
        /// ruleset
        /// </summary>
        Ruleset Ruleset { get; }

        /// <summary>
        /// working beatmap
        /// </summary>
        WorkingBeatmap WorkingBeatmap { get; }

        /// <summary>
        /// container, maybe will be use in some day i think
        /// </summary>
        KaraokeRulesetContainer KaraokeRulesetContainer { get; }

        /// <summary>
        /// some userful tools will be define in here
        /// </summary>
        KaraokeFieldTool KaraokeFieldTool { get; }

        /// <summary>
        /// list drawable Object
        /// </summary>
        List<IAmDrawableKaraokeObject> ListDrawableKaraokeObject { get; set; }
    }
}
