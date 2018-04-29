// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.ControlPanel.Desktop.Pieces
{
    public class KaraokeIntroduceText : OsuSpriteText, IHasTooltip
    {
        public KaraokeIntroduceText()
        {
            UseFullGlyphHeight = false;
            Origin = Anchor.CentreLeft;
            Anchor = Anchor.TopLeft;
            TextSize = 20;
            Alpha = 1;
        }

        public string TooltipText { get; set; }
    }
}
