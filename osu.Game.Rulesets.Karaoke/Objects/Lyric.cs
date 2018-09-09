// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using Newtonsoft.Json;
using osu.Game.Database;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.Objects.Localization;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.Objects.Translate;
using osu.Game.Rulesets.Karaoke.Objects.Types;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    ///     base karaoke object
    ///     contain single sentence , a main text and several additional text
    /// </summary>
    public class Lyric : HitObject, ILyric, IHasEndTime, IHasPrimaryKey , IHasStage
    {
        /// <summary>
        ///     The time at which the HitObject ends.
        /// </summary>
        [JsonIgnore]
        public double EndTime => StartTime + Duration + (EndPreemptiveTime ?? 0);

        /// <summary>
        ///     The duration of the HitObject.
        /// </summary>
        [JsonIgnore]
        public double Duration => TimeLines.LastOrDefault().Value?.RelativeTime ?? 0;

        /// <summary>
        ///     ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     template Index
        ///     if null , will use all the
        /// </summary>
        public int? TemplateIndex { get; set; }

        /// <summary>
        ///     the index of singer
        ///     Default is singler1;
        ///     Each singer has different Text color
        /// </summary>
        public int? SingerIndex { get; set; }

        /// <summary>
        ///     Main text
        /// </summary>
        [JsonIgnore]
        public MainTextList MainLyric { get; set; } = new MainTextList();

        /// <summary>
        ///     record list time where position goes
        /// </summary>
        public TimeLineList TimeLines { get; set; } = new TimeLineList();

        /// <summary>
        ///     all the translate for a single language
        /// </summary>
        /// <value>The list trans late.</value>
        public LyricTranslateList Translates { get; set; } = new LyricTranslateList();

        /// <summary>
        ///     if value is null ,use automatically generated preemptive time;
        /// </summary>
        public double? PreemptiveTime { get; set; } = 0;

        /// <summary>
        ///     End preemptive time
        /// </summary>
        public double? EndPreemptiveTime { get; set; } = 0;

        /// <summary>
        ///     get translate code
        /// </summary>
        /// <value>The translate code.</value>
        public TranslateCode Lang { get; set; }

        /// <summary>
        ///     Stage Index
        /// </summary>
        public int Stageindex { get; set; }

        /// <summary>
        ///     Version of the lyric
        /// </summary>
        public virtual int Ver { get; set; } = 0;

        /// <summary>
        /// Judgemnent
        /// </summary>
        /// <returns></returns>
        public override Judgement CreateJudgement() => new KaraokeJudgement();

        #region Function

        /// <summary>
        ///     Times the is in time.
        /// </summary>
        /// <returns><c>true</c>, if is in time was timed, <c>false</c> otherwise.</returns>
        /// <param name="nowRelativeTime">Now time.</param>
        public bool IsInTime(double nowRelativeTime)
        {
            if (nowRelativeTime > -PreemptiveTime && nowRelativeTime <= Duration + EndPreemptiveTime)
                return true;

            return false;
        }

        #endregion
    }

    /// <summary>
    ///     Main Text List
    /// </summary>
    public class MainTextList : LyricDictionary<int, MainText>, IHasText
    {
        public const string DELIMITER = "";
        public string Text
        {
            get
            {
                var result = this.Select(i => i.Value.Text).Aggregate((i, j) => i + DELIMITER + j);
                return result;
            }
        }
    }

    /// <summary>
    ///     Main Text
    /// </summary>
    public class MainText : TextComponent
    {
    }
}
