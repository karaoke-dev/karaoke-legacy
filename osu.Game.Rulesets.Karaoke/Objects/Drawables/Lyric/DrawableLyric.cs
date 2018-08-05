// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Types;
<<<<<<< HEAD
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
=======
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric
{
    /// <summary>
    ///     Karaoke Text
    /// </summary>
    public class DrawableLyric : DrawableHitObject<BaseLyric>, IDrawableLyricParameter, IDrawableLyricBindable
    {
        //Const
        public const float TIME_FADEIN = 0;

        public const float TIME_FADEOUT = 0;

        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="T:osu.Game.Rulesets.Karaoke.Objects.Drawables.BaseLyric.DrawableKaraokeObject" /> progress update by
        ///     time.
        /// </summary>
        /// <value><c>true</c> if progress update by time; otherwise, <c>false</c>.</value>
        public virtual bool ProgressUpdateByTime { get; set; } = true;

<<<<<<< HEAD
        //Drawable
        public Container TextsAndMaskPiece { get; }

        protected virtual LyricTextContainer LeftSideText { get; set; } = new LyricTextContainer();

        protected virtual LyricTextContainer RightSideText { get; set; } = new LyricTextContainer();

        public override float Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                LeftSideText.Width = Width;
                RightSideText.Width = Width;
            }
        }

        public override float Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
                LeftSideText.Height = Height;
                RightSideText.Height = Height;
            }
        }

