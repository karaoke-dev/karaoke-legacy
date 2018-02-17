// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces
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
                Text = _textObject.Text;
                Position = _textObject.Position;
                if (_textObject.FontSize != null)
                    TextSize = _textObject.FontSize.Value;
            }
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
