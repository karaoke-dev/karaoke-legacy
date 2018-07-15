using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;

namespace osu.Game.Rulesets.Karaoke.Objects.Helps
{
    public static class LyricHelper
    {
        public static LyricDictionary<TimeLineIndex, TimeLine.TimeLine> GetLyricTimeLines(BaseLyric lyric)
        {
            var list = lyric.Lyric.Select(x =>
            {
                var timelines = new Dictionary<TimeLineIndex, TimeLine.TimeLine>();
                foreach (var single in x.Value.TimeLines)
                {
                    timelines.Add(new TimeLineIndex(x.Key, single.Key), single.Value);
                }
                return timelines;
            });

            return list.Aggregate(new LyricDictionary<TimeLineIndex, TimeLine.TimeLine>(), (x, y) => x.Concat(y));
        }
    }
}
