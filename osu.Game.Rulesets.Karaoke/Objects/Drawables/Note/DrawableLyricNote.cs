using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Skinning;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public class DrawableLyricNote : SkinReloadableDrawable
    {
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

        public virtual BaseLyric HitObject { get; set; }

        public virtual KeyValuePair<TimeLinePoint, TimeLine.TimeLine> TimeLine
        {
            get => _timeLine;
            set
            {
                _timeLine = value;

                var tone = _timeLine.Value.Tone ?? new Tone();

                //height
                noteContainer.Y = NoteStageHelper.GetPositionByTone(tone);

                //text
                if (!string.IsNullOrEmpty(TimeLine.Value.DisplayText))
                {
                    text.Text = TimeLine.Value.DisplayText;
                }
                else
                {
                    var prevTimeLine = HitObject.TimeLines.GetPrevious(TimeLine.Key);
                    var lyric = HitObject.Lyric.Text;
                    var take = 0;
                    var displayText = "";
                    if (prevTimeLine != null)
                    {
                        take = _timeLine.Key.Index - prevTimeLine.Value.Key.Index;
                        displayText = lyric.Substring(prevTimeLine.Value.Key.Index + 1, take);
                    }
                    else
                    {
                        take = _timeLine.Key.Index;
                        displayText = lyric.Substring(0, take + 1);
                    }

                    text.Text = displayText;
                }
            }
        }

        private readonly DrawableHeadNote head;
        private readonly DrawableTailNote tail;
        private readonly TextFlowContainer text;

        private readonly GlowPiece glowPiece;
        private readonly BodyPiece bodyPiece;
        private readonly Container fullHeightContainer;

        private readonly Container<DrawableLyricNoteTick> tickContainer;

        private readonly Container noteContainer;

        /// <summary>
        ///     Time at which the user started holding this hold note. Null if the user is not holding this hold note.
        /// </summary>
        private double? holdStartTime;

        /// <summary>
        ///     Whether the hold note has been released too early and shouldn't give full score for the release.
        /// </summary>
        private bool hasBroken;

        private Color4 accentColour;

        private KeyValuePair<TimeLinePoint, TimeLine.TimeLine> _timeLine;

        public DrawableLyricNote()
            : base(null)
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
                        tickContainer = new Container<DrawableLyricNoteTick>
                        {
                            RelativeSizeAxes = Axes.Both
                            //ChildrenEnumerable = HitObject.NestedHitObjects.OfType<BaseLyric>().Select(tick => new DrawableKaraokeNoteTick(tick)
                            //{
                            //    HoldStartTime = () => holdStartTime
                            //})
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
                        text = new TextFlowContainer
                        {
                            Text = "Hello"
                        }
                    }
                }
            };


            foreach (var tick in tickContainer)
                noteContainer.Add(tick);

            //noteContainer.Add(head);
            //noteContainer.Add(tail);
        }

        protected virtual void UpdateState(ArmedState state)
        {
            switch (state)
            {
                //case ArmedState.Hit:
                // Good enough for now, we just want them to have a lifetime end
                //    this.Delay(2000).Expire();
                //    break;
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
