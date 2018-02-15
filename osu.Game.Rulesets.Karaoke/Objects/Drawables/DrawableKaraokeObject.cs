// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.IO.Stores;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Extension;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables
{
    /// <summary>
    /// Karaoke Text
    /// </summary>
    public class DrawableKaraokeObject : DrawableHitObject<KaraokeObject>, IAmDrawableKaraokeObject
    {
        //Private
        private KaraokeTemplate _template;

        private KaraokeSinger _singer;

        //Const
        public const float TIME_FADEIN = 100;

        public const float TIME_FADEOUT = 100;

        //Object
        public KaraokeObject KaraokeObject => HitObject;

        private KaraokeLyricConfig _style;
        public KaraokeLyricConfig Style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateDrawable();
            }
        }

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
            get => KaraokeObject.PreemptiveTime ?? 600;
            set
            {
                KaraokeObject.PreemptiveTime = value;
                UpdateDrawable();
            }
        }

        private TranslateCode _translateCode;

        public TranslateCode TranslateCode
        {
            get => _translateCode;
            set
            {
                _translateCode = value;
                UpdateDrawable();
            }
        }

        public virtual bool ProgressUpdateByTime { get; set; } = true;

        //Drawable
        public TextsAndMask TextsAndMaskPiece { get; set; } = new TextsAndMask();
        public KaraokeText TranslateText { get; set; } = new KaraokeText(null);

        private double _nowProgress;

        public DrawableKaraokeObject(KaraokeObject hitObject)
            : base(hitObject)
        {
            Alpha = 0;

            Template = new KaraokeTemplate();
            TranslateCode = TranslateCode.English;

            Children = new Drawable[]
            {
                TextsAndMaskPiece,
                TranslateText,
            };
        }

        /// <summary>
        /// update view
        /// </summary>
        protected virtual void UpdateDrawable()
        {
            TextsAndMaskPiece.ClearAllText();
            //main text
            TextsAndMaskPiece.AddMainText(Template?.MainText + KaraokeObject.MainText);

            //subtext
            foreach (var singleText in KaraokeObject.ListSubTextObject)
            {
                //1. recalculate position
                var startPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(singleText.CharIndex - 1);
                var endPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(singleText.CharIndex);
                singleText.X = (startPosition + endPosition) / 2;
                //2. update to subtext
                TextsAndMaskPiece.AddSubText(Template?.SubText + singleText);
            }

            Width = TextsAndMaskPiece.MainText.GetTextEndPosition(); //KaraokeObject.Width ?? (Template?.Width ?? 700);
            Height = KaraokeObject.Height ?? (Template?.Height ?? 100);

            UpdateValue();
        }

        protected virtual void UpdateValue()
        {
            //Color
            Color4 textColor = Singer?.LytricColor ?? Color4.Blue;
            Color4 backgroundColor = Singer?.LytricBackgroundColor ?? Color4.White;
            TextsAndMaskPiece.SetColor(textColor, backgroundColor);

            //translate text
            TranslateText.TextObject = Template?.TranslateText + KaraokeObject.ListTranslate.Where(x => x.LangCode == LangTagConvertor.GetCode(TranslateCode)).FirstOrDefault();
            TranslateText.Colour = Template?.TranslateTextColor ?? Color4.White;

            Scale = new Vector2(Template?.Scale ?? 1);

            //update progress
            Progress = Progress;
        }

        [BackgroundDependencyLoader]
        private void load(FontStore store)
        {
            UpdateDrawable();
        }

        public override float Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                TextsAndMaskPiece.SetWidth(base.Width);
            }
        }

        public override float Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
                TextsAndMaskPiece.SetHeight(base.Height);
            }
        }

        protected override void Update()
        {
            base.Update();

            if (!ProgressUpdateByTime)
                return;

            double currentRelativeTime = Time.Current - HitObject.StartTime;
            if (HitObject.IsInTime(currentRelativeTime))
            {
                //TODO : get progress point
                var startProgressPoint = HitObject.GetFirstProgressPointByTime(currentRelativeTime);
                var endProgressPoint = HitObject.GetLastProgressPointByTime(currentRelativeTime);

                var startPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(startProgressPoint.CharIndex);
                var endPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(endProgressPoint.CharIndex);

                var relativeTime = currentRelativeTime - startProgressPoint.RelativeTime;
                //Update progress
                Progress = startPosition + (endPosition - startPosition) / (float)(endProgressPoint.RelativeTime - startProgressPoint.RelativeTime) * (float)relativeTime;

                Show();
                Alpha = 1;
            }
            else
            {
                Hide();
                Alpha = 0;
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

            ApplyTransformsAt(transformTime, true);
            ClearTransformsAfter(transformTime, true);

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

            //wse Update() to jujgement show / hide instead
            //var sequence = this.Delay(HitObject.Duration).FadeOut(TIME_FADEOUT).Expire();

            //Expire();
        }

        public virtual void AddTranslate(TranslateCode code, string translateResult)
        {
            //Add and show translate in here
            TranslateText.Text = translateResult;
        }
    }
}
