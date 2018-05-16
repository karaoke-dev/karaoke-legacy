using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public class DrawableBarLine : DrawableHitObject<BarLine>
    {
        /// <summary>
        /// Height of major bar line triangles.
        /// </summary>
        private const float triangle_height = 12;

        /// <summary>
        /// Offset of the major bar line triangles from the sides of the bar line.
        /// </summary>
        private const float triangle_offset = 9;

        public DrawableBarLine(BarLine barLine)
            : base(barLine)
        {
            RelativeSizeAxes = Axes.X;
            Height = 1;

            AddInternal(new Box
            {
                Name = "Bar line",
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                RelativeSizeAxes = Axes.Both,
            });

            bool isMajor = barLine.BeatIndex % (int)barLine.ControlPoint.TimeSignature == 0;

            if (isMajor)
            {
                AddInternal(new EquilateralTriangle
                {
                    Name = "Left triangle",
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(triangle_height),
                    X = -triangle_offset,
                    Rotation = 90
                });

                AddInternal(new EquilateralTriangle
                {
                    Name = "Right triangle",
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.TopCentre,
                    Size = new Vector2(triangle_height),
                    X = triangle_offset,
                    Rotation = -90
                });
            }

            if (!isMajor && barLine.BeatIndex % 2 == 1)
                Alpha = 0.2f;
        }

        protected override void UpdateState(ArmedState state)
        {

        }
    }
}
