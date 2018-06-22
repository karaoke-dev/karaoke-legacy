// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces
{
    /// <summary>
    ///     this class contains
    ///     1. top text
    ///     2. main text
    ///     3. bottom text
    /// </summary>
    public class LyricContainer : Container
    {
        /// <summary>
        ///     top text
        /// </summary>
        public List<KaraokeText> ListDrawableSubText { get; } = new List<KaraokeText>();

        /// <summary>
        ///     bottom text
        /// </summary>
        public List<KaraokeText> ListDrawableBottomText { get; } = new List<KaraokeText>();

        /// <summary>
        ///     main text
        /// </summary>
        public LyricText LyricText;

        /// <summary>
        ///     template
        /// </summary>
        public LyricTemplate Template
        {
            get => _template;
            set
            {
                _template = value;
                UpdateDrawable();
            }
        }

        /// <summary>
        ///     Config
        /// </summary>
        public KaraokeLyricConfig Config
        {
            get => _config;
            set
            {
                _config = value;
                UpdateDrawable();
            }
        }

        /// <summary>
        ///     Lyric
        /// </summary>
        public BaseLyric Lyric
        {
            get => _lyric;
            set
            {
                _lyric = value;
                UpdateDrawable();
            }
        }

        private LyricTemplate _template;
        private KaraokeLyricConfig _config;
        private BaseLyric _lyric;

        public LyricContainer()
        {
            Masking = true;
        }

        public void SetMaskStartAndEndPosition(float startPositionX, float endPositionX)
        {
            Position = new Vector2(startPositionX, 0);

            if (startPositionX != 0)
                foreach (var singleText in Children)
                    if (singleText is KaraokeText terxtObject)
                        terxtObject.Position = terxtObject.TextObject.Position - Position;

            Width = endPositionX - startPositionX;
        }

        public void SetColor(Color4 color)
        {
            Colour = color;
        }

        /// <summary>
        ///     update view
        /// </summary>
        protected virtual void UpdateDrawable()
        {
            UpdateText();
        }

        protected virtual void UpdateText()
        {
            if (Config != null && Template != null && Lyric != null)
            {
                ClearAllText();

                var mainTextDelimiter = "";
                Dictionary<int, TextComponent> mainText = null;
                Dictionary<int, FormattedText> bottomTexts = null;
                Dictionary<int, FormattedText> subTexts = null;

                if (Config.RomajiFirst)
                {
                    if (Lyric is IHasRomaji romajiLyric)
                    {
                        mainText = romajiLyric.Romaji.ToDictionary(k => k.Key, v => (TextComponent)v.Value);
                        mainTextDelimiter = " ";
                    }
                }
                else
                {
                    mainText = Lyric.Lyric.ToDictionary(k => k.Key, v => (TextComponent)v.Value);
                }

                //main text
                AddMainText(Template?.MainText, mainText.ToDictionary(k => k.Key, v => v.Value), mainTextDelimiter);

                //show romaji text
                if (Config.RomajiVislbility)
                    if (Config.RomajiFirst)
                    {
                        bottomTexts = Lyric.Lyric.ToDictionary(k => k.Key, v => Template?.BottomText + v.Value + new FormattedText
                        {
                            X = getxPosition(v.Key)
                        });
                    }
                    else
                    {
                        if (Lyric is IHasRomaji romajiLyric)
                            bottomTexts = romajiLyric.Romaji.ToDictionary(k => k.Key, v => Template?.BottomText + v.Value + new FormattedText
                            {
                                X = getxPosition(v.Key)
                            });
                    }

                //show subtext
                if (Config.SubTextVislbility)
                    if (Lyric is IHasFurigana furiganaLyric)
                        subTexts = furiganaLyric.Furigana.ToDictionary(k => k.Key, v => Template?.TopText + v.Value + new FormattedText
                        {
                            X = getxPosition(v.Key)
                        });

                //show sub text
                AddSubText(subTexts?.Select(x => x.Value).ToList());

                //show sub text
                AddBottomText(bottomTexts?.Select(x => x.Value).ToList());

                Width = LyricText.GetTextEndPosition();
                Height = Lyric.Height ?? 100;
            }

            float getxPosition(int index)
            {
                var positionX = LyricText.GetTextCenterPosition(index);

                return positionX;
            }
        }

        protected virtual void AddMainText(FormattedText formattedText, Dictionary<int, TextComponent> textObject, string delimiter)
        {
            if (LyricText == null)
            {
                LyricText = new LyricText(formattedText, textObject, delimiter);
                Add(LyricText);
            }
            else
            {
                LyricText.TextObject = formattedText;
                LyricText.MainTextObject = textObject;
            }
        }

        protected void AddSubText(List<FormattedText> textObjectsList)
        {
            if (textObjectsList == null)
                textObjectsList = new List<FormattedText>
                {
                    new FormattedText()
                };

            foreach (var textObject in textObjectsList)
            {
                var subText = new KaraokeText(textObject)
                {
                    Origin = Anchor.BottomCentre
                };
                Add(subText);
                ListDrawableSubText.Add(subText);
            }
        }

        protected void AddBottomText(List<FormattedText> textObjectsList)
        {
            if (textObjectsList == null)
                textObjectsList = new List<FormattedText>
                {
                    new FormattedText()
                };

            foreach (var textObject in textObjectsList)
            {
                var subText = new KaraokeText(textObject)
                {
                    Origin = Anchor.BottomCentre
                };
                Add(subText);
                ListDrawableBottomText.Add(subText);
            }
        }

        protected void ClearAllText()
        {
            ListDrawableSubText.Clear();
            ListDrawableBottomText.Clear();
            LyricText = null;
            Children = new Drawable[] { };
        }
    }
}
