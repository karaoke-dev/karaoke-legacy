// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    ///     record what time the
    /// </summary>
    public class LyricTimeLine
    {

        public LyricTimeLine()
        {
        }

        public LyricTimeLine(double time)
        {
            RelativeTime = time;
        }

        /// <summary>
        /// relative to word's strt time
        /// </summary>
        public double RelativeTime { get; set; }

        /// <summary>
        /// Tone
        /// </summary>
        public int? Tone { get; set; }

        /// <summary>
        /// Add helf tone
        /// </summary>
        public bool HelfTone { get; set; }

        /// <summary>
        /// Display Text
        /// If null, will get text from <see cref="MainText"/>
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Duration
        /// Default is -1 , means duration is next.RelativeTime -  this.RelativeTime
        /// </summary>
        public double? EarlyTime { get; set; }
    }
}
