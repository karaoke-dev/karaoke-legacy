// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Helps
{
    /// <summary>
    /// create verious of condition of lyric
    /// </summary>
    public static class DemoKaraokeObject
    {
        public static Lyric WithoutProgressPoint()
        {
            var karaokeObject = new Lyric();
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
        public static Lyric GenerateDemo001()
        {
            var karaokeObject = new Lyric();
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

            karaokeObject.ProgressPoints.AddProgressPoint(new LyricProgressPoint(0, 0));

            karaokeObject.ProgressPoints.AddProgressPoint(new LyricProgressPoint(500, 1));
            karaokeObject.ProgressPoints.AddProgressPoint(new LyricProgressPoint(1000, 5));
            karaokeObject.ProgressPoints.AddProgressPoint(new LyricProgressPoint(1500, 11));

            return karaokeObject;
        }

        public static Lyric GenerateWithStartAndDuration(double startTime, double duration)
        {
            var karaokeObject = new Lyric();
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

            karaokeObject.ProgressPoints.AddProgressPoint(new LyricProgressPoint(duration / 5, 0));
            karaokeObject.ProgressPoints.AddProgressPoint(new LyricProgressPoint(duration / 4, 10));
            karaokeObject.ProgressPoints.AddProgressPoint(new LyricProgressPoint(duration, 11));

            return karaokeObject;
        }
    }
}
