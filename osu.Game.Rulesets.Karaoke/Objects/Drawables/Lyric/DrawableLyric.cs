// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.IO.Stores;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Types;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using osu.Game.Rulesets.Karaoke.Objects.Text;
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
        public const float TIME_FADEIN = 100;

        public const float TIME_FADEOUT = 100;

        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="T:osu.Game.Rulesets.Karaoke.Objects.Drawables.BaseLyric.DrawableKaraokeObject" /> progress update by
        ///     time.
        /// </summary>
        /// <value><c>true</c> if progress update by time; otherwise, <c>false</c>.</value>
        public virtual bool ProgressUpdateByTime { get; set; } = true;

        //Drawable
        public LyricContainer TextsAndMaskPiece { get; set; } = new LyricContainer();

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
                TextsAndMaskPiece.MovingMask((float)_nowProgress);
            }
        }

        private double _nowProgress;

        public DrawableLyric(BaseLyric hitObject)
            : base(hitObject)
        {
            Alpha = 0;
            InternalChildren = new Drawable[]
            {
                TextsAndMaskPiece,
                TranslateText
            };

            Style.ValueChanged += style => { OnStyleChange(); };
            Template.ValueChanged += style => { OnTemplateChange(); };
            SingerTemplate.ValueChanged += style => { OnSingerTemplateChange(); };
            TranslateCode.ValueChanged += style => { OnTranslateCodeChange(); };
        }

        protected virtual void OnStyleChange()
        {
            UpdateDrawable();
        }

        protected virtual void OnTemplateChange()
        {
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
            TextsAndMaskPiece.ClearAllText();

            var styleValue = Style.Value;
            var templateValue = Template.Value;

            if (styleValue != null)
            {
                var mainTextDelimiter = "";
                Dictionary<int, TextComponent> mainText = null;
                Dictionary<int, FormattedText> bottomTexts = null;
                Dictionary<int, FormattedText> subTexts = null;

                if (styleValue.RomajiFirst)
                {
                    if (Lyric is IHasRomaji romajiLyric)
                    {
                        mainText = romajiLyric.Romaji.ToDictionary(k => k.Key, v => (TextComponent)v.Value);
                        mainTextDelimiter = " ";
                    }
                }
                else
                {
                    mainText = Lyric.Lyric.ToDictionary(k => k.Key, v => (TextComponent)v.Value);
                }

                //main text
                TextsAndMaskPiece.AddMainText(templateValue?.MainText, mainText.ToDictionary(k => k.Key, v => v.Value), mainTextDelimiter);

                //show romaji text
                if (styleValue.RomajiVislbility)
                    if (styleValue.RomajiFirst)
                    {
                        bottomTexts = Lyric.Lyric.ToDictionary(k => k.Key, v => templateValue?.BottomText + v.Value + new FormattedText
                        {
                            X = getxPosition(v.Key)
                        });
                    }
                    else
                    {
                        if (Lyric is IHasRomaji romajiLyric)
                            bottomTexts = romajiLyric.Romaji.ToDictionary(k => k.Key, v => templateValue?.BottomText + v.Value + new FormattedText
                            {
                                X = getxPosition(v.Key)
                            });
                    }

                //show subtext
                if (styleValue.SubTextVislbility)
                    if (Lyric is IHasFurigana furiganaLyric)
                        subTexts = furiganaLyric.Furigana.ToDictionary(k => k.Key, v => templateValue?.TopText + v.Value + new FormattedText
                        {
                            X = getxPosition(v.Key)
                        });

                //show sub text
                TextsAndMaskPiece.AddSubText(subTexts?.Select(x => x.Value).ToList());

                //show sub text
                TextsAndMaskPiece.AddBottomText(bottomTexts?.Select(x => x.Value).ToList());

                Width = TextsAndMaskPiece.MainText.GetTextEndPosition();
                Height = Lyric.Height ?? 100;
            }

            float getxPosition(int index)
            {
                var positionX = TextsAndMaskPiece.MainText.GetTextCenterPosition(index);

                return positionX;
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
            TextsAndMaskPiece.SetColor(textColor, backgroundColor);

            Scale = new Vector2(templateValue?.Scale ?? 1);

            //update progress
            Progress = Progress;
        }

        protected override void Update()
        {
            base.Update();

            if (!ProgressUpdateByTime)
                return;

            var currentRelativeTime = Time.Current - HitObject.StartTime;
            if (HitObject.IsInTime(currentRelativeTime))
            {
                //TODO : get progress point
                var startProgressPoint = HitObject.TimeLines.GetFirstProgressPointByTime(currentRelativeTime);
                var endProgressPoint = HitObject.TimeLines.GetLastProgressPointByTime(currentRelativeTime);

                var startPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(startProgressPoint.Key);
                var endPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(endProgressPoint?.Key ?? -1);

                var relativeTime = currentRelativeTime - startProgressPoint.Value.RelativeTime;

                if (endProgressPoint?.Value == null)
                    return;

                if (startPosition == endPosition)
                    return;

                //Update progress
                Progress = startPosition + (endPosition - startPosition) / (float)(endProgressPoint?.Value.RelativeTime - startProgressPoint.Value.RelativeTime) * (float)relativeTime;

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
            UpdateDrawable();
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
