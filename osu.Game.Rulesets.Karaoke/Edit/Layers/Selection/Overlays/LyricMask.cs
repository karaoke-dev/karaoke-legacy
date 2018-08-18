// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    /// <summary>
    ///     Drawable BaseLyric Mask
    /// </summary>
    public class LyricMask : HitObjectMask
    {
        private readonly LyricContainer lyricContainer;

        public LyricMask(DrawableEditableKaraokeObject drawableLyric)
            : base(drawableLyric)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            Position = drawableLyric.Position;

            Scale = new Vector2(drawableLyric.Scale.X * 0.8f, drawableLyric.Scale.Y * 0.8f);

            InternalChildren = new Drawable[]
            {
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        lyricContainer = new LyricContainer
                        {
                            AutoSizeAxes = Axes.Y,
                            RelativeSizeAxes = Axes.X,
                        },
                    }
                }
            };

            lyricContainer.Lyric = drawableLyric.Lyric;
            lyricContainer.Template = drawableLyric.Template;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Colour = colours.Yellow;
        }

        protected override void Update()
        {
            base.Update();
            var rowPosition = Parent.ToLocalSpace(HitObject.ScreenSpaceDrawQuad.TopLeft);
            Position = new Vector2(rowPosition.X, rowPosition.Y - 200);
            Size = HitObject.DrawSize;
        }

        public class LyricContainer : FillFlowContainer<PartialLyric>
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

                        Add(new PartialLyric()
                        {
                            TopText = furigana?.Text ?? " ",
                            MainText = lyricValue.Text,
                            BottomText = romaji?.Text ?? " ",
                            Origin = Anchor.TopLeft,
                            Anchor = Anchor.TopLeft,
                        });
                    }
                }
            }

            private double _relativeTime;

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

        public class PartialLyric : FillFlowContainer
        {
            public Color4 AccentColour
            {
                set
                {
                    _topText.AccentColour = value;
                    _mainText.AccentColour = value;
                    _bottomText.AccentColour = value;
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

                    //_topText.Height = _template?.TopText?.FontSize ?? 30;
                    //_mainText.Height = _template?.MainText?.FontSize ?? 100;
                    //_bottomText.Height = _template?.BottomText?.FontSize ?? 140;

                    _topToMainBorderContainer.Height = 10; //TODO : real value
                    _mainToBottomBorderContainer.Height = 10; //TODO : real value
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
                        Origin = Anchor.TopCentre,
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
                if (Template == null)
                    Template = new LyricTemplate();
            }
        }

        public class MaskText : SpriteText , IHasAccentColour
        {
            public Color4 AccentColour
            {
                get => accentColour;
                set
                {
                    if (accentColour == value)
                        return;
                    accentColour = value;

                    updateGlow();
                }
            }

            private Color4 accentColour;

            protected override void LoadComplete()
            {
                base.LoadComplete();
                updateGlow();
            }

            private void updateGlow()
            {
                if (!IsLoaded)
                    return;

                return;
                WithEffect(new OutlineEffect
                {
                    BlurSigma = new Vector2(3f),
                    Strength = 3f,
                    Colour = AccentColour,
                    PadExtent = true,
                });
            }
        }
    }
}
