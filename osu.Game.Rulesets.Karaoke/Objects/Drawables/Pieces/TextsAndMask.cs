// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

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

        public KaraokeText SubKaraokeText => RightSideText.ListDrawableSubText.FirstOrDefault();

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
            LeftSideText.AddText(textObject);
            RightSideText.AddText(textObject);
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
            private TextObject _mainText;
            private List<TextObject> _listText = new List<TextObject>();
            public List<KaraokeText> ListDrawableSubText = new List<KaraokeText>();
            public MainKaraokeText MainKaraokeText;
            private float _height;

            public virtual void AddMainText(TextObject textObject)
            {
                _mainText = textObject;
                UpdateChild();
            }

            public void AddText(TextObject textObject)
            {
                _listText.Add(textObject);
                UpdateChild();
            }

            public void RemoveText(TextObject textObject)
            {
                _listText.Remove(textObject);
                UpdateChild();
            }

            public void ClearAllText()
            {
                _listText.Clear();
                //_mainText = null;
                UpdateChild();
            }

            protected void UpdateChild()
            {
                ListDrawableSubText.Clear();
                List<KaraokeText> list = new List<KaraokeText>();
                if (_mainText != null)
                {
                    MainKaraokeText = new MainKaraokeText(_mainText);
                    list.Add(MainKaraokeText);
                }
                foreach (var singleText in _listText)
                {
                    ListDrawableSubText.Add(new KaraokeText(singleText)
                    {
                        //Origin = Anchor.Centre,
                    });
                }
                list.AddRange(ListDrawableSubText);
                Children = list.ToArray();
                Masking = true;
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
                        Children[i].Position = _mainText.Position - Position;
                    }
                    else
                    {
                        Children[i].Position = _listText[i - 1].Position - Position;
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
