// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces
{
    public class TextsAndMask : Container //BufferedContainer
    {
        protected virtual TextSets LeftSideText { get; set; } = new TextSets();

        protected virtual TextSets RightSideText { get; set; } = new TextSets();

        private float _maskWidth;

        private float _maskHeight;

        //MainText
        public MainKaraokeText MainText => RightSideText.MainKaraokeText;

        //TopText
        public KaraokeText SubText => SubTexts?.FirstOrDefault();
        public List<KaraokeText> SubTexts => RightSideText.ListDrawableSubText;

        //BottomText
        public KaraokeText BottomText => BottomTexts?.FirstOrDefault();
        public List<KaraokeText> BottomTexts => RightSideText.ListDrawableBottomText;


        public TextsAndMask()
        {
            Children = new Drawable[]
            {
                RightSideText,
                LeftSideText,
            };
        }

        public void AddMainText(FormattedText formattedText, Dictionary<int, TextComponent> textObject)
        {
            LeftSideText.AddMainText(formattedText, textObject);
            RightSideText.AddMainText(formattedText, textObject);
        }

        public void AddSubText(List<FormattedText> textObject)
        {
            LeftSideText.AddSubText(textObject);
            RightSideText.AddSubText(textObject);
        }

        public void AddBottomText(List<FormattedText> textObject)
        {
            LeftSideText.AddBottomText(textObject);
            RightSideText.AddBottomText(textObject);
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
    }
}
