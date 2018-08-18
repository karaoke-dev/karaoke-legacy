// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    /// <summary>
    ///     Drawable BaseLyric Mask
    /// </summary>
    public class LyricMask : HitObjectMask
    {
        private readonly MaskLyricContainer lyricContainer;

        public LyricMask(DrawableEditableKaraokeObject drawableLyric)
            : base(drawableLyric)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
            Scale = new Vector2(drawableLyric.Scale.X * 0.8f, drawableLyric.Scale.Y * 0.8f);

            InternalChildren = new Drawable[]
            {
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        lyricContainer = new MaskLyricContainer
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
            Position = new Vector2(rowPosition.X, rowPosition.Y - 221);
            Size = HitObject.DrawSize;
        }

        public class MaskLyricContainer : LyricContainer<PartialLyric<MaskText>>
        {
            
        }

        public class MaskText : Container, IMaskText
        {
            public float Progress { get; set; }

            public string Text
            {
                get => "";
                set
                {
                    if (Children.FirstOrDefault() is SpriteText spriteText)
                    {
                        spriteText.Text = value;
                    }
                }
            }

            public Color4 FrontTextColor { get; set; }

            public Color4 BackTextColor { get; set; }

            public float TextSize
            {
                get => 0;
                set
                {
                    if (Children.FirstOrDefault() is SpriteText spriteText)
                    {
                        spriteText.TextSize = value;
                    }
                }
            }

            public MaskText()
            {
                AutoSizeAxes = Axes.X;
                Children = new Drawable[]
                {
                    new SpriteText
                    {
                        Text = "Mask",
                        Alpha = 0.5f,
                    }
                };
            }
        }
    }
}
