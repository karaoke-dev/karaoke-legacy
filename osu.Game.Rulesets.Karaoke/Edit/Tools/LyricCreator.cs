﻿using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;

namespace osu.Game.Rulesets.Karaoke.Edit.Tools
{
    /// <summary>
    /// used to convert <see cref="string"/> to <see cref="BaseLyric"/>
    /// </summary>
    public class LyricCreator
    {
        #region Method

        /// <summary>
        /// Create
        /// TODO : add another language support
        /// </summary>
        /// <param name="lyricText"></param>
        /// <returns></returns>
        public BaseLyric Create(string lyricText)
        {
            var lyric = new JpLyric();
            lyric.Lyric.Clear();
            var startCharIndex = 0;
            foreach (var singleCharacter in lyricText)
            {
                lyric.Lyric.Add(startCharIndex, new MainText
                {
                    Text = singleCharacter.ToString()
                });
                startCharIndex++;
            }

            CreateDefaultTimelines(lyric);

            var editor = new LyricEditor();
            editor.TargetLyric = lyric;
            //fix lyric format
            if (!editor.LyricFormatIsValid())
            {
                editor.FixLyricFormat();
            }

            return lyric;
        }

        #endregion

        #region Utilities

        protected void CreateDefaultTimelines(BaseLyric lyric)
        {
            var relativeTime = 0;
            foreach (var lyricPart in lyric.Lyric)
            {
                lyric.TimeLines.Add(new TimeLineIndex(lyricPart.Key), new TimeLine()
                {
                    RelativeTime = relativeTime
                });

                relativeTime = relativeTime + 100;
            }
        }

        #endregion


    }
}
