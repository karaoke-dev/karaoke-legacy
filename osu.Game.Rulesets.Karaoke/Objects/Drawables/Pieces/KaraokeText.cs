// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces
{
    public class KaraokeText : OsuSpriteText
    {
        private TextObject _textObject;

        public List<float> ListCharEndPosition { get; protected set; } = new List<float>();


        public virtual TextObject TextObject
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

        public KaraokeText(TextObject textObject)
        {
            TextObject = textObject;

            UseFullGlyphHeight = false;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            Alpha = 1;
        }
    }
}
