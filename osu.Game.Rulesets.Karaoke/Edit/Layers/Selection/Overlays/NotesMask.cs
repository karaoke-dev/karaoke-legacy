// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    public class NotesMask : HitObjectMask
    {
        public new DrawableEditableNotes HitObject => (DrawableEditableNotes)base.HitObject;

        private readonly FillFlowContainer<SingleNoteMask> listNote;

        public NotesMask(DrawableEditableNotes hitObject)
            : base(hitObject)
        {
            Scale = hitObject.Scale;

            RelativeSizeAxes = Axes.Y;

            InternalChildren = new Drawable[]
            {
                listNote = new FillFlowContainer<SingleNoteMask>
                {
                    Name = "Background",
                    Direction = FillDirection.Horizontal,
                    RelativeSizeAxes = Axes.Both
                },
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

            Size = new Vector2(HitObject.DrawSize.X, HitObject.DrawSize.Y / 540);
            Scale = HitObject.Scale;
            Position = Parent.ToLocalSpace(HitObject.ScreenSpaceDrawQuad.TopLeft);

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
        }

        private class SingleNoteMask : Container
        {
            public virtual BaseLyric HitObject { get; set; }

            private KeyValuePair<TimeLineIndex, TimeLine> _timeLine;

            private readonly Container noteContainer;
            private readonly NotesMask fullHeightContainer;

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
                    fullHeightContainer.AccentColour = accentColour;
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
                        RelativeSizeAxes = Axes.X,
                        Children = new Drawable[]
                        {
                            fullHeightContainer = new NotesMask
                            {
                                RelativeSizeAxes = Axes.Both,
                            },
                        }
                    }
                };
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

            public class NotesMask : CompositeDrawable, IHasAccentColour
            {
                public Color4 AccentColour
                {
                    get => accentColour;
                    set
                    {
                        if (accentColour == value)
                            return;
                        accentColour = value;

                        updateGlow();
                    }
                }

                private Color4 accentColour;

                public NotesMask()
                {
                    RelativeSizeAxes = Axes.Both;
                    Masking = true;
                    CornerRadius = 5f;

                    InternalChild = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0,
                        AlwaysPresent = true
                    };
                }

                protected override void LoadComplete()
                {
                    base.LoadComplete();
                    updateGlow();
                }

                private void updateGlow()
                {
                    if (!IsLoaded)
                        return;

                    EdgeEffect = new EdgeEffectParameters
                    {
                        Type = EdgeEffectType.Shadow,
                        Colour = AccentColour,
                        Radius = 2,
                        Hollow = true
                    };
                }
            }
        }
    }
}
