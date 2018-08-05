// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Tools;
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
            LyricCreator creator = new LyricCreator();
            var karaokeObject = creator.Create("終わるまでは終わらないよ") as JpLyric;
            LyricEditor editor = new LyricEditor(karaokeObject);
            editor.AddFurigana(0, new FuriganaText
            {
                Text = "お"
            });
            editor.AddFurigana(6, new FuriganaText
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
            LyricCreator creator = new LyricCreator();
            var karaokeObject = creator.Create("終わるまでは終わらないよ") as JpLyric;
            LyricEditor editor = new LyricEditor(karaokeObject);
            editor.AddFurigana(0, new FuriganaText
            {
                Text = "お"
            });
            editor.AddFurigana(6, new FuriganaText
            {
                Text = "お"
            });
            editor.AddTimeline(new TimeLineIndex(0), new TimeLine(0));
            editor.AddTimeline(new TimeLineIndex(1), new TimeLine(500));
            editor.AddTimeline(new TimeLineIndex(5), new TimeLine(1000));
            editor.AddTimeline(new TimeLineIndex(11), new TimeLine(1500));

            return karaokeObject;
        }

        /// <summary>
<<<<<<< HEAD
        /// use this in convertor
=======
        ///     use this in convertor
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static JpLyric GenerateWithStartAndDuration(double startTime, double duration)
        {
            LyricCreator creator = new LyricCreator();
            var karaokeObject = creator.Create("終わるまでは終わらないよ") as JpLyric;
            LyricEditor editor = new LyricEditor(karaokeObject);
            editor.AddFurigana(0, new FuriganaText
            {
                Text = "お"
            });
            editor.AddFurigana(6, new FuriganaText
            {
                Text = "お"
            });
            karaokeObject.StartTime = startTime;
<<<<<<< HEAD

            //it means
            //move to index 5 (0,1,2,3,4),tone 3, duration is (duration*1 / 6)
            karaokeObject.TimeLines.Add(5, new LyricTimeLine(duration*1 / 12)
=======
            editor.AddTimeline(new TimeLineIndex(0), new TimeLine(duration / 5)
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
            {
                Tone = new Tone(3)
            });
<<<<<<< HEAD
            karaokeObject.TimeLines.Add(9, new LyricTimeLine(duration*6 / 12)
=======
            editor.AddTimeline(new TimeLineIndex(9), new TimeLine(duration / 4)
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
            {
                Tone = new Tone(-3)
            });
<<<<<<< HEAD
            karaokeObject.TimeLines.Add(12, new LyricTimeLine(duration*5 / 12)
            {
                Tone = 0
=======
            editor.AddTimeline(new TimeLineIndex(11), new TimeLine(duration)
            {
                Tone = new Tone(0, true)
>>>>>>> 1b01f6105edd982a10b68d9a5e5f8fa8709be1db
            });
            editor.AddRomaji(0, new RomajiText("o"));
            editor.AddRomaji(1, new RomajiText("wa"));
            editor.AddRomaji(2, new RomajiText("ru"));
            editor.AddRomaji(3, new RomajiText("ma"));
            editor.AddRomaji(4, new RomajiText("de"));
            editor.AddRomaji(5, new RomajiText("wa"));
            editor.AddRomaji(6, new RomajiText("o"));
            editor.AddRomaji(7, new RomajiText("wa"));
            editor.AddRomaji(8, new RomajiText("ra"));
            editor.AddRomaji(9, new RomajiText("na"));
            editor.AddRomaji(10, new RomajiText("i"));
            editor.AddRomaji(11, new RomajiText("yo"));

            return karaokeObject;
        }

        public static JpLyric GenerateDeomKaraokeLyric()
        {
            LyricCreator creator = new LyricCreator();
            var karaokeObject = creator.Create("カラオケ") as JpLyric;
            return new JpLyric
            {
                Lyric = karaokeObject.Lyric,
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
