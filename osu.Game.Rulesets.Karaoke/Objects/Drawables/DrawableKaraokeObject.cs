// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Extension;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables
{
    /// <summary>
    /// Karaoke Text
    /// </summary>
    public class DrawableKaraokeObject : DrawableHitObject<KaraokeObject> , IAmDrawableKaraokeObject
    {
        //Private
        private KaraokeTemplate _template;
        private KaraokeSinger _singer;
        private double _preemptiveTime = 600;

        //Const
        public const float TIME_FADEIN = 100;
        public const float TIME_FADEOUT = 100;

        //Object
        public KaraokeObject KaraokeObject => HitObject;

        public KaraokeTemplate Template
        {
            get => _template;
            set
            {
                _template = value;
                UpdateDrawable();
            }
        }

        public KaraokeSinger Singer
        {
            get => _singer;
            set
            {
                _singer = value;
                UpdateDrawable();
            }
        }

        public double PreemptiveTime
        {
            get => _preemptiveTime;
            set
            {
                _preemptiveTime = value;
                UpdateDrawable();
            }
        }

        public bool ProgressUpdateByTime { get; set; } = true;

        //Drawable
        public TextsAndMask TextsAndMaskPiece { get; set; } = new TextsAndMask();

        public KaraokeText TranslateText { get; set; } = new KaraokeText(null);

        private double _nowProgress;

        public DrawableKaraokeObject(KaraokeObject hitObject)
            : base(hitObject)
        {
            Alpha = 0;

            UpdateDrawable();

            Children = new Drawable[]
            {
                TextsAndMaskPiece,
                TranslateText,
            };

        }

        /// <summary>
        /// update view
        /// </summary>
        protected void UpdateDrawable()
        {
            //Color
            Color4 textColor = Singer?.LytricColor ?? Color4.Blue;
            Color4 backgroundColor = Singer?.LytricBackgroundColor ?? Color4.White;
            TextsAndMaskPiece.SetColor(textColor, backgroundColor);

            
            TextsAndMaskPiece.ClearAllText();
            //main text
            TextsAndMaskPiece.AddMainText(Template?.MainText + KaraokeObject.MainText);
            //subtext
            foreach (var singleText in KaraokeObject.ListSubTextObject)
            {
                TextsAndMaskPiece.AddText(Template?.SubText + singleText);
            }

            //translate text
            TranslateText.TextObject = Template?.TranslateText;
            TranslateText.Colour = Template?.TranslateTextColor ?? Color4.White;

            float width = KaraokeObject.Width ?? (Template?.Width ?? 700);
            float height = KaraokeObject.Height ?? (Template?.Height ?? 100);
            SetWidth(width);
            SetHeight(height);
        }

        public void SetWidth(float width)
        {
            TextsAndMaskPiece.SetWidth(width);
            Width = width;
        }

        public void SetHeight(float height)
        {
            TextsAndMaskPiece.SetHeight(height);
            Height = height;
        }

        protected override void Update()
        {
            base.Update();

            if (!ProgressUpdateByTime)
                return;

            double currentRelativeTime = Time.Current - HitObject.StartTime;
            if(HitObject.IsInTime(currentRelativeTime))
            {
                //TODO : get progress point
                var startProgressPoint = HitObject.GetFirstProgressPointByTime(currentRelativeTime);
                var endProgressPoint = HitObject.GetLastProgressPointByTime(currentRelativeTime);

                var startPosition= TextsAndMaskPiece.MainKaraokeText.GetEndPositionByIndex(startProgressPoint.CharIndex);
                var endPosition = TextsAndMaskPiece.MainKaraokeText.GetEndPositionByIndex(endProgressPoint.CharIndex);

                var relativeTime = currentRelativeTime - startProgressPoint.RelativeTime;
                //Update progress
                Progress = startPosition + (endPosition - startPosition) / (float)(endProgressPoint.RelativeTime - startProgressPoint.RelativeTime) * (float)relativeTime;

                this.Show();
                Alpha = 1;
            }
            else
            {
                //this.Hide();
                //Alpha = 0;
            }
        }

        /// <summary>
        /// progress
        /// </summary>
        /// <value>The progress.</value>
        public double Progress
        {
            get => _nowProgress;
            set
            {
                _nowProgress = value;
                TextsAndMaskPiece.MovingMask((float)_nowProgress);
            }
        }

        protected sealed override void UpdateState(ArmedState state)
        {

            double transformTime = HitObject.StartTime - PreemptiveTime;

            base.ApplyTransformsAt(transformTime, true);
            base.ClearTransformsAfter(transformTime, true);

            using (BeginAbsoluteSequence(transformTime, true))
            {
                UpdatePreemptState();

                using (BeginDelayedSequence(PreemptiveTime + (Judgements.FirstOrDefault()?.TimeOffset ?? 0), true))
                    UpdateCurrentState(state);
            }
        }

        protected virtual void UpdatePreemptState()
        {
            this.FadeIn(TIME_FADEIN);
        }

        protected virtual void UpdateCurrentState(ArmedState state)
        {
            if (!ProgressUpdateByTime)
                return;

            //delay
            var sequence = this.Delay(HitObject.Duration).FadeOut(TIME_FADEOUT).Expire();

            //Expire();
        }

        public void AddTranslate(TranslateCode code, string translateResult)
        {
            //Add and show translate in here
            TranslateText.Text = translateResult;
            
        }

    }
}
