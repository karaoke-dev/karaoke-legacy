// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Objects.TimeLine
{
    /// <summary>
    ///     record what time the
    /// </summary>
    public class LyricTimeLine
    {
        /// <summary>
        ///     relative to word's strt time
        /// </summary>
        public double Duration { get; set; }

        public LyricTimeLine()
        {
        }

        public LyricTimeLine(double time)
        {
            Duration = time;
        }
    }
}
