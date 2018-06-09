using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public class DrawableLyricNote : DrawableBaseNote<BaseLyric>
    {
        private readonly DrawableNote head;
        private readonly DrawableNote tail;

        private readonly GlowPiece glowPiece;
        private readonly BodyPiece bodyPiece;
        private readonly Container fullHeightContainer;

        private readonly Container<DrawableKaraokeNoteTick> tickContainer;

        /// <summary>
        /// Time at which the user started holding this hold note. Null if the user is not holding this hold note.
        /// </summary>
        private double? holdStartTime;

        /// <summary>
        /// Whether the hold note has been released too early and shouldn't give full score for the release.
        /// </summary>
        private bool hasBroken;

        private Container noteContainer;

        public DrawableLyricNote(float height, BaseLyric hitObject) : base(hitObject)
        {
            RelativeSizeAxes = Axes.Y;
            InternalChildren = new Drawable[]
            {
                noteContainer = new Container()
                {
                    Y = height,
                    Height = KaraokeStage.COLUMN_HEIGHT,
                    Children = new Drawable[]
                    {
                        // The hit object itself cannot be used for various elements because the tail overshoots it
                        // So a specialized container that is updated to contain the tail height is used
                        fullHeightContainer = new Container
                        {
                            RelativeSizeAxes = Axes.Y,
                            Child = glowPiece = new GlowPiece()
                        },
                        bodyPiece = new BodyPiece
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft,
                            RelativeSizeAxes = Axes.Y,
                        },
                        tickContainer = new Container<DrawableKaraokeNoteTick>
                        {
                            RelativeSizeAxes = Axes.Both,
                            ChildrenEnumerable = HitObject.NestedHitObjects.OfType<BaseLyric>().Select(tick => new DrawableKaraokeNoteTick(tick)
                            {
                                HoldStartTime = () => holdStartTime
                            })
                        },
                        head = new DrawableHeadNote(this)
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft
                        },
                        tail = new DrawableTailNote(this)
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreLeft
                        },
                        new TextFlowContainer()
                        {
                            Text = "Hello",
                        }
                    }
                }
            };


            foreach (var tick in tickContainer)
                noteContainer.Add(tick);

            //noteContainer.Add(head);
            //noteContainer.Add(tail);
        }

        private Color4 accentColour;
        public override Color4 AccentColour
        {
            get { return accentColour; }
            set
            {
                accentColour = value;

                glowPiece.AccentColour = value;
                bodyPiece.AccentColour = value;
                head.AccentColour = value;
                tail.AccentColour = value;
            }
        }

        public virtual double Duration
        {
            get
            {
                //TODO : real value
                return 500;
            }
        }

        protected override void UpdateState(ArmedState state)
        {
            switch (state)
            {
                case ArmedState.Hit:
                    // Good enough for now, we just want them to have a lifetime end
                    this.Delay(2000).Expire();
                    break;
            }
        }

        protected override void Update()
        {
            base.Update();

            // Make the body piece not lie under the head note
            bodyPiece.X = head.Width;
            bodyPiece.Width = DrawWidth - head.Width;

            // Make the fullHeightContainer "contain" the height of the tail note, keeping in mind
            // that the tail note overshoots the height of this hit object
            fullHeightContainer.Width = DrawWidth + tail.Width;
        }

        /// <summary>
        /// The head note of a hold.
        /// </summary>
        private class DrawableHeadNote : DrawableNote
        {
            private readonly DrawableLyricNote holdNote;

            public DrawableHeadNote(DrawableLyricNote holdNote)
                : base(holdNote.HitObject)
            {
                this.holdNote = holdNote;

                GlowPiece.Alpha = 0;
            }

            protected override void UpdateState(ArmedState state)
            {
                // The holdnote keeps scrolling through for now, so having the head disappear looks weird
            }
        }

        /// <summary>
        /// The tail note of a hold.
        /// </summary>
        private class DrawableTailNote : DrawableNote
        {
            /// <summary>
            /// Lenience of release hit windows. This is to make cases where the hold note release
            /// is timed alongside presses of other hit objects less awkward.
            /// Todo: This shouldn't exist for non-LegacyBeatmapDecoder beatmaps
            /// </summary>
            private const double release_window_lenience = 1.5;

            private readonly DrawableLyricNote holdNote;

            public DrawableTailNote(DrawableLyricNote holdNote)
                : base(holdNote.HitObject)
            {
                this.holdNote = holdNote;

                GlowPiece.Alpha = 0;
            }

            protected override void CheckForJudgements(bool userTriggered, double timeOffset)
            {
                // Factor in the release lenience
                timeOffset /= release_window_lenience;

                if (!userTriggered)
                {
                    if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    {
                        AddJudgement(new KaraokeNoteTailJudgement
                        {
                            Result = HitResult.Miss,
                            HasBroken = holdNote.hasBroken
                        });
                    }

                    return;
                }

                var result = HitObject.HitWindows.ResultFor(timeOffset);
                if (result == HitResult.None)
                    return;

                AddJudgement(new KaraokeNoteTailJudgement
                {
                    Result = result,
                    HasBroken = holdNote.hasBroken
                });
            }

            protected override void UpdateState(ArmedState state)
            {
                // The holdnote keeps scrolling through, so having the tail disappear looks weird
            }
        }
    }
}
