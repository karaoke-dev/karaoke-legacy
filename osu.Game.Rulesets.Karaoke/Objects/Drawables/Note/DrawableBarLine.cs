using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public class DrawableBarLine : DrawableHitObject<BarLine>
    {
        /// <summary>
        ///     Height of major bar line triangles.
        /// </summary>
        private const float triangle_height = 12;

        /// <summary>
        ///     Offset of the major bar line triangles from the sides of the bar line.
        /// </summary>
        private const float triangle_offset = 9;

        private readonly Container triangleContainer;

        public DrawableBarLine(BarLine barLine)
            : base(barLine)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.Centre;

            RelativeSizeAxes = Axes.Y;
            Width = 1;

            AddInternal(new Box
            {
                Name = "Bar line",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                EdgeSmoothness = new Vector2(0.5f, 0)
            });

            var isMajor = barLine.BeatIndex % (int)barLine.ControlPoint.TimeSignature == 0;

            if (isMajor)
                AddInternal(triangleContainer = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children = new[]
                    {
                        new EquilateralTriangle
                        {
                            Name = "Top",
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Position = new Vector2(0, -triangle_offset),
                            Size = new Vector2(-triangle_height),
                            EdgeSmoothness = new Vector2(1)
                        },
                        new EquilateralTriangle
                        {
                            Name = "Bottom",
                            Anchor = Anchor.BottomCentre,
                            Origin = Anchor.TopCentre,
                            Position = new Vector2(0, triangle_offset),
                            Size = new Vector2(triangle_height),
                            EdgeSmoothness = new Vector2(1)
                        }
                    }
                });

            if (!isMajor && barLine.BeatIndex % 2 == 1)
                Alpha = 0.2f;
        }

        protected override void UpdateState(ArmedState state)
        {
        }
    }
}
