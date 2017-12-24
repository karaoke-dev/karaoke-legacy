using osu.Framework.Allocation;
using osu.Framework.IO.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces
{
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
