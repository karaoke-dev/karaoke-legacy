using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using osu.Framework.Allocation;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Develop
{
    [TestFixture]
    public class DevelopLyric : OsuTestCase
    {
        public DevelopLyric()
        {
            /*
            var drawableLuyric = new DrawableLyric
            {
                Lyric = DemoKaraokeObject.GenerateWithStartAndDuration(1000, 3000)
            };

            Add(drawableLuyric);
            */

             var drawableMasktext = new PartialLyric
            {
                TopText = "Hello",
                MainText = "Hello",
                BottomText = "Hello",
                Progress = 0.6f,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                FrontTextColor = Color4.Blue
            };

            Add(drawableMasktext);

            /*
             var drawableMasktext = new MaskText
            {
                Text = "Hello",
                Progress = 0.6f,
                TextSize = 50,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                FrontTextColor = Color4.Blue
            };

            Add(drawableMasktext);
             */
           
        }
    }


    public class DrawableLyric : FillFlowContainer
    {
        public BaseLyric Lyric { get; set; }

        public DrawableLyric()
        {

        }
    }

    /// <summary>
    /// Contains
    /// 1. sub text(like Furigana)
    /// 2. main text(Lyric)
    /// 3. romaji
    /// </summary>
    internal class PartialLyric : FillFlowContainer
    {
        public float Progress
        {
            set
            {
                _topText.Progress = value;
                _mainText.Progress = value;
                _bottomText.Progress = value;
            }
        }

        public Color4 FrontTextColor
        {
            set
            {
                _topText.FrontTextColor = value;
                _mainText.FrontTextColor = value;
                _bottomText.FrontTextColor = value;
            }
        }

        public Color4 BackTextColor
        {
            set
            {
                _topText.BackTextColor = value;
                _mainText.BackTextColor = value;
                _bottomText.BackTextColor = value;
            }
        }

        public string TopText
        {
            get => _topText.Text;
            set => _topText.Text = value;
        }

        public string MainText
        {
            get => _mainText.Text;
            set => _mainText.Text = value;
        }

        public string BottomText
        {
            get => _bottomText.Text;
            set => _bottomText.Text = value;
        }

        private LyricTemplate _template;
        public LyricTemplate Template
        {
            get => _template;
            set
            {
                _template = value;
                _topText.TextSize = _template?.TopText?.FontSize ?? 20;
                _mainText.TextSize = _template?.MainText?.FontSize ?? 50;
                _bottomText.TextSize = _template?.BottomText?.FontSize ?? 20;
                _topToMainBorderContainer.Height = 10;//TODO : real value
                _mainToBottomBorderContainer.Height = 10;//TODO : real value
            }
        }

        private readonly MaskText _topText;

        private readonly Container _topToMainBorderContainer;

        private readonly MaskText _mainText;

        private readonly Container _mainToBottomBorderContainer;

        private readonly MaskText _bottomText;

        public PartialLyric()
        {
            AutoSizeAxes = Axes.Both;
            Direction = FillDirection.Vertical;
            Spacing = new Vector2(-10);
            Children = new Drawable[]
            {
                _topText = new MaskText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                },
                _topToMainBorderContainer = new Container(),
                _mainText = new MaskText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                },
                _mainToBottomBorderContainer = new Container(),
                _bottomText = new MaskText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            //use default template if null
            if(Template == null)
                Template = new LyricTemplate();
        }
    }

    /// <summary>
    /// Contains : 
    /// 1. mask
    /// 2. front text
    /// 2. back text
    /// </summary>
    internal class MaskText : FillFlowContainer
    {
        private float _progress;
        public float Progress 
        { 
            get => _progress;
            set
            {
                _progress = value;
                UpdateProgress();
            } 
        }

        private string _text;
        public string Text 
        { 
            get => _text;
            set
            {
                _text = value;
                _frontText.Text = _text;
                _backtext.Text = _text;
                UpdateProgress();
            } 
        }

        private float _testSize;
        public float TextSize 
        { 
            get => _testSize;
            set
            {
                _testSize = value;
                _frontText.TextSize = _testSize;
                _backtext.TextSize = _testSize;
            } 
        }

        public Color4 FrontTextColor
        {
            get=> _frontText.Colour;
            set => _frontText.Colour = value;
        }

        public Color4 BackTextColor
        {
            get=> _backtext.Colour;
            set => _backtext.Colour = value;
        }

        private readonly Container _leftMask;
        private readonly Container _rightMask;
        private readonly SpriteText _frontText;
        private readonly SpriteText _backtext;

        public MaskText()
        {
            AutoSizeAxes = Axes.Both;
            Direction = FillDirection.Horizontal;
            Spacing = new Vector2(0);
            this.Children = new Drawable[]
            {
                _leftMask = new Container
                {
                    Masking = true,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        _frontText = new SpriteText
                        {
                            
                        }
                    }
                    
                },
                _rightMask = new Container
                {
                    Masking = true,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        _backtext = new SpriteText
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                        }
                    }
                }
            };
            UpdateProgress();
        }

        protected void UpdateProgress()
        {
            var witdh = _frontText.Width;
            _leftMask.Width = witdh * Progress;
            _rightMask.Width = witdh * (1 - Progress);
        }

        private bool updated;
        protected override void UpdateAfterAutoSize()
        {
            if (!updated)
            {
                updated = true;
                UpdateProgress();
                base.UpdateAfterAutoSize();
            }
        }
    }
}
