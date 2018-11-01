// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables.Pieces;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Osu.Edit.Masks.SliderMasks.Components
{
    public class SliderBodyPiece : CompositeDrawable
    {
        private readonly Slider slider;
        private readonly SnakingSliderBody body;

        public SliderBodyPiece(Slider slider)
        {
            this.slider = slider;
            InternalChild = body = new SnakingSliderBody(slider)
            {
                AccentColour = Color4.Transparent,
                PathWidth = slider.Scale * 64
            };

            slider.PositionChanged += _ => updatePosition();
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            body.BorderColour = colours.Yellow;

            updatePosition();
        }

        private void updatePosition() => Position = slider.StackedPosition;

        protected override void Update()
        {
            base.Update();

            Size = body.Size;
            OriginPosition = body.PathOffset;

            // Need to cause one update
            body.UpdateProgress(0);
            body.Refresh();
        }
    }
}
