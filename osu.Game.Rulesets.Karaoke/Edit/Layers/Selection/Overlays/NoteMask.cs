using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Skinning;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    public class NoteMask : HitObjectMask
    {
        public new DrawableEditableKaraokeNoteGroup HitObject => (DrawableEditableKaraokeNoteGroup)base.HitObject;

        private readonly FillFlowContainer<SingleNoteMask> listNote;

        public NoteMask(DrawableEditableKaraokeNoteGroup hitObject)
            : base(hitObject)
        {
            Scale = hitObject.Scale;

            CornerRadius = 5;
            Masking = true;

            RelativeSizeAxes = Axes.Y;

            InternalChildren = new Drawable[]
            {
                listNote = new FillFlowContainer<SingleNoteMask>
                {
                    Name = "Background",
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both
                }
            };

            //initial note
            InitialNote();
        }

        protected virtual void InitialNote()
        {
            foreach (var timeline in HitObject.HitObject.TimeLines)
            {
                var note = new SingleNoteMask
                {
                    HitObject = HitObject.HitObject,
                    TimeLine = timeline
                };
                listNote.Add(note);
            }
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            foreach (var single in listNote)
            {
                single.AccentColour = colours.Yellow;
            }
        }

        private float _lastWidth;
        protected override void Update()
        {
            base.Update();

            Size = new Vector2(HitObject.DrawSize.X / 2.367f, HitObject.DrawSize.Y / 505);
            Scale = HitObject.Scale;

            //means width changed
            if (Math.Abs(_lastWidth - DrawWidth) > 0)
            {
                _lastWidth = DrawWidth;
                foreach (var note in listNote)
                {
                    var precentage = note.Duration / HitObject.HitObject.Duration;
                    note.Width = (float)(_lastWidth * precentage);
                }
            }

            Position = Parent.ToLocalSpace(HitObject.ScreenSpaceDrawQuad.TopLeft);
        }

        private class SingleNoteMask : Container
        {
            public virtual BaseLyric HitObject { get; set; }

            private KeyValuePair<TimeLineIndex, TimeLine> _timeLine;

            private readonly Container noteContainer;
            private readonly BodyPiece bodyPiece;
            private readonly Container fullHeightContainer;
            private readonly GlowPiece glowPiece;
            private readonly DrawableHeadNote head;
            private readonly DrawableTailNote tail;

            public virtual double Duration
            {
                get
                {
                    var thisTimeLine = TimeLine.Value;
                    var previousTimeLine = HitObject.TimeLines.GetPrevious(TimeLine.Key)?.Value;

                    //if next is not empty
                    if (previousTimeLine != null)
                        return thisTimeLine.RelativeTime - previousTimeLine.RelativeTime - (thisTimeLine.EarlyTime ?? 0);
                    return thisTimeLine.RelativeTime;
                }
            }

            private Color4 accentColour;
            public virtual Color4 AccentColour
            {
                get => accentColour;
                set
                {
                    accentColour = value;

                    glowPiece.AccentColour = value;
                    bodyPiece.AccentColour = value;
                    //head.AccentColour = value;
                    //tail.AccentColour = value;
                }
            }

            public SingleNoteMask()
            {
                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                RelativeSizeAxes = Axes.Y;
                InternalChildren = new Drawable[]
                {
                    noteContainer = new Container
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
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
                                RelativeSizeAxes = Axes.Y
                            },
                            head = new DrawableHeadNote
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft
                            },
                            tail = new DrawableTailNote
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft
                            },
                        }
                    }
                };
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

            public virtual KeyValuePair<TimeLineIndex, TimeLine> TimeLine
            {
                get => _timeLine;
                set
                {
                    _timeLine = value;

                    var tone = _timeLine.Value.Tone ?? new Tone();

                    //height
                    noteContainer.Y = NoteStageHelper.GetPositionByTone(tone);
                }
            }

            /// <summary>
            ///     The head note of a hold.
            /// </summary>
            private class DrawableHeadNote : SkinReloadableDrawable
            {
                public DrawableHeadNote()
                {
                    Anchor = Anchor.CentreLeft;
                    Origin = Anchor.CentreLeft;
                }
            }

            /// <summary>
            ///     The tail note of a hold.
            /// </summary>
            private class DrawableTailNote : SkinReloadableDrawable
            {
                /// <summary>
                ///     Lenience of release hit windows. This is to make cases where the hold note release
                ///     is timed alongside presses of other hit objects less awkward.
                ///     Todo: This shouldn't exist for non-LegacyBeatmapDecoder beatmaps
                /// </summary>
                private const double release_window_lenience = 1.5;

                public DrawableTailNote()
                {
                    Anchor = Anchor.CentreLeft;
                    Origin = Anchor.CentreLeft;
                }
            }
        }
    }
}
