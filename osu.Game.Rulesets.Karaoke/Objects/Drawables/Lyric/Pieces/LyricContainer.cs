// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces
{
    public class LyricContainer : Container //BufferedContainer
    {

        /// <summary>
        /// template
        /// </summary>
        public LyricTemplate Template
        {
            get => LeftSideText.Template;
            set
            {
                LeftSideText.Template = value;
                RightSideText.Template = value;
            }
        }

        /// <summary>
        /// Config
        /// </summary>
        public KaraokeLyricConfig Config
        {
            get => LeftSideText.Config;
            set
            {
                LeftSideText.Config = value;
                RightSideText.Config = value;
            }
        }

        /// <summary>
        /// Lyric
        /// </summary>
        public BaseLyric Lyric
        {
            get => LeftSideText.Lyric;
            set
            {
                LeftSideText.Lyric = value;
                RightSideText.Lyric = value;
            }
        }

        //Lyric
        public LyricText MainText => RightSideText.LyricText;

        //TopText
        public KaraokeText SubText => SubTexts?.FirstOrDefault();

        public List<KaraokeText> SubTexts => RightSideText.ListDrawableSubText;

        //BottomText
        public KaraokeText BottomText => BottomTexts?.FirstOrDefault();

        public List<KaraokeText> BottomTexts => RightSideText.ListDrawableBottomText;

        protected virtual LyricTextContainer LeftSideText { get; set; } = new LyricTextContainer();

        protected virtual LyricTextContainer RightSideText { get; set; } = new LyricTextContainer();

        private float _maskWidth;

        private float _maskHeight;

        public LyricContainer()
        {
            Children = new Drawable[]
            {
                RightSideText,
                LeftSideText
            };
        }

        public void SetWidth(float width)
        {
            _maskWidth = width;
            LeftSideText.Width = _maskWidth;
            RightSideText.Width = _maskWidth;
        }

        public void SetHeight(float height)
        {
            _maskHeight = height;
            LeftSideText.Height = _maskHeight;
            RightSideText.Height = _maskHeight;
        }

        public void MovingMask(float newValue)
        {
            LeftSideText.SetMaskStartAndEndPosition(0, newValue);
            RightSideText.SetMaskStartAndEndPosition(newValue, _maskWidth);
        }

        public void SetColor(Color4 color, Color4 backgroundColor)
        {
            LeftSideText.SetColor(color);
            //Right side is white
            RightSideText.SetColor(Color4.White);
        }
    }
}
