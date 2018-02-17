// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.IO.Stores;
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
    public class DrawableKaraokeObject : DrawableHitObject<Lyric>, IAmDrawableKaraokeObject
    {
        //Const
        public const float TIME_FADEIN = 100;
        public const float TIME_FADEOUT = 100;


        private double _nowProgress;

        #region Field

        //Private
        private KaraokeLyricConfig _config;
        private KaraokeTemplate _template;
        private KaraokeSinger _singer;
        private TranslateCode _translateCode;


        /// <summary>
        /// Gets the karaoke object.
        /// </summary>
        /// <value>The karaoke object.</value>
        public Lyric Lyric => HitObject;

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        public KaraokeLyricConfig Config
        {
            get => _config;
            set
            {
                _config = value;
                UpdateDrawable();
            }
        }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>The template.</value>
        public KaraokeTemplate Template
        {
            get => _template;
            set
            {
                _template = value;
                UpdateDrawable();
            }
        }

        /// <summary>
        /// Gets or sets the singer.
        /// </summary>
        /// <value>The singer.</value>
        public KaraokeSinger Singer
        {
            get => _singer;
            set
            {
                _singer = value;
                UpdateDrawable();
            }
        }

        /// <summary>
        /// Gets or sets the translate code.
        /// </summary>
        /// <value>The translate code.</value>
        public TranslateCode TranslateCode
        {
            get => _translateCode;
            set
            {
                _translateCode = value;
                UpdateDrawable();
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the preemptive time.
        /// </summary>
        /// <value>The preemptive time.</value>
        public double PreemptiveTime
        {
            get => Lyric.PreemptiveTime ?? 600;
            set
            {
                Lyric.PreemptiveTime = value;
                UpdateDrawable();
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="T:osu.Game.Rulesets.Karaoke.Objects.Drawables.DrawableKaraokeObject"/> progress update by time.
        /// </summary>
        /// <value><c>true</c> if progress update by time; otherwise, <c>false</c>.</value>
        public virtual bool ProgressUpdateByTime { get; set; } = true;

        //Drawable
        public TextsAndMask TextsAndMaskPiece { get; set; } = new TextsAndMask();
        public KaraokeText TranslateText { get; set; } = new KaraokeText(null);



        public DrawableKaraokeObject(Lyric hitObject)
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
            TextsAndMaskPiece.AddMainText(Template?.MainText + Lyric.MainText);

            //subtext
            foreach (var singleText in Lyric.ListSubTextObject)
            {
                //1. recalculate position
                var startPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(singleText.CharIndex - 1);
                var endPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(singleText.CharIndex);
                singleText.X = (startPosition + endPosition) / 2;
                //2. update to subtext
                TextsAndMaskPiece.AddSubText(Template?.SubText + singleText);
            }

            Width = TextsAndMaskPiece.MainText.GetTextEndPosition(); //Lyric.Width ?? (Template?.Width ?? 700);
            Height = Lyric.Height ?? (Template?.Height ?? 100);

            UpdateValue();
        }

        protected virtual void UpdateValue()
        {
            //Color
            Color4 textColor = Singer?.LytricColor ?? Color4.Blue;
            Color4 backgroundColor = Singer?.LytricBackgroundColor ?? Color4.White;
            TextsAndMaskPiece.SetColor(textColor, backgroundColor);

            //translate text
            TranslateText.TextObject = Template?.TranslateText + Lyric.ListTranslate.Where(x => x.LangCode == LangTagConvertor.GetCode(TranslateCode)).FirstOrDefault();
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
