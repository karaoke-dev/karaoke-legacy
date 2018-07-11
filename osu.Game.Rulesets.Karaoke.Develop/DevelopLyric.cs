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
        }
    }


    public class DrawableLyric : CustomizableTextContainer
    {
        public BaseLyric Lyric { get; set; }


    }

    /// <summary>
    /// Contains
    /// 1. sub text(like Furigana)
    /// 2. main text(Lyric)
    /// 3. romaji
    /// </summary>
    public class PartialLyric : FillFlowContainer<MaskText>
    {
        
    }

    /// <summary>
    /// Contains : 
    /// 1. mask
    /// 2. front text
    /// 2. back text
    /// </summary>
    public class MaskText : FillFlowContainer
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
