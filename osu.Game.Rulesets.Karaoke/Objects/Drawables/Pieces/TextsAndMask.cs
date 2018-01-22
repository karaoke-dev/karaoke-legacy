// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces
{
    public class TextsAndMask : Container //BufferedContainer
    {
        protected virtual SingleSideOfAndMask LeftSideText { get; set; } = new SingleSideOfAndMask();

        protected virtual SingleSideOfAndMask RightSideText { get; set; } = new SingleSideOfAndMask();

        private float _maskWidth;

        private float _maskHeight;

        public MainKaraokeText MainKaraokeText => RightSideText.MainKaraokeText;

        public KaraokeText SubKaraokeText => SubKaraokeTexts?.FirstOrDefault();

        public List<KaraokeText> SubKaraokeTexts => RightSideText.ListDrawableSubText;

        public TextsAndMask()
        {
            //CacheDrawnFrameBuffer = true;
            //this.Attach(RenderbufferInternalFormat.DepthComponent16);
            //Width = 700;
            //Height = 100;

            Children = new Drawable[]
            {
                RightSideText,
                LeftSideText,
            };
        }

        public void AddMainText(TextObject textObject)
        {
            LeftSideText.AddMainText(textObject);
            RightSideText.AddMainText(textObject);
        }

        public void AddSubText(TextObject textObject)
        {
            LeftSideText.AddSubText(textObject);
            RightSideText.AddSubText(textObject);
        }

        public void ClearAllText()
        {
            LeftSideText.ClearAllText();
            RightSideText.ClearAllText();
        }

        public void SetWidth(float width)
        {
            _maskWidth = width;
        }

        public void SetHeight(float height)
        {
            _maskHeight = height;
            LeftSideText.SetHeight(_maskHeight);
            RightSideText.SetHeight(_maskHeight);
        }

        public void MovingMask(float newValue)
        {
            LeftSideText.SetMaskStartAndEndPosition(0, newValue);
            RightSideText.SetMaskStartAndEndPosition(newValue, _maskWidth);
        }

        public void SetColor(Color4 color, Color4 backgroundColor)
        {
            LeftSideText.SetColor(color);
            //Right side is white
            RightSideText.SetColor(Color4.White);
        }

        protected class SingleSideOfAndMask : Container
        {
            public List<KaraokeText> ListDrawableSubText = new List<KaraokeText>();
            public MainKaraokeText MainKaraokeText;
            private float _height;

            public SingleSideOfAndMask()
            {
                Masking = true;
            }

            public virtual void AddMainText(TextObject textObject)
            {
                if (MainKaraokeText == null)
                {
                    MainKaraokeText = new MainKaraokeText(textObject);
                    Add(MainKaraokeText);
                }
                else
                {
                    MainKaraokeText.TextObject = textObject;
                }
            }

            public void AddSubText(TextObject textObject)
            {
                var subText = new KaraokeText(textObject)
                {
                    Origin = Anchor.BottomCentre,
                };
                ListDrawableSubText.Add(subText);
                Add(subText);
            }

            public void ClearAllText()
            {
                ListDrawableSubText.Clear();
                MainKaraokeText = null;
                Children = new Drawable[] { };
            }

            public void SetHeight(float height)
            {
                _height = height;
                Height = _height;
            }

            public void SetMaskStartAndEndPosition(float startPositionX, float endPositionX)
            {
                Position = new Vector2(startPositionX, 0);

                for (int i = 0; i < Children.Count; i++)
                {
                    if (i == 0)
                    {
                        Children[i].Position = MainKaraokeText.TextObject.Position - Position;
                    }
                    else
                    {
                        Children[i].Position = ListDrawableSubText[i - 1].TextObject.Position - Position;
                    }
                }
                Width = endPositionX - startPositionX;
            }

            public void SetColor(Color4 color)
            {
                Colour = color;
            }
        }
    }
}
