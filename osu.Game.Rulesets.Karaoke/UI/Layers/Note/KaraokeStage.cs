// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Note
{
    /// <summary>
    ///     the stage contains list note group
    /// </summary>
    public class KaraokeStage : ScrollingPlayfield
    {
        public const float HIT_TARGET_POSITION = 600;

        public const float COLUMN_HEIGHT = 25;

        public const float COLUMN_SPACING = 1;

        /// <summary>
        ///     Whether this playfield should be inverted. This flips everything inside the playfield.
        /// </summary>
        public readonly Bindable<bool> Inverted = new Bindable<bool>(true);

        /// <summary>
        ///     Trigger if column is changed
        /// </summary>
        public readonly Bindable<KaraokeStageDefinition> TriggerColumnChanged = new Bindable<KaraokeStageDefinition>(new KaraokeStageDefinition());

        /// <summary>
        ///     Trigger if user changed the tone
        /// </summary>
        public readonly Bindable<Tone> Tone = new Bindable<Tone>(new Tone());

        public IReadOnlyList<Background> Columns => columnFlow.Children;
        public Container<DrawableNoteJudgement> Judgements => judgements;

        /// <summary>
        ///     Notified note if definition changed
        /// </summary>
        public KaraokeStageDefinition StateDefinition
        {
            get => _definition;
            set
            {
                _definition = value;

                //columns
                for (var i = 0; i < StateDefinition.Columns; i++)
                {
                    var column = new Background
                    {
                        Height = COLUMN_HEIGHT,
                        Alpha = 0.15f
                    };
                    AddBackground(column);
                }

                //Tone
                Tone.TriggerChange();

                //trigger change
                TriggerColumnChanged.TriggerChange();
            }
        }

        protected override Container<Drawable> Content => content;
        private readonly FillFlowContainer<Background> columnFlow;
        private readonly Container<Drawable> content;
        private readonly JudgementContainer<DrawableNoteJudgement> judgements;
        private readonly Background toneBackground;

        private List<Color4> normalColumnColours = new List<Color4>();
        private Color4 specialColumnColour;
        private KaraokeStageDefinition _definition;


        public KaraokeStage(KaraokeStageDefinition definition)
        {
            Direction.Value = ScrollingDirection.Left;

            if (definition == null)
                throw new ArgumentNullException(nameof(definition) + "cannot be null");

            if (definition.Columns <= 0 || definition.Columns % 2 == 0)
                throw new ArgumentException(nameof(definition.Columns) + "cannot be even.");

            if (definition.DefaultTone == null)
                throw new ArgumentNullException(nameof(definition.DefaultTone) + "cannot be null");

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
                                    Alpha = 0.5f
                                },
                                columnFlow = new FillFlowContainer<Background>
                                {
                                    Name = "Columns",
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Direction = FillDirection.Vertical,
                                    Padding = new MarginPadding { Top = COLUMN_SPACING, Bottom = COLUMN_SPACING },
                                    Spacing = new Vector2(0, COLUMN_SPACING)
                                },
                                toneBackground = new Background
                                {
                                    Name = "ToneIndicator",
                                    RelativeSizeAxes = Axes.X,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Height = COLUMN_HEIGHT,
                                    AccentColour = Color4.Red
                                }
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
                        new Box
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.Centre,
                            Colour = Color4.Red,
                            Width = 5,
                            RelativeSizeAxes = Axes.Y,
                            X = HIT_TARGET_POSITION
                        },
                        judgements = new JudgementContainer<DrawableNoteJudgement>
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            X = HIT_TARGET_POSITION - 150,
                            BypassAutoSizeAxes = Axes.Both
                        }
                    }
                }
            };

            StateDefinition = definition;

            Inverted.ValueChanged += invertedChanged;
            Inverted.TriggerChange();

            Tone.ValueChanged += ToneChanged;
            Tone.TriggerChange();
        }

        public void AddBackground(Background c)
        {
            columnFlow.Add(c);
        }

        public override void Add(DrawableHitObject h)
        {
            h.Y = 0;
            h.OnNewResult += OnNewResult;
            //add
            base.Add(h);
        }

        public void Add(BarLine barline)
        {
            base.Add(new DrawableBarLine(barline));
        }

        public void OnNewResult(DrawableHitObject judgedObject, JudgementResult result)
        {
            if (!judgedObject.DisplayResult || !DisplayJudgements)
                return;

            judgements.Clear();
            judgements.Add(new DrawableNoteJudgement(result, judgedObject)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }

        protected void ToneChanged(Tone tone)
        {
            //Change tone
            var realTone = StateDefinition.DefaultTone + tone;
            toneBackground.Y = NoteStageHelper.GetPositionByTone(realTone);
            toneBackground.Height = realTone.Helf ? COLUMN_SPACING * 4 : COLUMN_HEIGHT;
            toneBackground.Alpha = realTone.Helf ? 0.3f : 0.15f;
        }

        protected override void Update()
        {
            // Due to masking differences, it is not possible to get the width of the columns container automatically
            // While masking on effectively only the Y-axis, so we need to set the width of the bar line container manually
            content.Height = columnFlow.Height;
        }

        private void invertedChanged(bool newValue)
        {
            //TODO : change the position but not change scale
            //Scale = new Vector2(newValue ? -1 : 1,1);
            Judgements.Scale = Scale;
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
            for (var i = 0; i < nonSpecialColumns.Count; i++)
            {
                var colour = normalColumnColours[i % normalColumnColours.Count];
                nonSpecialColumns[i].AccentColour = colour;
                //nonSpecialColumns[nonSpecialColumns.Count - 1 - i].AccentColour = colour;
            }
        }
    }

    public class Background : Box, IHasAccentColour
    {
        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                if (accentColour == value)
                    return;
                accentColour = value;

                Colour = accentColour;
            }
        }


        private Color4 accentColour;

        public Background()
        {
            RelativeSizeAxes = Axes.X;
        }
    }
}
