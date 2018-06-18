using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces;
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

                //if next is not empty
                return thisTimeLine.Duration - (thisTimeLine.EarlyTime ?? 0);
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

        public virtual KeyValuePair<int, LyricTimeLine> TimeLine
        {
            get => _timeLine;
            set
            {
                _timeLine = value;
                //height
                var noteHeight = (_timeLine.Value.Tone ?? 0) * KaraokeStage.COLUMN_HEIGHT;
                noteContainer.Y = noteHeight;

                //text
                if (!string.IsNullOrEmpty(TimeLine.Value.DisplayText))
                {
                    text.Text = TimeLine.Value.DisplayText;
                }
                else
                {
                    var nextTimeLine = HitObject.TimeLines.GetNext(TimeLine.Key)?.Key;
                    var lyric = HitObject.Lyric.Text;
                    int take = 0;
                    if (nextTimeLine != null)
                    {
                        take = nextTimeLine.Value - _timeLine.Key;
                    }
                    else
                    {
                        take = lyric.Length - _timeLine.Key;
                    }
                    var displayText = lyric.Substring(_timeLine.Key, take);
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

        private readonly Container<DrawableKaraokeNoteTick> tickContainer;

        /// <summary>
        ///     Time at which the user started holding this hold note. Null if the user is not holding this hold note.
        /// </summary>
        private double? holdStartTime;

        /// <summary>
        ///     Whether the hold note has been released too early and shouldn't give full score for the release.
        /// </summary>
        private bool hasBroken;

        private readonly Container noteContainer;

        private Color4 accentColour;

        private KeyValuePair<int, LyricTimeLine> _timeLine;

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
                        tickContainer = new Container<DrawableKaraokeNoteTick>
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
