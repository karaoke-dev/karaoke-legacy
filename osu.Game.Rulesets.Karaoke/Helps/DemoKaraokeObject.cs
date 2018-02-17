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
            karaokeObject.MainText.Text = "終わるまでは終わらないよ";
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.ListSubTextObject.Add(new SubText
            {
                Text = "お",
                CharIndex = 0,
            });
            karaokeObject.ListSubTextObject.Add(new SubText
            {
                Text = "お",
                CharIndex = 6,
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
            karaokeObject.MainText.Text = "終わるまでは終わらないよ";
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.ListSubTextObject.Add(new SubText
            {
                Text = "お",
                CharIndex = 0,
            });
            karaokeObject.ListSubTextObject.Add(new SubText
            {
                Text = "お",
                CharIndex = 6,
            });

            karaokeObject.ListProgressPoint.AddProgressPoint(new ProgressPoint(0, 0));

            karaokeObject.ListProgressPoint.AddProgressPoint(new ProgressPoint(500, 1));
            karaokeObject.ListProgressPoint.AddProgressPoint(new ProgressPoint(1000, 5));
            karaokeObject.ListProgressPoint.AddProgressPoint(new ProgressPoint(1500, 11));

            return karaokeObject;
        }

        public static Lyric GenerateWithStartAndDuration(double startTime, double duration)
        {
            var karaokeObject = new Lyric();
            karaokeObject.MainText.Text = "終わるまでは終わらないよ";
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.ListSubTextObject.Add(new SubText
            {
                Text = "お",
                CharIndex = 0,
            });
            karaokeObject.ListSubTextObject.Add(new SubText
            {
                Text = "お",
                CharIndex = 6,
            });
            karaokeObject.StartTime = startTime;

            karaokeObject.ListProgressPoint.AddProgressPoint(new ProgressPoint(duration / 5, 0));
            karaokeObject.ListProgressPoint.AddProgressPoint(new ProgressPoint(duration / 4, 10));
            karaokeObject.ListProgressPoint.AddProgressPoint(new ProgressPoint(duration, 11));

            return karaokeObject;
        }
    }
}
