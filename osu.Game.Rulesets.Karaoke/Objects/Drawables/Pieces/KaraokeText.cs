using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.IO.Stores;

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

    public class MainKaraokeText : KaraokeText
    {
        protected FontStore FontStore = null;

        public override TextObject TextObject
        {
            get => base.TextObject;
            set
            {
                base.TextObject = value;
                //update each text's end position
                UpdateSingleCharacterEndPosition();
            }
        }

        public MainKaraokeText(TextObject textObject) : base(textObject)
        {

        }

        protected void UpdateSingleCharacterEndPosition()
        {
            if (FontStore == null)
                return;

            if (TextObject?.Text != null)
            {
                float totalWidth = 0;
                ListCharEndPosition.Clear();
                foreach (var single in TextObject.Text)
                {
                    //get single char width
                    var singleCharWhdth = CreateCharacterDrawable(single).Width * TextSize;
                    totalWidth += singleCharWhdth;
                    ListCharEndPosition.Add(totalWidth);
                }
            }
        }

        public float GetEndPositionByIndex(int index)
        {
            try
            {
                if (index < 0)
                    return 0;

                return ListCharEndPosition[index];
            }
            catch
            {
                return ListCharEndPosition.Last();
            }
        }

        public int GetIndexByPosition(float position)
        {
            return ListCharEndPosition.FindIndex(x => x > position);
        }

        [BackgroundDependencyLoader]
        private void load(FontStore store)
        {
            FontStore = store;
            UpdateSingleCharacterEndPosition();
        }
    }
}
