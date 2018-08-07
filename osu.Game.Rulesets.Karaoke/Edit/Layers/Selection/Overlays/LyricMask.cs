// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;

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
            Origin = Anchor.Centre;
            Position = drawableLyric.Position;
            
            Scale = drawableLyric.Scale;

            CornerRadius = Size.X / 2;

            //AddInternal(new RingPiece());

            //drawableLyric.HitObject.PositionChanged += _ => Position = hitCircle.Position;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Yellow;
        }

        protected override void Update()
        {
            base.Update();
            Position = Parent.ToLocalSpace(HitObject.ScreenSpaceDrawQuad.TopLeft);
            Size = HitObject.DrawSize;
        }
    }
}
