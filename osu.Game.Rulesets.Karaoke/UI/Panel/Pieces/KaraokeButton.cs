﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics.Cursor;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Rulesets.Karaoke.UI.Panel.Pieces
{
    /// <summary>
    /// inherit from osuButton
    /// Has tooltop
    /// </summary>
    public class KaraokeButton : OsuButton, IHasTooltip
    {
        public string TooltipText { get; set; }
    }
}
