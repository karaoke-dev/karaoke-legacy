using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    /// <summary>
    ///     Visualises a <see cref="BaseLyric" /> hit object.
    /// </summary>
    public class DrawableNote : DrawableBaseNote<BaseLyric>
    {
        public override Color4 AccentColour
        {
            get => base.AccentColour;
            set
            {
                base.AccentColour = value;
                laneGlowPiece.AccentColour = AccentColour;
                GlowPiece.AccentColour = AccentColour;
                headPiece.AccentColour = AccentColour;
            }
        }

        protected readonly GlowPiece GlowPiece;

        private readonly LaneGlowPiece laneGlowPiece;
        private readonly NotePiece headPiece;

        public DrawableNote(BaseLyric hitObject)
            : base(hitObject)
        {
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                laneGlowPiece = new LaneGlowPiece
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                GlowPiece = new GlowPiece(),
                headPiece = new NotePiece
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft
                }
            };
        }

        protected override void CheckForJudgements(bool userTriggered, double timeOffset)
        {
            if (!userTriggered)
            {
                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    AddJudgement(new KaraokeJudgement { Result = HitResult.Miss });
                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);
            if (result == HitResult.None)
                return;

            AddJudgement(new KaraokeJudgement { Result = result });
        }

        protected override void UpdateState(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                case ArmedState.Miss:
                    this.FadeOut(100).Expire();
                    break;
            }
        }
    }
}
