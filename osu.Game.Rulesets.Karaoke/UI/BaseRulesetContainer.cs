using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Input;
using osu.Framework.Lists;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Timing;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.Karaoke.UI
{
    /// <summary>
    ///     TODO :
    ///     <see cref="KaraokeTonePlayfield" /> 's column cannot work without this
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRulesetContainer<T> : RulesetContainer<T> where T : HitObject
    {
        /// <summary>
        ///     Provides the default <see cref="MultiplierControlPoint" />s that adjust the scrolling rate of
        ///     <see cref="HitObject" />s
        ///     inside this <see cref="RulesetContainer{TPlayfield,TObject}" />.
        /// </summary>
        /// <returns></returns>
        protected readonly SortedList<MultiplierControlPoint> DefaultControlPoints = new SortedList<MultiplierControlPoint>(Comparer<MultiplierControlPoint>.Default);

        public BaseRulesetContainer(Ruleset ruleset, WorkingBeatmap workingBeatmap)
            : base(ruleset, workingBeatmap)
        {
        }

        public override PassThroughInputManager CreateInputManager()
        {
            throw new NotImplementedException();
        }

        protected override Playfield CreatePlayfield()
        {
            throw new NotImplementedException();
        }

        protected override DrawableHitObject<T> GetVisualRepresentation(T h)
        {
            throw new NotImplementedException();
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            // Calculate default multiplier control points
            var lastTimingPoint = new TimingControlPoint();
            var lastDifficultyPoint = new DifficultyControlPoint();

            // Merge timing + difficulty points
            var allPoints = new SortedList<ControlPoint>(Comparer<ControlPoint>.Default);
            allPoints.AddRange(Beatmap.ControlPointInfo.TimingPoints);
            allPoints.AddRange(Beatmap.ControlPointInfo.DifficultyPoints);

            // Generate the timing points, making non-timing changes use the previous timing change
            var timingChanges = allPoints.Select(c =>
            {
                var timingPoint = c as TimingControlPoint;
                var difficultyPoint = c as DifficultyControlPoint;

                if (timingPoint != null)
                    lastTimingPoint = timingPoint;

                if (difficultyPoint != null)
                    lastDifficultyPoint = difficultyPoint;

                return new MultiplierControlPoint(c.Time)
                {
                    TimingPoint = lastTimingPoint,
                    DifficultyPoint = lastDifficultyPoint
                };
            });

            var lastObjectTime = (Objects.LastOrDefault() as IHasEndTime)?.EndTime ?? Objects.LastOrDefault()?.StartTime ?? double.MaxValue;

            // Perform some post processing of the timing changes
            timingChanges = timingChanges
                // Collapse sections after the last hit object
                .Where(s => s.StartTime <= lastObjectTime)
                // Collapse sections with the same start time
                .GroupBy(s => s.StartTime).Select(g => g.Last()).OrderBy(s => s.StartTime);

            DefaultControlPoints.AddRange(timingChanges);

            // If we have no control points, add a default one
            if (DefaultControlPoints.Count == 0)
                DefaultControlPoints.Add(new MultiplierControlPoint());

            DefaultControlPoints.ForEach(c => applySpeedAdjustment(c, (Playfield as KaraokeBasePlayfield).KaraokeTonePlayfield));
        }

        private void applySpeedAdjustment(MultiplierControlPoint controlPoint, ScrollingPlayfield playfield)
        {
            playfield.HitObjects.AddControlPoint(controlPoint);
            playfield.NestedPlayfields?.OfType<ScrollingPlayfield>().ForEach(p => applySpeedAdjustment(controlPoint, p));
        }
    }
}
