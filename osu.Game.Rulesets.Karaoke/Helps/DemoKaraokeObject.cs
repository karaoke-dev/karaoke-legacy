// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Helps
{
    /// <summary>
    /// create verious of condition of lyric
    /// </summary>
    public static class DemoKaraokeObject
    {
        public static JpLyric WithoutProgressPoint()
        {
            var karaokeObject = new JpLyric();
            karaokeObject.Lyric = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.Furigana.Add(0, new SubText
            {
                Text = "お",
            });
            karaokeObject.Furigana.Add(6, new SubText
            {
                Text = "お",
            });

            return karaokeObject;
        }

        /// <summary>
        /// generate normal demo 001
        /// </summary>
        /// <returns></returns>
        public static JpLyric GenerateDemo001()
        {
            var karaokeObject = new JpLyric();
            karaokeObject.Lyric = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.Furigana.Add(0, new SubText
            {
                Text = "お",
            });
            karaokeObject.Furigana.Add(6, new SubText
            {
                Text = "お",
            });
            karaokeObject.ProgressPoints.Add(0, new LyricProgressPoint(0));
            karaokeObject.ProgressPoints.Add(1, new LyricProgressPoint(500));
            karaokeObject.ProgressPoints.Add(5, new LyricProgressPoint(1000));
            karaokeObject.ProgressPoints.Add(11, new LyricProgressPoint(1500));

            return karaokeObject;
        }

        public static JpLyric GenerateWithStartAndDuration(double startTime, double duration)
        {
            var karaokeObject = new JpLyric();
            karaokeObject.Lyric = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.Furigana.Add(0, new SubText
            {
                Text = "お",
            });
            karaokeObject.Furigana.Add(6, new SubText
            {
                Text = "お",
            });
            karaokeObject.StartTime = startTime;

            karaokeObject.ProgressPoints.Add(0, new LyricProgressPoint(duration / 5));
            karaokeObject.ProgressPoints.Add(9, new LyricProgressPoint(duration / 4));
            karaokeObject.ProgressPoints.Add(11, new LyricProgressPoint(duration));

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
            return new JpLyric()
            {
                Lyric = MainTextList.SetJapaneseLyric("カラオケ"),
                Furigana = new Dictionary<int, SubText>()
                {
                    { 0, new SubText() { Text = "か" } },
                    { 1, new SubText() { Text = "ら" } },
                    { 2, new SubText() { Text = "お" } },
                    { 3, new SubText() { Text = "け" } },
                },
                Romaji = new RomajiTextList()
                {
                    { 0, new RomajiText() { Text = "ka" } },
                    { 1, new RomajiText() { Text = "ra" } },
                    { 2, new RomajiText() { Text = "o" } },
                    { 3, new RomajiText() { Text = "ke" } },
                },
                Translates = new LyricTranslateList()
                {
                    { TranslateCode.English, new LyricTranslate() { Text = "Karaoke" } },
                }
            };
        }
    }
}
