// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Localization;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;

namespace osu.Game.Rulesets.Karaoke.Edit.Tools
{
    /// <summary>
    /// used to convert <see cref="string"/> to <see cref="Lyric"/>
    /// </summary>
    public class LyricCreator
    {
        #region Method

        public Lyric CreateDefaultLyric()
        {
            //TODO : 根據不同語系產生對應的歌詞物件
            return Create<Lyric>("New Lyric");
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="lyricText"></param>
        /// <returns></returns>
        public T Create<T>(string lyricText) where T : Lyric , new()
        {
            var lyric = new T();
            lyric.TimeLines.Clear();

            var startCharIndex = 0;
            List<string> noteTexts;
            if (lyric is JpLyric)
            {
                noteTexts = lyricText.Select(x => x.ToString()).ToList();
            }
            else
            {
                noteTexts = lyricText.Split(' ').ToList();
            }

            foreach (var noteText in noteTexts)
            {
                lyric.TimeLines.Add(startCharIndex, new TimeLine
                {
                    LyricText = noteText
                });
                startCharIndex++;
            }

            CreateDefaultTimelines(lyric);

            var editor = new LyricEditor { TargetLyric = lyric };
            //fix lyric format
            if (!editor.LyricFormatIsValid())
            {
                editor.FixLyricFormat();
            }

            return lyric;
        }

        #endregion

        #region Utilities

        protected void CreateDefaultTimelines(Lyric lyric)
        {
            var relativeTime = 0;
            foreach (var lyricPart in lyric.TimeLines)
            {
                relativeTime = relativeTime + 200;
                lyric.TimeLines.Add(lyricPart.Key, new TimeLine
                {
                    RelativeTime = relativeTime
                });
            }
        }

        #endregion
    }
}
