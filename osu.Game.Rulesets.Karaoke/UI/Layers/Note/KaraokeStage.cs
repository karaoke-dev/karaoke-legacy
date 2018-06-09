using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Screens.Play;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Note
{
    /// <summary>
    /// the stage contains list note group
    /// </summary>
    public class KaraokeStage : ScrollingPlayfield
    {
        public const float HIT_TARGET_POSITION = 200;

        public const float COLUMN_HEIGHT = 25;

        public const float COLUMN_SPACING = 1;

        /// <summary>
        /// Whether this playfield should be inverted. This flips everything inside the playfield.
        /// </summary>
        public readonly Bindable<bool> Inverted = new Bindable<bool>(true);

        public IReadOnlyList<Background> Columns => columnFlow.Children;
        private readonly FillFlowContainer<Background> columnFlow;

        protected override Container<Drawable> Content => content;
        private readonly Container<Drawable> content;

        public Container<DrawableNoteJudgement> Judgements => judgements;
        private readonly JudgementContainer<DrawableNoteJudgement> judgements;

        private List<Color4> normalColumnColours = new List<Color4>();
        private Color4 specialColumnColour;

        private readonly int firstColumnIndex;

        public KaraokeStage(int firstColumnIndex, KaraokeStageDefinition definition)
            : base(ScrollingDirection.Left)
        {
            this.firstColumnIndex = firstColumnIndex;

            Name = "Stage";

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Name = "Columns mask",
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Name = "Background",
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black,
                                    Alpha = 0.5f,
                                },
                                columnFlow = new FillFlowContainer<Background>
                                {
                                    Name = "Columns",
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Direction = FillDirection.Vertical,
                                    Padding = new MarginPadding { Top = COLUMN_SPACING, Bottom = COLUMN_SPACING },
                                    Spacing = new Vector2(0, COLUMN_SPACING),
                                },
                            }
                        },
                        new Container
                        {
                            Name = "Barlines mask",
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.X,
                            Height = 1366, // Bar lines should only be masked on the vertical axis
                            BypassAutoSizeAxes = Axes.Both,
                            Masking = true,
                            Child = content = new Container
                            {
                                Name = "Bar lines",
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.X,
                                Padding = new MarginPadding { Left = HIT_TARGET_POSITION }
                            }
                        },
                        judgements = new JudgementContainer<DrawableNoteJudgement>
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            X = HIT_TARGET_POSITION + 150,
                            BypassAutoSizeAxes = Axes.Both
                        },
                    }
                }
            };

            for (int i = 0; i < definition.Columns; i++)
            {
                var isSpecial = definition.IsSpecialColumn(i);
                var column = new Background
                {
                    Height = COLUMN_HEIGHT,
                    Alpha = 0.15f,
                };
                AddBackground(column);
            }

            Inverted.ValueChanged += invertedChanged;
            Inverted.TriggerChange();
        }

        private void invertedChanged(bool newValue)
        {
            //TODO : change the position but not change scale
            //Scale = new Vector2(newValue ? -1 : 1,1);
            Judgements.Scale = Scale;
        }

        public void AddBackground(Background c)
        {
            columnFlow.Add(c);
        }

        public override void Add(DrawableHitObject h)
        {
            h.Y = 0;
            h.OnJudgement += OnJudgement;
            //add
            base.Add(h);
        }

        public void Add(BarLine barline) => base.Add(new DrawableBarLine(barline));

        public void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            judgements.Clear();
            judgements.Add(new DrawableNoteJudgement(judgement, judgedObject)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            normalColumnColours = new List<Color4>
            {
                colours.Gray0,
                colours.Gray9
            };

            specialColumnColour = colours.BlueLight;

            var nonSpecialColumns = Columns.ToList();

            // We'll set the colours of the non-special columns in a separate loop, because the non-special
            // column colours are mirrored across their centre and special styles mess with this
            for (int i = 0; i < nonSpecialColumns.Count; i++)
            {
                Color4 colour = normalColumnColours[i % normalColumnColours.Count];
                nonSpecialColumns[i].AccentColour = colour;
                //nonSpecialColumns[nonSpecialColumns.Count - 1 - i].AccentColour = colour;
            }
        }

        protected override void Update()
        {
            // Due to masking differences, it is not possible to get the width of the columns container automatically
            // While masking on effectively only the Y-axis, so we need to set the width of the bar line container manually
            content.Height = columnFlow.Height;
        }
    }

    public class Background : Box, IHasAccentColour
    {
        public Background()
        {
            RelativeSizeAxes = Axes.X;
        }


        private Color4 accentColour;
        public Color4 AccentColour
        {
            get { return accentColour; }
            set
            {
                if (accentColour == value)
                    return;
                accentColour = value;

                this.Colour = accentColour;

            }
        }
    }
}
