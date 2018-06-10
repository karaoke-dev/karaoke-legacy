// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.MathUtils;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Screens.Play;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Note
{
    /// <summary>
    ///     use to show karaoke tone Playfield
    ///     like :
    ///     ---------------------------#####
    ///     --------------#####----####-----
    ///     ---------#####-----####---------
    ///     ---######-----------------------
    ///     --------------------------------
    /// </summary>
    public class KaraokeTonePlayfield : ScrollingPlayfield, ILayer
    {
        /// <summary>
        /// Whether this playfield should be inverted. This flips everything inside the playfield.
        /// </summary>
        public readonly Bindable<bool> Inverted = new Bindable<bool>(false);

        private readonly List<KaraokeStage> stages = new List<KaraokeStage>();

        public IList<BarLine> BarLines = new List<BarLine>();

        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }

        public KaraokeTonePlayfield(List<KaraokeStageDefinition> stageDefinitions)
            : base(ScrollingDirection.Left)
        {
            if (stageDefinitions == null)
                throw new ArgumentNullException(nameof(stageDefinitions));

            if (stageDefinitions.Count <= 0)
                throw new ArgumentException("Can't have zero or fewer stages.");

            Inverted.Value = true;

            GridContainer playfieldGrid;

            int firstColumnIndex = 0;

            var content = new Drawable[2][];
            for (int i = 0; i < stageDefinitions.Count; i++)
            {
                var newStage = new KaraokeStage(firstColumnIndex, stageDefinitions[i]);
                newStage.VisibleTimeRange.BindTo(VisibleTimeRange);
                newStage.Inverted.BindTo(Inverted);

                content[i] = new[] { newStage };

                stages.Add(newStage);
                AddNested(newStage);

                firstColumnIndex += newStage.Columns.Count;
            }

            InternalChild = playfieldGrid = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                Content = content,
            };
        }

        public override void Add(DrawableHitObject h)
        {
            //Create object
            var drawableNote = new DrawableKaraokeNoteGroup(h.HitObject as BaseLyric)
            {
                AccentColour = Color4.Blue
            };

            //regist event
            drawableNote.NoteSpeed.BindTo(VisibleTimeRange);

            //然後根據事件去做物件的加減
            getStageByColumn(((BaseLyric)drawableNote.HitObject).SingerIndex ?? 0).Add(drawableNote);
        }

        public void Add(BarLine barline) => stages.ForEach(s => s.Add(barline));

        private KaraokeStage getStageByColumn(int column)
        {
            int sum = 0;
            foreach (var stage in stages)
            {
                sum = sum + stage.Columns.Count;
                if (sum > column)
                    return stage;
            }

            return null;
        }

        [BackgroundDependencyLoader]
        private void load(KaraokeConfigManager karaokeConfig)
        {
            //initial bar lines
            initialBarLine();

            VisibleTimeRange.Value = 6000;
            //karaokeConfig.BindWith(KaraokeSetting.NoteSpeed, VisibleTimeRange);
        }

        private void initialBarLine()
        {
            var beatmap = KaraokeRulesetContainer.Beatmap;
            var objects = beatmap.HitObjects;
            double lastObjectTime = (objects.LastOrDefault() as IHasEndTime)?.EndTime ?? objects.LastOrDefault()?.StartTime ?? double.MaxValue;
            var timingPoints = beatmap.ControlPointInfo.TimingPoints;
            var barLines = new List<BarLine>();
            for (int i = 0; i < timingPoints.Count; i++)
            {
                TimingControlPoint point = timingPoints[i];

                double endTime = i < timingPoints.Count - 1 ? timingPoints[i + 1].Time - point.BeatLength : lastObjectTime + point.BeatLength * (int)point.TimeSignature;

                int index = 0;
                for (double t = timingPoints[i].Time; Precision.DefinitelyBigger(endTime, t); t += point.BeatLength, index++)
                {
                    barLines.Add(new BarLine
                    {
                        StartTime = t,
                        ControlPoint = point,
                        BeatIndex = index
                    });
                }
            }
            BarLines = barLines;
            BarLines.ForEach(Add);
        }
    }
}
