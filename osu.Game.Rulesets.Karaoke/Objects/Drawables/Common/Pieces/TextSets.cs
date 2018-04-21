// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces
{
    /// <summary>
    /// this class contains 
    /// 1. top text
    /// 2. main text
    /// 3. bottom text
    /// </summary>
    public class TextSets : Container
    {
        /// <summary>
        /// main text 
        /// </summary>
        public MainKaraokeText MainKaraokeText;

        /// <summary>
        /// top text
        /// </summary>
        public List<KaraokeText> ListDrawableSubText = new List<KaraokeText>();

        /// <summary>
        /// bottom text
        /// </summary>
        public List<KaraokeText> ListDrawableBottomText = new List<KaraokeText>();

        private float _height;

        public TextSets()
        {
            Masking = true;
        }

        public virtual void AddMainText(FormattedText formattedText, Dictionary<int, TextComponent> textObject, string delimiter)
        {
            if (MainKaraokeText == null)
            {
                MainKaraokeText = new MainKaraokeText(formattedText, textObject, delimiter);
                Add(MainKaraokeText);
            }
            else
            {
                MainKaraokeText.TextObject = formattedText;
                MainKaraokeText.MainTextObject = textObject;
            }
        }

        public void AddSubText(List<FormattedText> textObjectsList)
        {
            if (textObjectsList == null)
                textObjectsList = new List<FormattedText>()
                {
                    new FormattedText(),
                };

            foreach (var textObject in textObjectsList)
            {
                var subText = new KaraokeText(textObject)
                {
                    Origin = Anchor.BottomCentre,
                };
                Add(subText);
                ListDrawableSubText.Add(subText);
            }
        }

        public void AddBottomText(List<FormattedText> textObjectsList)
        {
            if (textObjectsList == null)
                textObjectsList = new List<FormattedText>()
                {
                    new FormattedText(),
                };

            foreach (var textObject in textObjectsList)
            {
                var subText = new KaraokeText(textObject)
                {
                    Origin = Anchor.BottomCentre,
                };
                Add(subText);
                ListDrawableBottomText.Add(subText);
            }
        }

        public void ClearAllText()
        {
            ListDrawableSubText.Clear();
            ListDrawableBottomText.Clear();
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

            if (startPositionX != 0)
            {
                foreach (var singleText in Children)
                {
                    if (singleText is KaraokeText terxtObject)
                    {
                        terxtObject.Position = terxtObject.TextObject.Position - Position;
                    }
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
