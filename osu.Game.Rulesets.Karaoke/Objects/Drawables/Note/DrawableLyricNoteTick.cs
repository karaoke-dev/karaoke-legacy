using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    /// <summary>
    ///     Visualises a <see cref="HoldNoteTick" /> hit object.
    /// </summary>
    public class DrawableLyricNoteTick : DrawableBaseNote<BaseLyric>
    {
        /// <summary>
        ///     References the time at which the user started holding the hold note.
        /// </summary>
        public Func<double?> HoldStartTime;

        public override Color4 AccentColour
        {
            get => base.AccentColour;
            set
            {
                base.AccentColour = value;

                glowContainer.EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Glow,
                    Radius = 2f,
                    Roundness = 15f,
                    Colour = value.Opacity(0.3f)
                };
            }
        }

        private readonly Container glowContainer;

        public DrawableLyricNoteTick(BaseLyric hitObject)
            : base(hitObject)
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;

            RelativeSizeAxes = Axes.X;
            Size = new Vector2(1);

            InternalChildren = new[]
            {
                glowContainer = new CircularContainer
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0,
                            AlwaysPresent = true
                        }
                    }
                }
            };
        }

        protected override void CheckForJudgements(bool userTriggered, double timeOffset)
        {
            if (!userTriggered)
                return;

            if (Time.Current < HitObject.StartTime)
                return;

            if (HoldStartTime?.Invoke() > HitObject.StartTime)
                return;

            AddJudgement(new KaraokeNoteTickJudgement { Result = HitResult.Perfect });
        }

        protected override void UpdateState(ArmedState state)
        {
            switch (State.Value)
            {
                case ArmedState.Hit:
                    AccentColour = Color4.Green;
                    break;
            }
        }

        /*
        protected override void Update()
        {
            if (AllJudged)
                return;

            if (HoldStartTime?.Invoke() == null)
                return;

            UpdateJudgement(true);
        }
        */
    }
}
