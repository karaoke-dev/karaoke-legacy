// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.IO.Stores;
using osu.Game.Rulesets.Karaoke.Objects.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces
{
    public class MainKaraokeText : KaraokeText
    {
        private Dictionary<int, TextComponent> _mainTextObject;

        protected FontStore FontStore = null;

        public float TotalWidth { get; protected set; } = 0;

        public Dictionary<int, float> ListCharEndPosition { get; protected set; } = new Dictionary<int, float>();

        public Dictionary<int, TextComponent> MainTextObject
        {
            get => _mainTextObject;
            set
            {
                if (value == null)
                    return;

                _mainTextObject = value;
                //set text
                UpdateText();

                //update each text's end position
                UpdateSingleCharacterEndPosition();
            }
        }

        public string Delimiter { get; set; } = "";

        protected override void UpdateText()
        {
            Text = MainTextObject?.Select(i => i.Value.Text).Aggregate((i, j) => i + Delimiter + j);
        }

        public MainKaraokeText(FormattedText formattedText, Dictionary<int, TextComponent> textObject, string delimiter)
            : base(formattedText)
        {
            Delimiter = delimiter;
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
                    var singleCharWhdth = GetStringWidth(single.Value.Text);
                    TotalWidth += singleCharWhdth;
                    ListCharEndPosition.Add(single.Key, TotalWidth);

                    //delimiterWhdth
                    if (MainTextObject.Last().Key != single.Key)
                    {
                        var delimiterWhdth = GetStringWidth(Delimiter.Replace(" ", " "));
                        TotalWidth += delimiterWhdth;
                        ListCharEndPosition.Add(single.Key - 100, TotalWidth);
                    }
                }
            }
        }

        public float GetTextCenterPosition(int index)
        {
            //find this
            var thisValue = ListCharEndPosition.Where(x => x.Key == index).FirstOrDefault().Value;

            //find previous
            var previousValue = ListCharEndPosition.Where(x => x.Key == index - 100 - 1).FirstOrDefault().Value;

            //(a + b)/2
            var returnValue = (previousValue + thisValue) / 2;

            return returnValue;
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

                return ListCharEndPosition.Last().Value;
            }
            catch
            {
                //if private void load(FontStore store) not loaded,will run in here
                return 700;
            }
        }

        public int GetIndexByPosition(float position)
        {
            return ListCharEndPosition.Where(x => x.Value > position).FirstOrDefault().Key;
        }

        [BackgroundDependencyLoader]
        private void load(FontStore store)
        {
            FontStore = store;
            UpdateSingleCharacterEndPosition();
        }

        #region Function

        protected float GetStringWidth(string str)
        {
            float totalWidth = 0;
            foreach (var single in str)
            {
                //get single char width
                var singleCharWhdth = single == ' ' ? 15 : CreateCharacterDrawable(single).Width * TextSize;
                totalWidth += singleCharWhdth;
            }

            return totalWidth;
        }

        #endregion
    }
}
