using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces
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

        public virtual void AddMainText(FormattedText formattedText, MainTextList textObject)
        {
            if (MainKaraokeText == null)
            {
                MainKaraokeText = new MainKaraokeText(formattedText,textObject);
                Add(MainKaraokeText);
            }
            else
            {
                MainKaraokeText.TextObject = formattedText;
                MainKaraokeText.MainTextObject = textObject;
            }
        }

        public void AddSubText(FormattedText textObject)
        {
            var subText = new KaraokeText(textObject)
            {
                Origin = Anchor.BottomCentre,
            };
            ListDrawableSubText.Add(subText);
            Add(subText);
        }

        public void AddBottomText(FormattedText textObject)
        {
            var bottomText = new KaraokeText(textObject)
            {
                Origin = Anchor.BottomCentre,
            };
            ListDrawableBottomText.Add(bottomText);
            Add(bottomText);
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
