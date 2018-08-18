// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces
{
    public class LyricContainer : LyricContainer<PartialLyric>
    {

    }

    public class LyricContainer<T> : FillFlowContainer<T> where T : Drawable, IPartialLyric, new() 
    {
        private LyricTemplate _template = new LyricTemplate();

        public LyricTemplate Template
        {
            get => _template;
            set
            {
                _template = value;
                foreach (var parcialLyric in Children)
                {
                    parcialLyric.Template = _template;
                }
            }
        }

        private BaseLyric _lyric;

        public BaseLyric Lyric
        {
            get => _lyric;
            set
            {
                _lyric = value;
                Clear();
                foreach (var single in Lyric.Lyric)
                {
                    var key = single.Key;
                    var lyricValue = single.Value;

                    ((IHasFurigana)Lyric).Furigana.TryGetValue(key, out var furigana);
                    ((IHasRomaji)Lyric).Romaji.TryGetValue(key, out var romaji);

                    Add(new T
                    {
                        TopText = furigana?.Text ?? " ",
                        MainText = lyricValue.Text,
                        BottomText = romaji?.Text ?? " ",
                        Origin = Anchor.TopLeft,
                        Anchor = Anchor.TopLeft,
                        FrontTextColor = Color4.Blue,
                        Index = key,
                    });
                }
            }
        }

        private double _relativeTime;

        public double RelativeTime
        {
            get => _relativeTime;
            set
            {
                _relativeTime = value;
                var startProgressPoint = Lyric.TimeLines.GetFirstProgressPointByTime(_relativeTime);
                var endProgressPoint = Lyric.TimeLines.GetLastProgressPointByTime(_relativeTime);

                foreach (var partialLyric in Children)
                {
                    if (partialLyric.Index <= startProgressPoint.Key.Index)
                    {
                        partialLyric.Progress = 1;
                    }
                    else if (partialLyric.Index > endProgressPoint?.Key.Index)
                    {
                        partialLyric.Progress = 0;
                    }
                    else
                    {
                        var startPercentage = startProgressPoint.Key.Index != partialLyric.Index ? 0 : startProgressPoint.Key.Percentage;
                        var endPercentage = endProgressPoint?.Key.Percentage;

                        var startRelativeTime = startProgressPoint.Value.RelativeTime;
                        var endRelativeTime = endProgressPoint?.Value.RelativeTime;

                        var startTime = startProgressPoint.Value.RelativeTime;

                        var relativePercentage = (_relativeTime - startTime) / (endRelativeTime - startRelativeTime);
                        var percantage = startPercentage + relativePercentage / (endPercentage - startPercentage);

                        //TODO : cal
                        partialLyric.Progress = (float)percantage;
                    }
                }
            }
        }

        protected float NewLineXPosition { get; set; }

        protected override IEnumerable<Vector2> ComputeLayoutPositions()
        {
            var points = base.ComputeLayoutPositions().ToArray();
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].Y != points[0].Y)
                {
                    points[i].X = points[i].X + NewLineXPosition;
                }
            }

            return points;
        }

        public LyricContainer()
        {
            Direction = FillDirection.Full;
            NewLineXPosition = 50;
        }
    }

    public interface IPartialLyric
    {
        float Progress { set; }

        int Index { get; set; }

        Color4 FrontTextColor { set; }

        Color4 BackTextColor { set; }

        string TopText { get; set; }

        string MainText { get; set; }

        string BottomText { get; set; }

        LyricTemplate Template { get; set; }
    }

    /// <summary>
    /// Contains
    /// 1. sub text(like Furigana)
    /// 2. main text(Lyric)
    /// 3. romaji
    /// </summary>
    public class PartialLyric : PartialLyric<MaskText>
    {

    }

   
    public class PartialLyric<T> : FillFlowContainer , IPartialLyric where T : Drawable , IMaskText ,new()
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

        public int Index { get; set; }

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
                _topText.TextSize = _template?.TopText?.FontSize ?? 30;
                _mainText.TextSize = _template?.MainText?.FontSize ?? 80;
                _bottomText.TextSize = _template?.BottomText?.FontSize ?? 30;

                _topText.Height = _template?.TopText?.FontSize ?? 30;
                _mainText.Height = _template?.MainText?.FontSize ?? 100;
                _bottomText.Height = _template?.BottomText?.FontSize ?? 140;

                _topToMainBorderContainer.Height = 10; //TODO : real value
                _mainToBottomBorderContainer.Height = 10; //TODO : real value
            }
        }

        private readonly T _topText;

        private readonly Container _topToMainBorderContainer;

        private readonly T _mainText;

        private readonly Container _mainToBottomBorderContainer;

        private readonly T _bottomText;

        public PartialLyric()
        {
            AutoSizeAxes = Axes.Both;
            Direction = FillDirection.Vertical;
            Spacing = new Vector2(-10);
            Children = new Drawable[]
            {
                _topText = new T
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                _topToMainBorderContainer = new Container(),
                _mainText = new T
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                },
                _mainToBottomBorderContainer = new Container(),
                _bottomText = new T
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
            if (Template == null)
                Template = new LyricTemplate();
        }
    }

    public interface IMaskText
    {
        float Progress { get; set; }

        string Text { get; set; }

        Color4 FrontTextColor { get; set; }

        Color4 BackTextColor { get; set; }

        float TextSize { get; set; }
    }

    /// <summary>
    /// Contains : 
    /// 1. mask
    /// 2. front text
    /// 2. back text
    /// </summary>
    public class MaskText : FillFlowContainer , IMaskText
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
            get => _frontText.Colour;
            set => _frontText.Colour = value;
        }

        public Color4 BackTextColor
        {
            get => _backtext.Colour;
            set => _backtext.Colour = value;
        }

        private readonly Container _leftMask;
        private readonly Container _rightMask;
        private readonly SpriteText _frontText;
        private readonly SpriteText _backtext;

        public MaskText()
        {
            AutoSizeAxes = Axes.X;
            Direction = FillDirection.Horizontal;
            Spacing = new Vector2(0);
            Children = new Drawable[]
            {
                _leftMask = new Container
                {
                    Masking = true,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        _frontText = new SpriteText
                        {
                            UseFullGlyphHeight = false
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
                            UseFullGlyphHeight = false,
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
