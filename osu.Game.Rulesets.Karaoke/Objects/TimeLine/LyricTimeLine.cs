// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using JetBrains.Annotations;
using osu.Game.Rulesets.Karaoke.Objects.Note;

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
        public double RelativeTime { get; set; }

        /// <summary>
        ///     Tone
        /// </summary>
        public Tone Tone { get; set; }

        /// <summary>
        ///     Display Text
        ///     If null, will get text from <see cref="MainText" />
        /// </summary>
        [CanBeNull]
        public string DisplayText { get; set; }

        /// <summary>
        ///     Duration
        ///     Default is -1 , means duration is next.RelativeTime -  this.RelativeTime
        /// </summary>
        public double? EarlyTime { get; set; }

        public LyricTimeLine()
        {
        }

        public LyricTimeLine(double time)
        {
            RelativeTime = time;
        }
    }
}
