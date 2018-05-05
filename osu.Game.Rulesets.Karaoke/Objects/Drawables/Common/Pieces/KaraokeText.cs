// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Objects.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces
{
    public class KaraokeText : OsuSpriteText
    {
        private FormattedText _textObject;

        public virtual FormattedText TextObject
        {
            get => _textObject;
            set
            {
                _textObject = value;
                if (_textObject == null)
                    return;
                //set text
                UpdateText();

                //update position and size
                Position = _textObject.Position;
                TextSize = _textObject.FontSize ?? 18;
            }
        }

        protected virtual void UpdateText()
        {
            Text = TextObject.Text;
        }

        public KaraokeText(FormattedText textObject)
        {
            TextObject = textObject;
            UseFullGlyphHeight = false;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.BottomLeft;
            Alpha = 1;
        }
    }
}
