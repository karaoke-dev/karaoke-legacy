// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Game.Database;
using osu.Game.Rulesets.Karaoke.Objects.Types;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// base karaoke object
    /// contain single sentence , a main text and several additional text
    /// </summary>
    public class KaraokeObject : HitObject, IHasKaraokeComponent, IHasPosition, IHasCombo, IHasEndTime, IHasPrimaryKey
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// template Index
        /// if null , will use all the 
        /// </summary>
        public int? TemplateIndex { get; set; } = 0;

        /// <summary>
        /// position Index
        /// if null , will be auto allogate
        /// </summary>
        public int? PositionIndex { get; set; } = null;

        /// <summary>
        /// the index of singer 
        /// Default is singler1;
        /// Each singer has different Text color
        /// </summary>
        public int? SingerIndex { get; set; } = 0;

        /// <inheritdoc />
        /// <summary>
        /// if template !=null will relative to template's position
        /// else, will be absolute position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// X position
        /// </summary>
        [JsonIgnore]
        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Y);
        }

        /// <inheritdoc />
        /// <summary>
        /// Y position
        /// </summary>
        [JsonIgnore]
        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(X, value);
        }

        /// <summary>
        /// width
        /// </summary>
        [JsonIgnore]
        public float? Width { get; set; }

        /// <summary>
        /// height
        /// </summary>
        [JsonIgnore]
        public float? Height { get; set; }

        /// <summary>
        /// Main text
        /// </summary>
        public TextObject MainText { get; set; } = new TextObject();

        /// <summary>
        /// List little aid text,like japanese's text
        /// </summary>
        public List<SubTextObject> ListSubTextObject { get; set; } = new List<SubTextObject>();

        /// <summary>
        /// List little aid text,like japanese's text
        /// </summary>
        public ListRomajiTextObject ListRomajiTextObject { get; set; } = new ListRomajiTextObject();

        /// <summary>
        /// record list time where position goes
        /// </summary>
        public ListProgressPoint ListProgressPoint { get; set; } = new ListProgressPoint();

        /// <summary>
        /// all the translate for a single language
        /// </summary>
        /// <value>The list trans late.</value>
        public ListKaraokeTranslateString ListTranslate { get; set; } = new ListKaraokeTranslateString();

        /// <summary>
        /// The time at which the HitObject ends.
        /// </summary>
        [JsonIgnore]
        public double EndTime => StartTime + Duration + (EndPreemptiveTime ?? 0);

        /// <summary>
        /// The duration of the HitObject.
        /// </summary>
        [JsonIgnore]
        public double Duration => ListProgressPoint.LastOrDefault()?.RelativeTime ?? 0;

        /// <summary>
        /// new combo
        /// </summary>
        public virtual bool NewCombo { get; set; }

        /// <summary>
        /// combo index，will be assign by beatmap post process or other extension?
        /// </summary>
        public int ComboIndex { get; set; }

        /// <summary>
        /// if value is null ,use automatically generated preemptive time;
        /// </summary>
        public double? PreemptiveTime { get; set; } = 600;

        /// <summary>
        /// End preemptive time
        /// </summary>
        public double? EndPreemptiveTime { get; set; } = 600;
    }
}
