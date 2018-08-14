﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    /// <summary>
    ///     Drawable BaseLyric Mask
    /// </summary>
    public class LyricMask : HitObjectMask
    {
        public LyricMask(DrawableEditableKaraokeObject drawableLyric)
            : base(drawableLyric)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            Position = drawableLyric.Position;

            Scale = drawableLyric.Scale;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Yellow;
        }

        protected override void Update()
        {
            base.Update();
            var rowPosition = Parent.ToLocalSpace(HitObject.ScreenSpaceDrawQuad.TopLeft);
            Position = new Vector2(rowPosition.X, rowPosition.Y - 200);
            Size = HitObject.DrawSize;
        }
    }
}
