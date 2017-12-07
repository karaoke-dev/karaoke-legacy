using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.IO.Stores;
using osu.Framework.Input;

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
