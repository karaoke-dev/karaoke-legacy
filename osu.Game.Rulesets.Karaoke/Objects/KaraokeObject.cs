// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// base karaoke object
    /// contain single sentence , a main text and several additional text
    /// </summary>
    public class KaraokeObject : HitObject, IHasPosition ,IHasCombo, IHasEndTime
    {
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
        public int SingerIndex { get; set; } = 0;

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
        public float X
        {
            get => Position.X;
            set => Position = new Vector2(value, Y);
        }

        /// <inheritdoc />
        /// <summary>
        /// Y position
        /// </summary>
        public float Y
        {
            get => Position.Y;
            set => Position = new Vector2(X, value);
        }

        /// <summary>
        /// width
        /// </summary>
        public float? Width { get; set; }

        /// <summary>
        /// height
        /// </summary>
        public float? Height { get; set; }

        /// <summary>
        /// Main text
        /// </summary>
        public TextObject MainText { get; set; } = new TextObject()
        {
            //FontSize = 50, //default Main text Size is 70
            //Position = new Vector2(0, 30), //default position
        };

        /// <summary>
        /// List little aid text,like japanese's text
        /// </summary>
        public List<TextObject> ListSubTextObject { get; set; } = new List<TextObject>();

        /// <summary>
        /// record list time where position goes
        /// </summary>
        public List<ProgressPoint> ListProgressPoint { get; set; } = new List<ProgressPoint>()
        {
            
        };

        /// <summary>
        /// all the translate for a single language
        /// </summary>
        /// <value>The list trans late.</value>
        public List<KaraokeTranslateString> ListTranslate { get; set; } = new List<KaraokeTranslateString>();

        /// <summary>
        /// The time at which the HitObject ends.
        /// </summary>
        public double EndTime => StartTime + Duration;

        /// <summary>
        /// The duration of the HitObject.
        /// </summary>
        public double Duration { get; set; }

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
        public double? PreemptiveTime { get; set; }
    }
}
