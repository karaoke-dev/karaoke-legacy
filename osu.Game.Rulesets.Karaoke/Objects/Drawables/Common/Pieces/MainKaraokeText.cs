// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.IO.Stores;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces
{
    public class MainKaraokeText : KaraokeText
    {
        private MainTextList _mainTextObject;

        protected FontStore FontStore = null;

        public float TotalWidth { get; protected set; } = 0;

        public List<float> ListCharEndPosition { get; protected set; } = new List<float>();

        public MainTextList MainTextObject
        {
            get => _mainTextObject;
            set
            {
                _mainTextObject = value;
                //set text
                UpdateText();

                Position = TextObject.Position;

                //update each text's end position
                UpdateSingleCharacterEndPosition();
            }
        }

        protected override void UpdateText()
        {
            Text = MainTextObject?.Text;
        }

        public MainKaraokeText(FormattedText formattedText,MainTextList textObject) : base(formattedText)
        {
            MainTextObject = textObject;
        }

        protected void UpdateSingleCharacterEndPosition()
        {
            if (FontStore == null)
                return;

            if (MainTextObject != null && MainTextObject.Count > 0)
            {
                ListCharEndPosition.Clear();
                TotalWidth = 0;
                foreach (var single in MainTextObject)
                {
                    //get single char width
                    var singleCharWhdth = GetStringWidth(single.Text);
                    TotalWidth += singleCharWhdth;
                    ListCharEndPosition.Add(TotalWidth);
                }
            }
        }

        protected float GetStringWidth(string str)
        {
            float totalWidth = 0;
            foreach (var single in str)
            {
                //get single char width
                var singleCharWhdth = CreateCharacterDrawable(single).Width * TextSize;
                totalWidth += singleCharWhdth;
                
            }
            return totalWidth;
        }

        public float GetEndPositionByIndex(int index)
        {
            try
            {
                if (ListCharEndPosition.Count == 0)
                    return 0;

                if (index < 0)
                    return 0;

                return ListCharEndPosition[index];
            }
            catch
            {
                return GetTextEndPosition();
            }
        }

        public float GetTextEndPosition()
        {
            try
            {
                if (ListCharEndPosition.Count == 0)
                    return 700;

                return ListCharEndPosition.Last();
            }
            catch
            {
                //if private void load(FontStore store) not loaded,will run in here
                return 700;
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
