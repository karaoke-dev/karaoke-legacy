using OpenTK;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Helps
{
    /// <summary>
    /// create verious of condition of karaokeObject
    /// </summary>
    public static class DemoKaraokeObject
    {
        public static KaraokeObject WithoutProgressPoint()
        {
            var karaokeObject = new KaraokeObject();
            karaokeObject.MainText.Text = "終わるまでは終わらないよ";
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.ListSubTextObject.Add(new TextObject
            {
                Text = "お",
                X=10,
            });
            karaokeObject.ListSubTextObject.Add(new TextObject
            {
                Text = "お",
                X=200,
            });

            return karaokeObject;
        }

        /// <summary>
        /// generate normal demo 001
        /// </summary>
        /// <returns></returns>
        public static KaraokeObject GenerateDemo001()
        {
            var karaokeObject = new KaraokeObject();
            karaokeObject.MainText.Text = "終わるまでは終わらないよ";
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.ListSubTextObject.Add(new TextObject
            {
                Text = "お",
                X=10,
            });
            karaokeObject.ListSubTextObject.Add(new TextObject
            {
                Text = "お",
                X=200,
            });

            karaokeObject.AddProgressPoint(new ProgressPoint(0, 0));

            karaokeObject.AddProgressPoint(new ProgressPoint(500, 1));
            karaokeObject.AddProgressPoint(new ProgressPoint(1000, 5));
            karaokeObject.AddProgressPoint(new ProgressPoint(1500, 11));

            karaokeObject.Duration = 1500;

            return karaokeObject;
        }

        public static KaraokeObject GenerateWithStartAndDuration(double startTime,double duration)
        {
            var karaokeObject = new KaraokeObject();
            karaokeObject.MainText.Text = "終わるまでは終わらないよ";
            karaokeObject.Position = new Vector2(300, 150);
            karaokeObject.ListSubTextObject.Add(new TextObject
            {
                Text = "お",
                X=10,
            });
            karaokeObject.ListSubTextObject.Add(new TextObject
            {
                Text = "お",
                X=200,
            });
            karaokeObject.StartTime = startTime;
            karaokeObject.Duration = duration;

            karaokeObject.AddProgressPoint(new ProgressPoint(duration/5, 0));
            karaokeObject.AddProgressPoint(new ProgressPoint(duration/4, 10));
            karaokeObject.AddProgressPoint(new ProgressPoint(duration, 11));

            return karaokeObject;
        }
    }
}
