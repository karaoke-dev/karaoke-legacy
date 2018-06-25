// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.Objects.Translate;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Helps
{
    /// <summary>
    ///     create verious of condition of lyric
    /// </summary>
    public static class DemoKaraokeObject
    {
        public static JpLyric WithoutProgressPoint()
        {
            var karaokeObject = new JpLyric();
            karaokeObject.Lyric = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.Furigana.Add(0, new FuriganaText
            {
                Text = "お"
            });
            karaokeObject.Furigana.Add(6, new FuriganaText
            {
                Text = "お"
            });

            return karaokeObject;
        }

        /// <summary>
        ///     generate normal demo 001
        /// </summary>
        /// <returns></returns>
        public static JpLyric GenerateDemo001()
        {
            var karaokeObject = new JpLyric();
            karaokeObject.Lyric = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.Furigana.Add(0, new FuriganaText
            {
                Text = "お"
            });
            karaokeObject.Furigana.Add(6, new FuriganaText
            {
                Text = "お"
            });
            karaokeObject.TimeLines.Add(new TimeLineIndex(0), new TimeLine(0));
            karaokeObject.TimeLines.Add(new TimeLineIndex(1), new TimeLine(500));
            karaokeObject.TimeLines.Add(new TimeLineIndex(5), new TimeLine(1000));
            karaokeObject.TimeLines.Add(new TimeLineIndex(11), new TimeLine(1500));

            return karaokeObject;
        }

        /// <summary>
        ///     use this in convertor
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static JpLyric GenerateWithStartAndDuration(double startTime, double duration)
        {
            var karaokeObject = new JpLyric();
            karaokeObject.Lyric = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.Furigana.Add(0, new FuriganaText
            {
                Text = "お"
            });
            karaokeObject.Furigana.Add(6, new FuriganaText
            {
                Text = "お"
            });
            karaokeObject.StartTime = startTime;

            karaokeObject.TimeLines.Add(new TimeLineIndex(0), new TimeLine(duration / 5)
            {
                Tone = new Tone(3)
            });
            karaokeObject.TimeLines.Add(new TimeLineIndex(9), new TimeLine(duration / 4)
            {
                Tone = new Tone(-3)
            });
            karaokeObject.TimeLines.Add(new TimeLineIndex(11), new TimeLine(duration)
            {
                Tone = new Tone(0, true)
            });

            karaokeObject.Romaji.Add(0, new RomajiText("o"));
            karaokeObject.Romaji.Add(1, new RomajiText("wa"));
            karaokeObject.Romaji.Add(2, new RomajiText("ru"));
            karaokeObject.Romaji.Add(3, new RomajiText("ma"));
            karaokeObject.Romaji.Add(4, new RomajiText("de"));
            karaokeObject.Romaji.Add(5, new RomajiText("wa"));
            karaokeObject.Romaji.Add(6, new RomajiText("o"));
            karaokeObject.Romaji.Add(7, new RomajiText("wa"));
            karaokeObject.Romaji.Add(8, new RomajiText("ra"));
            karaokeObject.Romaji.Add(9, new RomajiText("na"));
            karaokeObject.Romaji.Add(10, new RomajiText("i"));
            karaokeObject.Romaji.Add(11, new RomajiText("yo"));

            return karaokeObject;
        }

        public static JpLyric GenerateDeomKaraokeLyric()
        {
            return new JpLyric
            {
                Lyric = MainTextList.SetJapaneseLyric("カラオケ"),
                Furigana = new Dictionary<int, FuriganaText>
                {
                    { 0, new FuriganaText { Text = "か" } },
                    { 1, new FuriganaText { Text = "ら" } },
                    { 2, new FuriganaText { Text = "お" } },
                    { 3, new FuriganaText { Text = "け" } }
                },
                Romaji = new RomajiTextList
                {
                    { 0, new RomajiText { Text = "ka" } },
                    { 1, new RomajiText { Text = "ra" } },
                    { 2, new RomajiText { Text = "o" } },
                    { 3, new RomajiText { Text = "ke" } }
                },
                Translates = new LyricTranslateList
                {
                    { TranslateCode.English, new LyricTranslate { Text = "Karaoke" } }
                }
            };
        }
    }
}