=======
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
        /// <summary>
        ///     progress
        /// </summary>
        /// <value>The progress.</value>
        public double Progress
        {
            get => _nowProgress;
            set
            {
                _nowProgress = value;
<<<<<<< HEAD
                LeftSideText.SetMaskStartAndEndPosition(0, (float)Progress);
                RightSideText.SetMaskStartAndEndPosition((float)Progress, Width);
=======

                //TODO : 
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
            }
        }

        protected LyricContainer LyricContainer ;


        private double _nowProgress;

        public DrawableLyric(BaseLyric hitObject)
            : base(hitObject)
        {
            Alpha = 0;
<<<<<<< HEAD
            LeftSideText.Lyric = hitObject;
            RightSideText.Lyric = hitObject;

            InternalChildren = new Drawable[]
            {
                TextsAndMaskPiece = new Container()
                {
                    Children = new Drawable[]
                    {
                        RightSideText,
                        LeftSideText
                    },
=======

            InternalChildren = new Drawable[]
            {
                LyricContainer = new LyricContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    AutoSizeAxes = Axes.None,
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
                },
                TranslateText
            };

            Width = 300;

            LyricContainer.Lyric = hitObject;

            Style.ValueChanged += style => { OnStyleChange(); };
            Template.ValueChanged += style => { OnTemplateChange(); };
            SingerTemplate.ValueChanged += style => { OnSingerTemplateChange(); };
            TranslateCode.ValueChanged += style => { OnTranslateCodeChange(); };
        }

        protected virtual void OnStyleChange()
        {
<<<<<<< HEAD
            LeftSideText.Config = Style.Value;
            RightSideText.Config = Style.Value;
=======
            //LeftSideText.Config = Style.Value;
            //RightSideText.Config = Style.Value;
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
            UpdateDrawable();
        }

        protected virtual void OnTemplateChange()
        {
<<<<<<< HEAD
            LeftSideText.Template = Template.Value;
            RightSideText.Template = Template.Value;
=======
            LyricContainer.Template = Template.Value;
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
            UpdateDrawable();
        }

        protected virtual void OnSingerTemplateChange()
        {
            UpdateValue();
        }

        protected virtual void OnTranslateCodeChange()
        {
            UpdateValue();
        }

        /// <summary>
        ///     update view
        /// </summary>
        protected virtual void UpdateDrawable()
        {
            UpdateText();

            UpdateValue();
        }

        protected virtual void UpdateText()
        {
            //TODO : fix logic
            if (Style != null && Template != null && Lyric != null)
            {
<<<<<<< HEAD
                Width = LeftSideText?.LyricText?.GetTextEndPosition() ?? 200;
                Height = Lyric.Height ?? 100;
=======
                //Width = LeftSideText?.LyricText?.GetTextEndPosition() ?? 200;
                //Height = Lyric.Height ?? 100;
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
            }
        }

        protected virtual void UpdateValue()
        {
            var styleValue = Style.Value;
            var templateValue = Template.Value;

            //translate
            if (styleValue != null)
            {
                if (styleValue.ShowTranslate) //show translate
                    TranslateText.TextObject = templateValue?.TranslateText + Lyric.Translates.Where(x => x.Key == TranslateCode).FirstOrDefault().Value;
                else
                    TranslateText.TextObject = templateValue?.TranslateText;

                TranslateText.Colour = templateValue?.TranslateTextColor ?? Color4.White;
            }

            //Color
            var textColor = Singer?.LytricColor ?? Color4.Blue;
            var backgroundColor = Singer?.LytricBackgroundColor ?? Color4.White;
            //
<<<<<<< HEAD
            LeftSideText.SetColor(textColor);
            RightSideText.SetColor(backgroundColor);
=======
            //LyricContainer
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db

            Scale = new Vector2(templateValue?.Scale ?? 1);

            //update progress
            Progress = Progress;
        }

<<<<<<< HEAD
        
=======

>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
        protected override void Update()
        {
            base.Update();


            if (!ProgressUpdateByTime)
                return;

            var currentRelativeTime = Time.Current - HitObject.StartTime;
            if (HitObject.IsInTime(currentRelativeTime))
            {
<<<<<<< HEAD
                //get range progress point
                var startPoint = HitObject.TimeLines.GetFirstProgressPointByTime(currentRelativeTime);
                var endPoint = HitObject.TimeLines.GetLastProgressPointByTime(currentRelativeTime);
=======
                //TODO : get progress point
                /*
                var startProgressPoint = HitObject.TimeLines.GetFirstProgressPointByTime(currentRelativeTime);
                var endProgressPoint = HitObject.TimeLines.GetLastProgressPointByTime(currentRelativeTime);

                var startPosition = LeftSideText.LyricText.GetEndPositionByIndex(startProgressPoint.Key.Index);
                var endPosition = LeftSideText.LyricText.GetEndPositionByIndex(endProgressPoint?.Key.Index ?? -1);
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db

                if (startPoint == null)
                    startPoint = new KeyValuePair<int, LyricTimeLine>(0,new LyricTimeLine());

                if (endPoint == null)
                    return;

                //get position
                var startPosition = LeftSideText.LyricText.GetStartPositionByIndex(startPoint.Value.Key);
                var endPosition = LeftSideText.LyricText.GetStartPositionByIndex(endPoint.Value.Key);

                //duration
                var relativeTime = currentRelativeTime - HitObject.TimeLines.GetFirstProgressDuration(startPoint.Value.Key);

                if (startPosition == endPosition)
                    return;

                //Update progress
<<<<<<< HEAD
                Progress = startPosition + (endPosition - startPosition) / (float)(endPoint.Value.Value.Duration) * (float)relativeTime;
=======
                Progress = startPosition + (endPosition - startPosition) / (float)(endProgressPoint?.Value.RelativeTime - startProgressPoint.Value.RelativeTime) * (float)relativeTime;
                */

                LyricContainer.RelativeTime = currentRelativeTime;
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db

                Show();
                Alpha = 1;

                
            }
            else
            {
                Hide();
                Alpha = 0;
            }
        }
        


        protected sealed override void UpdateState(ArmedState state)
        {
            var transformTime = HitObject.StartTime - PreemptiveTime;

            ApplyTransformsAt(transformTime, true);
            ClearTransformsAfter(transformTime, true);

            using (BeginAbsoluteSequence(transformTime, true))
            {
                UpdatePreemptState();

                using (BeginDelayedSequence(PreemptiveTime + (Judgements.FirstOrDefault()?.TimeOffset ?? 0), true))
                {
                    UpdateCurrentState(state);
                }
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

        [BackgroundDependencyLoader]
        private void load(FontStore store)
        {
            Style.TriggerChange();
            Template.TriggerChange();
        }

        #region Bindable

        /// <summary>
        ///     Style
        /// </summary>
        public BindableObject<KaraokeLyricConfig> Style { get; set; } = new BindableObject<KaraokeLyricConfig>(new KaraokeLyricConfig());

        /// <summary>
        ///     Template
        /// </summary>
        public BindableObject<LyricTemplate> Template { get; set; } = new BindableObject<LyricTemplate>(new LyricTemplate());

        /// <summary>
        ///     SingerTemplate
        /// </summary>
        public BindableObject<SingerTemplate> SingerTemplate { get; set; } = new BindableObject<SingerTemplate>(new SingerTemplate());

        /// <summary>
        ///     Lang
        /// </summary>
        public Bindable<TranslateCode> TranslateCode { get; set; } = new Bindable<TranslateCode>();

        #endregion

        #region Field

        //Private
        private KaraokeLyricConfig _config;

        private Singer _singer;
        private TranslateCode _translateCode;


        /// <summary>
        ///     Gets the karaoke object.
        /// </summary>
        /// <value>The karaoke object.</value>
        public BaseLyric Lyric => HitObject;


        /// <summary>
        ///     Gets or sets the singer.
        /// </summary>
        /// <value>The singer.</value>
        public Singer Singer
        {
            get => _singer;
            set
            {
                _singer = value;
                UpdateDrawable();
            }
        }

        /// <summary>
        ///     Gets or sets the preemptive time.
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

        public TranslateString TranslateText { get; set; } = new TranslateString(null);

        #endregion
    }
}
