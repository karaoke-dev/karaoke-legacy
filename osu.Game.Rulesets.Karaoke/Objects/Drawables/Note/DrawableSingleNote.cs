// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Audio.Track;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public class DrawableSingleNote : Container
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
                background.Colour = value;
                InitialKiaiEffect();
            }
        }

        private Objects.Lyric lyric;

        public virtual Objects.Lyric HitObject
        {
            get => lyric;
            set
            {
                lyric = value;
                InitialKiaiEffect();
            }
        }

        public virtual KeyValuePair<TimeLineIndex, TimeLine.TimeLine> TimeLine
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
                    var lyric = HitObject.MainLyric.Text;
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

        private readonly TextFlowContainer text;
        private readonly Container background;
        private readonly DrawableSingleNoteContainer noteContainer;
        private Color4 accentColour;

        private KeyValuePair<TimeLineIndex, TimeLine.TimeLine> _timeLine;

        public DrawableSingleNote()
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            RelativeSizeAxes = Axes.Y;
            InternalChildren = new Drawable[]
            {
                noteContainer = new DrawableSingleNoteContainer
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Height = KaraokeStage.COLUMN_HEIGHT,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        background = new Container
                        {
                            Masking = true,
                            CornerRadius = 5,
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.6f,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                }
                            }
                        },
                        text = new TextFlowContainer
                        {
                            Padding = new MarginPadding { Left = 5, Top = 2 },
                            Text = "Hello"
                        }
                    }
                }
            };

            InitialKiai();
        }

        private const float edge_alpha_kiai = 0.5f;
        private const double pre_beat_transition_time = 0;

        protected virtual void InitialKiai()
        {
            noteContainer.OnBeatAction += (beatIndex, timingPoint, effectPoint, amplitudes) =>
            {
                if (!effectPoint.KiaiMode)
                    return;

                double duration = timingPoint.BeatLength * 4;

                var alpha = beatIndex % (int)timingPoint.TimeSignature != 0 ? 0.8f : 1;
                background
                    .FadeEdgeEffectTo(alpha, pre_beat_transition_time, Easing.OutQuint)
                    .Then()
                    .FadeEdgeEffectTo(edge_alpha_kiai, duration, Easing.OutQuint);
            };
        }

        protected virtual void InitialKiaiEffect()
        {
            bool kiai = HitObject.Kiai;
            background.EdgeEffect = new EdgeEffectParameters
            {
                Type = EdgeEffectType.Glow,
                Colour = AccentColour.Opacity(kiai ? edge_alpha_kiai : 1.0f),
                Radius = kiai ? 15 : 1
            };
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

        protected class DrawableSingleNoteContainer : BeatSyncedContainer
        {
            public Action<int, TimingControlPoint, EffectControlPoint, TrackAmplitudes> OnBeatAction;

            protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
            {
                OnBeatAction?.Invoke(beatIndex, timingPoint, effectPoint, amplitudes);
            }
        }
    }
}
