// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Screens.Play;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Note
{
    /// <summary>
    /// use to show karaoke tone Playfield
    /// like : 
    /// ---------------------------#####
    /// --------------#####----####-----
    /// ---------#####-----####---------
    /// ---######-----------------------
    /// --------------------------------
    /// </summary>
    public class KaraokeTonePlayfield : ScrollingPlayfield, ILayer
    {
        /// <summary>
        /// Whether this playfield should be inverted. This flips everything inside the playfield.
        /// </summary>
        public readonly Bindable<bool> Inverted = new Bindable<bool>(true);

        public List<SquareGraph.Column> Columns => stages.SelectMany(x => x.Columns).ToList();
        private readonly List<KaraokeStage> stages = new List<KaraokeStage>();

        public KaraokeToneStage(List<KaraokeStageDefinition> stageDefinitions)
            : base(ScrollingDirection.Up)
        {
            if (stageDefinitions == null)
                throw new ArgumentNullException(nameof(stageDefinitions));

            if (stageDefinitions.Count <= 0)
                throw new ArgumentException("Can't have zero or fewer stages.");

            Inverted.Value = true;

            GridContainer playfieldGrid;
            InternalChild = playfieldGrid = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                Content = new[] { new Drawable[stageDefinitions.Count] }
            };


            int firstColumnIndex = 0;
            for (int i = 0; i < stageDefinitions.Count; i++)
            {
                var newStage = new KaraokeStage(firstColumnIndex, stageDefinitions[i]);
                newStage.VisibleTimeRange.BindTo(VisibleTimeRange);
                newStage.Inverted.BindTo(Inverted);

                playfieldGrid.Content[0][i] = newStage;

                stages.Add(newStage);
                AddNested(newStage);

                firstColumnIndex += newStage.Columns.Count;
            }
        }

        public override void Add(DrawableHitObject h) => getStageByColumn(((DrawableKaraokeNote)h).TimeLine.Tone).Add(h);

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
            karaokeConfig.BindWith(KaraokeSetting.ScrollTime, VisibleTimeRange);
        }

        internal void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            getStageByColumn(((ManiaHitObject)judgedObject.HitObject).Column).OnJudgement(judgedObject, judgement);
        }
    }
}
