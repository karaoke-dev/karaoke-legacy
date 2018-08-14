// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces;
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

                if (glowPiece == null)
                    return;

                glowPiece.AccentColour = value;
                bodyPiece.Colour = value;
            }
        }

        public virtual BaseLyric HitObject { get; set; }

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

        private readonly TextFlowContainer text;
        private readonly GlowPiece glowPiece;
        private readonly Box bodyPiece;
        private readonly Container fullHeightContainer;
        private readonly Container noteContainer;
        private Color4 accentColour;

        private KeyValuePair<TimeLineIndex, TimeLine.TimeLine> _timeLine;

        public DrawableSingleNote()
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
                        fullHeightContainer = new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Child = glowPiece = new GlowPiece()
                        },
                        bodyPiece = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.3f
                        },
                        text = new TextFlowContainer
                        {
                            Text = "Hello"
                        }
                    }
                }
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
    }
}
