// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.IO.Stores;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Extension;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric
{
    /// <summary>
    /// Karaoke Text
    /// </summary>
    public class DrawableLyricObject : DrawableHitObject<Objects.Lyric>, IAmDrawableLyricObject
    {
        //Const
        public const float TIME_FADEIN = 100;
        public const float TIME_FADEOUT = 100;


        private double _nowProgress;

        #region Field

        //Private
        private KaraokeLyricConfig _config;
        private LyricTemplate _template;
        private Singer _singer;
        private TranslateCode _translateCode;


        /// <summary>
        /// Gets the karaoke object.
        /// </summary>
        /// <value>The karaoke object.</value>
        public Objects.Lyric Lyric => HitObject;

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        public KaraokeLyricConfig Config
        {
            get => _config ?? (_config = new KaraokeLyricConfig());
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
        public LyricTemplate Template
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
        /// <see cref="T:osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.DrawableKaraokeObject"/> progress update by time.
        /// </summary>
        /// <value><c>true</c> if progress update by time; otherwise, <c>false</c>.</value>
        public virtual bool ProgressUpdateByTime { get; set; } = true;

        //Drawable
        public TextsAndMask TextsAndMaskPiece { get; set; } = new TextsAndMask();
        public KaraokeText TranslateText { get; set; } = new KaraokeText(null);


        public DrawableLyricObject(Objects.Lyric hitObject)
            : base(hitObject)
        {
            Alpha = 0;

            Template = new LyricTemplate();
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

            if (Config != null)
            {
                string mainTextDelimiter = "";
                Dictionary<int, TextComponent> mainText = null;
                Dictionary<int, FormattedText> bottomTexts = null;
                Dictionary<int, FormattedText> subTexts = null;

                if (Config.RomajiFirst)
                {
                    mainText = Lyric.RomajiTextListRomajiTexts.ToDictionary(k => k.Key, v => (TextComponent) v.Value );
                    mainTextDelimiter = " ";
                }
                else
                {
                    mainText = Lyric.MainText.ToDictionary(k => k.Key, v => (TextComponent)v.Value);
                }
                //main text
                TextsAndMaskPiece.AddMainText(Template?.MainText, mainText.ToDictionary(k => k.Key, v => (TextComponent)v.Value), mainTextDelimiter);

                //show romaji text
                if (Config.RomajiVislbility)
                {
                    if (Config.RomajiFirst)
                    {
                        bottomTexts = Lyric.MainText.ToDictionary(k => k.Key, v => (Template?.BottomText + v.Value + new FormattedText()
                        {
                            X = getxPosition(v.Key),
                        }));
                    }
                    else
                    {
                        bottomTexts = Lyric.RomajiTextListRomajiTexts.ToDictionary(k => k.Key, v => (Template?.BottomText + v.Value + new FormattedText()
                        {
                            X = getxPosition(v.Key),
                        }));
                    }
                }

                //show subtext
                if (Config.SubTextVislbility)
                {
                    subTexts = Lyric.SubTexts.ToDictionary(k=> k.Key, v => (Template?.TopText + v.Value + new FormattedText()
                    {
                        X = getxPosition(v.Key),
                    }));
                }

                //show sub text
                TextsAndMaskPiece.AddSubText(subTexts?.Select(x => x.Value).ToList());

                //show sub text
                TextsAndMaskPiece.AddBottomText(bottomTexts?.Select(x => x.Value).ToList());

                Width = TextsAndMaskPiece.MainText.GetTextEndPosition();
                Height = Lyric.Height ?? 100;

                UpdateValue();
            }

            float getxPosition(int index)
            {
                var startPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(index - 1);
                var endPosition = TextsAndMaskPiece.MainText.GetEndPositionByIndex(index);

                var positionX = (startPosition + endPosition) / 2;

                return positionX;
            }
        }

        protected virtual void UpdateValue()
        {
            //Color
            Color4 textColor = Singer?.LytricColor ?? Color4.Blue;
            Color4 backgroundColor = Singer?.LytricBackgroundColor ?? Color4.White;
            TextsAndMaskPiece.SetColor(textColor, backgroundColor);

            //translate text
            TranslateText.TextObject = Template?.TranslateText + Lyric.Translates.Where(x => x.LangCode == LangTagConvertor.GetCode(TranslateCode)).FirstOrDefault();
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
