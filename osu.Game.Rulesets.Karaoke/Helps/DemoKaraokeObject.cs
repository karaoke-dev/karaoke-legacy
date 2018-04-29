// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Helps
{
    /// <summary>
    /// create verious of condition of lyric
    /// </summary>
    public static class DemoKaraokeObject
    {
        public static BaseLyric WithoutProgressPoint()
        {
            var karaokeObject = new BaseLyric();
            karaokeObject.MainText = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.SubTexts.Add(0, new SubText
            {
                Text = "お",
            });
            karaokeObject.SubTexts.Add(6, new SubText
            {
                Text = "お",
            });

            return karaokeObject;
        }

        /// <summary>
        /// generate normal demo 001
        /// </summary>
        /// <returns></returns>
        public static BaseLyric GenerateDemo001()
        {
            var karaokeObject = new BaseLyric();
            karaokeObject.MainText = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.SubTexts.Add(0, new SubText
            {
                Text = "お",
            });
            karaokeObject.SubTexts.Add(6, new SubText
            {
                Text = "お",
            });
            karaokeObject.ProgressPoints.Add(0, new LyricProgressPoint(0));
            karaokeObject.ProgressPoints.Add(1, new LyricProgressPoint(500));
            karaokeObject.ProgressPoints.Add(5, new LyricProgressPoint(1000));
            karaokeObject.ProgressPoints.Add(11, new LyricProgressPoint(1500));

            return karaokeObject;
        }

        public static BaseLyric GenerateWithStartAndDuration(double startTime, double duration)
        {
            var karaokeObject = new BaseLyric();
            karaokeObject.MainText = MainTextList.SetJapaneseLyric("終わるまでは終わらないよ");
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.SubTexts.Add(0, new SubText
            {
                Text = "お",
            });
            karaokeObject.SubTexts.Add(6, new SubText
            {
                Text = "お",
            });
            karaokeObject.StartTime = startTime;

            karaokeObject.ProgressPoints.Add(0, new LyricProgressPoint(duration / 5));
            karaokeObject.ProgressPoints.Add(9, new LyricProgressPoint(duration / 4));
            karaokeObject.ProgressPoints.Add(11, new LyricProgressPoint(duration));

            karaokeObject.RomajiTextListRomajiTexts.Add(0, new RomajiText("o"));
            karaokeObject.RomajiTextListRomajiTexts.Add(1, new RomajiText("wa"));
            karaokeObject.RomajiTextListRomajiTexts.Add(2, new RomajiText("ru"));
            karaokeObject.RomajiTextListRomajiTexts.Add(3, new RomajiText("ma"));
            karaokeObject.RomajiTextListRomajiTexts.Add(4, new RomajiText("de"));
            karaokeObject.RomajiTextListRomajiTexts.Add(5, new RomajiText("wa"));
            karaokeObject.RomajiTextListRomajiTexts.Add(6, new RomajiText("o"));
            karaokeObject.RomajiTextListRomajiTexts.Add(7, new RomajiText("wa"));
            karaokeObject.RomajiTextListRomajiTexts.Add(8, new RomajiText("ra"));
            karaokeObject.RomajiTextListRomajiTexts.Add(9, new RomajiText("na"));
            karaokeObject.RomajiTextListRomajiTexts.Add(10, new RomajiText("i"));
            karaokeObject.RomajiTextListRomajiTexts.Add(11, new RomajiText("yo"));

            return karaokeObject;
        }

        public static BaseLyric GenerateDeomKaraokeLyric()
        {
            return new BaseLyric()
            {
                MainText = MainTextList.SetJapaneseLyric("カラオケ"),
                SubTexts = new Dictionary<int, SubText>()
                {
                    { 0, new SubText() { Text = "か" } },
                    { 1, new SubText() { Text = "ら" } },
                    { 2, new SubText() { Text = "お" } },
                    { 3, new SubText() { Text = "け" } },
                },
                RomajiTextListRomajiTexts = new RomajiTextList()
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
