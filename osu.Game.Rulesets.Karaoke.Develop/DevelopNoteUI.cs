using System;
using System.Collections.Generic;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Configuration;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Judgements;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Tests.Visual;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Develop
{
    [TestFixture]
    public class DevelopNoteUi : OsuTestCase
    {
        private RulesetInfo maniaRuleset;
        private DependencyContainer dependencies;
        private KaraokePlayfield playfield;

        public DevelopNoteUi()
        {
            var rng = new Random(1337);

            /* 
            AddStep("test columns", () =>
            {
                Clear();
                
                var drawableNote = CreateDrawableHitObject();

                var column = new Column();
                column.VisibleTimeRange.Value = 1000;
                column.VisibleTimeRange.TriggerChange();
                column.AccentColour = Color4.Blue;
                Add(column);

                column.Add(drawableNote);
            });
            */

            /* 
            AddStep("test stage", () =>
            {
                Clear();

                var drawableNote = CreateDrawableHitObject();

                //add stage
                var stage = new KaraokeStage(0,new StageDefinition(){Columns = 10});
                Add(stage);

                //add hit object 
                stage.Add(drawableNote);
            });
            */

            AddStep("test playField", () =>
            {
                var drawableNote = CreateDrawableHitObject();

                //add playfield
                var stages = new List<KaraokeStageDefinition>
                {
                    new KaraokeStageDefinition
                    {
                        Columns = 11,
                        DefaultTone = new Tone(),
                    },
                    new KaraokeStageDefinition
                    {
                        Columns = 11,
                        DefaultTone = new Tone(),
                    },
                };
                playfield = createPlayfield(stages);

                playfield.Add(drawableNote);
            });


            //add hitExplosion
            AddStep("Hit explosion", () =>
            {

                int col = rng.Next(0, 4);

                var note = DemoKaraokeObject.GenerateWithStartAndDuration(0, 10000);
                note.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());

                var drawableNote = new DrawableLyricNoteGroup(note)
                {
                    //AccentColour = playfield.Columns.ElementAt(col).AccentColour
                };

                playfield.OnJudgement(drawableNote, new KaraokeJudgement { Result = HitResult.Perfect });
                //playfield.Columns[col].OnJudgement(drawableNote, new ManiaJudgement { Result = HitResult.Perfect });
            });

            //add note
            AddStep("Add Note", () =>
            {
                int col = rng.Next(0, 4);
                var note = DemoKaraokeObject.GenerateWithStartAndDuration(0, 10000);

                note.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());

                var drawableNote = new DrawableLyricNoteGroup(note)
                {
                    //AccentColour = playfield.Columns.ElementAt(col).AccentColour
                };

                playfield.Add(drawableNote);
            });
        }

        protected DrawableLyricNoteGroup CreateDrawableHitObject(int column = -1)
        {
            if (column == -1)
            {
                var rng = new Random(1337);
                column = rng.Next(0, 4);
            }

            var note = DemoKaraokeObject.GenerateWithStartAndDuration(0, 10000);
            note.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty());
            var drawableNote = new DrawableLyricNoteGroup(note)
            {
                X = 100,
                Width = 100,
                LifetimeStart = double.MinValue,
                LifetimeEnd = double.MaxValue,
                AccentColour = Color4.Red,
            };

            return drawableNote;
        }

        //protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent)
        //    => dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets, SettingsStore settings)
        {
            maniaRuleset = rulesets.GetRuleset(3);

            dependencies.Cache(new KaraokeConfigManager(settings, maniaRuleset, 0));
        }

        private KaraokePlayfield createPlayfield(List<KaraokeStageDefinition> stages, bool inverted = false)
        {
            Clear();

            var inputManager = new KaraokeInputManager(maniaRuleset) { RelativeSizeAxes = Axes.Both };
            Add(inputManager);

            KaraokePlayfield playfield;

            inputManager.Add(playfield = new KaraokePlayfield(stages)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });

            playfield.Inverted.Value = inverted;

            return playfield;
        }
    }

    public class KaraokePlayfield : ScrollingPlayfield
    {
        /// <summary>
        /// Whether this playfield should be inverted. This flips everything inside the playfield.
        /// </summary>
        public readonly Bindable<bool> Inverted = new Bindable<bool>(false);

        private readonly List<KaraokeStage> stages = new List<KaraokeStage>();

        public KaraokePlayfield(List<KaraokeStageDefinition> stageDefinitions)
        {
            Direction.Value = ScrollingDirection.Left;

            if (stageDefinitions == null)
                throw new ArgumentNullException(nameof(stageDefinitions));

            if (stageDefinitions.Count <= 0)
                throw new ArgumentException("Can't have zero or fewer stages.");

            Inverted.Value = true;

            GridContainer playfieldGrid;

            int firstColumnIndex = 0;

            var content = new Drawable[stageDefinitions.Count][];
            for (int i = 0; i < stageDefinitions.Count; i++)
            {
                var newStage = new KaraokeStage(stageDefinitions[i]);
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

        public override void Add(DrawableHitObject h) => getStageByColumn(((BaseLyric)h.HitObject).SingerIndex ?? 0).Add(h);

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
        private void load(KaraokeConfigManager maniaConfig)
        {
            maniaConfig.BindWith(KaraokeSetting.NoteSpeed, VisibleTimeRange);
        }

        internal void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            getStageByColumn(((BaseLyric)judgedObject.HitObject).SingerIndex ?? 0).OnJudgement(judgedObject, judgement);
        }
    }

    


    

   
}
