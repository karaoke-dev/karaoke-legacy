// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

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

        /// <summary>
        /// Create
        /// TODO : add another language support
        /// </summary>
        /// <param name="lyricText"></param>
        /// <returns></returns>
        public Lyric Create(string lyricText)
        {
            var lyric = new JpLyric();
            lyric.MainLyric.Clear();
            var startCharIndex = 0;
            foreach (var singleCharacter in lyricText)
            {
                lyric.MainLyric.Add(startCharIndex, new MainText
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

        protected void CreateDefaultTimelines(Lyric lyric)
        {
            var relativeTime = 0;
            foreach (var lyricPart in lyric.MainLyric)
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
