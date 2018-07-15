// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Game.Database;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Helps;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.Objects.Translate;
using osu.Game.Rulesets.Karaoke.Objects.Types;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    ///     base karaoke object
    ///     contain single sentence , a main text and several additional text
    /// </summary>
    public class BaseLyric : HitObject, ILyric, IHasCombo, IHasEndTime, IHasPrimaryKey
    {
        /// <summary>
        ///     ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     template Index
        ///     if null , will use all the
        /// </summary>
        public int? TemplateIndex { get; set; } = 0;

        /// <summary>
        ///     the index of singer
        ///     Default is singler1;
        ///     Each singer has different Text color
        /// </summary>
        public int? SingerIndex { get; set; } = 0;

        /// <summary>
        ///     Main text
        /// </summary>
        [JsonIgnore]
        public MainTextList Lyric { get; set; } = new MainTextList();

        /// <summary>
        ///     all the translate for a single language
        /// </summary>
        /// <value>The list trans late.</value>
        public LyricTranslateList Translates { get; set; } = new LyricTranslateList();

        /// <summary>
        ///     new combo
        /// </summary>
        public virtual bool NewCombo { get; set; }

        /// <summary>
        ///     combo index，will be assign by beatmap post process or other extension?
        /// </summary>
        public int ComboIndex { get; set; }

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
        ///     Version of the lyric
        /// </summary>
        public virtual int Ver { get; set; } = 0;


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

        /// <summary>
        ///     The time at which the HitObject ends.
        /// </summary>
        [JsonIgnore]
        public double EndTime => StartTime + Duration + (EndPreemptiveTime ?? 0);

        /// <summary>
        ///     The duration of the HitObject.
        /// </summary>
        [JsonIgnore]
        public double Duration => LyricHelper.GetLyricTimeLines(this).LastOrDefault().Value.RelativeTime;

        #endregion
    }

    /// <summary>
    ///     Main Text List
    /// </summary>
    public class MainTextList : LyricDictionary<int, LyricText.LyricText>, IHasText
    {
        public string Text
        {
            get
            {
                var result = this.Select(i => i.Value.MainText).Aggregate((i, j) => i + Delimiter + j);
                return result;
            }
        }

        [JsonIgnore] public string Delimiter = "";

        public static MainTextList SetJapaneseLyric(string str)
        {
            var returnList = new MainTextList();
            var startCharIndex = 0;
            foreach (var singleCharacter in str)
            {
                returnList.Add(startCharIndex, new LyricText.LyricText
                {
                    MainText = singleCharacter.ToString()
                });
                startCharIndex++;
            }

            return returnList;
        }

        public static MainTextList SetEnglishLyric(string str)
        {
            var returnList = new MainTextList();

            //TODO : implement

            return returnList;
        }
    }
}
