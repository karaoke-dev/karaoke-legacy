using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;

namespace osu.Game.Rulesets.Karaoke.Edit.Tools
{
    /// <summary>
    /// used to convert <see cref="string"/> to <see cref="BaseLyric"/>
    /// </summary>
    public class LyricCreator
    {
        private readonly LyricEditor _editor = new LyricEditor();

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

            _editor.TargetLyric = lyric;
            //fix lyric format
            if (!_editor.LyricFormatIsValid())
            {
                _editor.FixLyricFormat();
            }

            return lyric;
        }
    }
}
