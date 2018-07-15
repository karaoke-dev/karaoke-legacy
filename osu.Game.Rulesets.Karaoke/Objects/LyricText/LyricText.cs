using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.LyricText
{
    public class LyricText
    {
        public string MainText { get; set; }

        public Dictionary<float, TimeLine.TimeLine> TimeLines { get; set; }
    }
}
