using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Bindings;
using osu.Game.Graphics;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Common.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.Note;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Tests.Visual;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Develop
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test Karaoke Object")]
    public class DevelopNoteUi : OsuTestCase
    {
        public DevelopNoteUi()
        {
            ManiaStage stage = new ManiaStage();

            this.Add(stage);
        }
    }

    /// <summary>
    /// A collection of <see cref="Column"/>s.
    /// </summary>
    internal class ManiaStage : ScrollingPlayfield
    {
        public const float HIT_TARGET_POSITION = 50;

        /// <summary>
        /// Whether this playfield should be inverted. This flips everything inside the playfield.
        /// </summary>
        public readonly Bindable<bool> Inverted = new Bindable<bool>(true);

        public IReadOnlyList<Column> Columns => columnFlow.Children;
        private readonly FillFlowContainer<Column> columnFlow;

        protected override Container<Drawable> Content => content;
        private readonly Container<Drawable> content;

        public Container<DrawableManiaJudgement> Judgements => judgements;
        private readonly JudgementContainer<DrawableManiaJudgement> judgements;

        private readonly Container topLevelContainer;

        private List<Color4> normalColumnColours = new List<Color4>();
        private Color4 specialColumnColour;

        private readonly int firstColumnIndex;

        public ManiaStage(int firstColumnIndex, StageDefinition definition, ref ManiaAction normalColumnStartAction, ref ManiaAction specialColumnStartAction)
            : base(ScrollingDirection.Up)
        {
            this.firstColumnIndex = firstColumnIndex;

            Name = "Stage";

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Name = "Columns mask",
                            RelativeSizeAxes = Axes.Y,
                            AutoSizeAxes = Axes.X,
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Name = "Background",
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black
                                },
                                columnFlow = new FillFlowContainer<Column>
                                {
                                    Name = "Columns",
                                    RelativeSizeAxes = Axes.Y,
                                    AutoSizeAxes = Axes.X,
                                    Direction = FillDirection.Horizontal,
                                    Padding = new MarginPadding { Left = 1, Right = 1 },
                                    Spacing = new Vector2(1, 0)
                                },
                            }
                        },
                        new Container
                        {
                            Name = "Barlines mask",
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            RelativeSizeAxes = Axes.Y,
                            Width = 1366, // Bar lines should only be masked on the vertical axis
                            BypassAutoSizeAxes = Axes.Both,
                            Masking = true,
                            Child = content = new Container
                            {
                                Name = "Bar lines",
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                RelativeSizeAxes = Axes.Y,
                                Padding = new MarginPadding { Top = HIT_TARGET_POSITION }
                            }
                        },
                        judgements = new JudgementContainer<DrawableManiaJudgement>
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            Y = HIT_TARGET_POSITION + 150,
                            BypassAutoSizeAxes = Axes.Both
                        },
                        topLevelContainer = new Container { RelativeSizeAxes = Axes.Both }
                    }
                }
            };

            for (int i = 0; i < definition.Columns; i++)
            {
                var isSpecial = definition.IsSpecialColumn(i);
                var column = new Column
                {
                    IsSpecial = isSpecial,
                    Action = isSpecial ? specialColumnStartAction++ : normalColumnStartAction++
                };

                AddColumn(column);
            }

            Inverted.ValueChanged += invertedChanged;
            Inverted.TriggerChange();
        }

        private void invertedChanged(bool newValue)
        {
            Scale = new Vector2(1, newValue ? -1 : 1);
            Judgements.Scale = Scale;
        }

        public void AddColumn(Column c)
        {
            c.VisibleTimeRange.BindTo(VisibleTimeRange);

            topLevelContainer.Add(c.TopLevelContainer.CreateProxy());
            columnFlow.Add(c);
            AddNested(c);
        }

        public override void Add(DrawableHitObject h)
        {
            var maniaObject = (ManiaHitObject)h.HitObject;
            int columnIndex = maniaObject.Column - firstColumnIndex;
            Columns.ElementAt(columnIndex).Add(h);
            h.OnJudgement += OnJudgement;
        }

        public void Add(BarLine barline) => base.Add(new DrawableBarLine(barline));

        internal void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            judgements.Clear();
            judgements.Add(new DrawableManiaJudgement(judgement, judgedObject)
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
                colours.RedDark,
                colours.GreenDark
            };

            specialColumnColour = colours.BlueDark;

            // Set the special column + colour + key
            foreach (var column in Columns)
            {
                if (!column.IsSpecial)
                    continue;

                column.AccentColour = specialColumnColour;
            }

            var nonSpecialColumns = Columns.Where(c => !c.IsSpecial).ToList();

            // We'll set the colours of the non-special columns in a separate loop, because the non-special
            // column colours are mirrored across their centre and special styles mess with this
            for (int i = 0; i < Math.Ceiling(nonSpecialColumns.Count / 2f); i++)
            {
                Color4 colour = normalColumnColours[i % normalColumnColours.Count];
                nonSpecialColumns[i].AccentColour = colour;
                nonSpecialColumns[nonSpecialColumns.Count - 1 - i].AccentColour = colour;
            }
        }

        protected override void Update()
        {
            // Due to masking differences, it is not possible to get the width of the columns container automatically
            // While masking on effectively only the Y-axis, so we need to set the width of the bar line container manually
            content.Width = columnFlow.Width;
        }
    }

    public class Column : ScrollingPlayfield, IKeyBindingHandler<ManiaAction>, IHasAccentColour
    {
        private const float key_icon_size = 10;
        private const float key_icon_corner_radius = 3;
        private const float key_icon_border_radius = 2;

        private const float hit_target_height = 10;
        private const float hit_target_bar_height = 2;

        private const float column_width = 45;
        private const float special_column_width = 70;

        public ManiaAction Action;

        private readonly Box background;
        private readonly Container hitTargetBar;
        private readonly Container keyIcon;

        internal readonly Container TopLevelContainer;
        private readonly Container explosionContainer;

        protected override Container<Drawable> Content => content;
        private readonly Container<Drawable> content;

        private const float opacity_released = 0.1f;
        private const float opacity_pressed = 0.25f;

        public Column()
            : base(ScrollingDirection.Up)
        {
            RelativeSizeAxes = Axes.Y;
            Width = column_width;

            InternalChildren = new Drawable[]
            {
                background = new Box
                {
                    Name = "Background",
                    RelativeSizeAxes = Axes.Both,
                    Alpha = opacity_released
                },
                new Container
                {
                    Name = "Hit target + hit objects",
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding { Top = ManiaStage.HIT_TARGET_POSITION },
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Name = "Hit target",
                            RelativeSizeAxes = Axes.X,
                            Height = hit_target_height,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Name = "Background",
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black
                                },
                                hitTargetBar = new Container
                                {
                                    Name = "Bar",
                                    RelativeSizeAxes = Axes.X,
                                    Height = hit_target_bar_height,
                                    Masking = true,
                                    Children = new[]
                                    {
                                        new Box
                                        {
                                            RelativeSizeAxes = Axes.Both
                                        }
                                    }
                                }
                            }
                        },
                        content = new Container
                        {
                            Name = "Hit objects",
                            RelativeSizeAxes = Axes.Both,
                        },
                        // For column lighting, we need to capture input events before the notes
                        new InputTarget
                        {
                            Pressed = onPressed,
                            Released = onReleased
                        },
                        explosionContainer = new Container
                        {
                            Name = "Hit explosions",
                            RelativeSizeAxes = Axes.Both
                        }
                    }
                },
                new Container
                {
                    Name = "Key",
                    RelativeSizeAxes = Axes.X,
                    Height = ManiaStage.HIT_TARGET_POSITION,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Name = "Key gradient",
                            RelativeSizeAxes = Axes.Both,
                            Colour = ColourInfo.GradientVertical(Color4.Black, Color4.Black.Opacity(0)),
                            Alpha = 0.5f
                        },
                        keyIcon = new Container
                        {
                            Name = "Key icon",
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(key_icon_size),
                            Masking = true,
                            CornerRadius = key_icon_corner_radius,
                            BorderThickness = 2,
                            BorderColour = Color4.White, // Not true
                            Children = new[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Alpha = 0,
                                    AlwaysPresent = true
                                }
                            }
                        }
                    }
                },
                TopLevelContainer = new Container { RelativeSizeAxes = Axes.Both }
            };

            TopLevelContainer.Add(explosionContainer.CreateProxy());
        }

        public override Axes RelativeSizeAxes => Axes.Y;

        private bool isSpecial;
        public bool IsSpecial
        {
            get { return isSpecial; }
            set
            {
                if (isSpecial == value)
                    return;
                isSpecial = value;

                Width = isSpecial ? special_column_width : column_width;
            }
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

                background.Colour = accentColour;

                hitTargetBar.EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Glow,
                    Radius = 5,
                    Colour = accentColour.Opacity(0.5f),
                };

                keyIcon.EdgeEffect = new EdgeEffectParameters
                {
                    Type = EdgeEffectType.Glow,
                    Radius = 5,
                    Colour = accentColour.Opacity(0.5f),
                };
            }
        }

        /// <summary>
        /// Adds a DrawableHitObject to this Playfield.
        /// </summary>
        /// <param name="hitObject">The DrawableHitObject to add.</param>
        public override void Add(DrawableHitObject hitObject)
        {
            hitObject.AccentColour = AccentColour;
            hitObject.OnJudgement += OnJudgement;

            HitObjects.Add(hitObject);
        }

        internal void OnJudgement(DrawableHitObject judgedObject, Judgement judgement)
        {
            if (!judgement.IsHit)
                return;

            explosionContainer.Add(new HitExplosion(judgedObject));
        }

        private bool onPressed(ManiaAction action)
        {
            if (action == Action)
            {
                background.FadeTo(opacity_pressed, 50, Easing.OutQuint);
                keyIcon.ScaleTo(1.4f, 50, Easing.OutQuint);
            }

            return false;
        }

        private bool onReleased(ManiaAction action)
        {
            if (action == Action)
            {
                background.FadeTo(opacity_released, 800, Easing.OutQuart);
                keyIcon.ScaleTo(1f, 400, Easing.OutQuart);
            }

            return false;
        }

        /// <summary>
        /// This is a simple container which delegates various input events that have to be captured before the notes.
        /// </summary>
        private class InputTarget : Container, IKeyBindingHandler<ManiaAction>
        {
            public Func<ManiaAction, bool> Pressed;
            public Func<ManiaAction, bool> Released;

            public InputTarget()
            {
                RelativeSizeAxes = Axes.Both;
                AlwaysPresent = true;
                Alpha = 0;
            }

            public bool OnPressed(ManiaAction action) => Pressed?.Invoke(action) ?? false;
            public bool OnReleased(ManiaAction action) => Released?.Invoke(action) ?? false;
        }

        public bool OnPressed(ManiaAction action)
        {
            if (action != Action)
                return false;

            var hitObject = HitObjects.Objects.LastOrDefault(h => h.HitObject.StartTime > Time.Current) ?? HitObjects.Objects.FirstOrDefault();
            hitObject?.PlaySamples();

            return true;
        }

        public bool OnReleased(ManiaAction action) => false;
    }

    /// <summary>
    /// Visualises a <see cref="HoldNote"/> hit object.
    /// </summary>
    public class DrawableHoldNote : DrawableHitObject<BaseLyric>
    {
        private readonly DrawableNote head;
        private readonly DrawableNote tail;

        private readonly GlowPiece glowPiece;
        private readonly BodyPiece bodyPiece;
        private readonly Container fullHeightContainer;

        /// <summary>
        /// Time at which the user started holding this hold note. Null if the user is not holding this hold note.
        /// </summary>
        private double? holdStartTime;

        /// <summary>
        /// Whether the hold note has been released too early and shouldn't give full score for the release.
        /// </summary>
        private bool hasBroken;

        public DrawableHoldNote(HoldNote hitObject, ManiaAction action)
            : base(hitObject, action)
        {
            Container<DrawableHoldNoteTick> tickContainer;
            RelativeSizeAxes = Axes.X;

            InternalChildren = new Drawable[]
            {
                // The hit object itself cannot be used for various elements because the tail overshoots it
                // So a specialized container that is updated to contain the tail height is used
                fullHeightContainer = new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Child = glowPiece = new GlowPiece()
                },
                bodyPiece = new BodyPiece
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                },
                tickContainer = new Container<DrawableHoldNoteTick>
                {
                    RelativeSizeAxes = Axes.Both,
                    ChildrenEnumerable = HitObject.NestedHitObjects.OfType<HoldNoteTick>().Select(tick => new DrawableHoldNoteTick(tick)
                    {
                        HoldStartTime = () => holdStartTime
                    })
                },
                head = new DrawableHeadNote(this, action)
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                },
                tail = new DrawableTailNote(this, action)
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre
                }
            };

            foreach (var tick in tickContainer)
                AddNested(tick);

            AddNested(head);
            AddNested(tail);
        }

        public override Color4 AccentColour
        {
            get { return base.AccentColour; }
            set
            {
                base.AccentColour = value;

                glowPiece.AccentColour = value;
                bodyPiece.AccentColour = value;
                head.AccentColour = value;
                tail.AccentColour = value;
            }
        }

        protected override void UpdateState(ArmedState state)
        {
        }

        protected override void Update()
        {
            base.Update();

            // Make the body piece not lie under the head note
            bodyPiece.Y = head.Height;
            bodyPiece.Height = DrawHeight - head.Height;

            // Make the fullHeightContainer "contain" the height of the tail note, keeping in mind
            // that the tail note overshoots the height of this hit object
            fullHeightContainer.Height = DrawHeight + tail.Height;
        }

        /// <summary>
        /// The head note of a hold.
        /// </summary>
        private class DrawableHeadNote : DrawableNote
        {
            private readonly DrawableHoldNote holdNote;

            public DrawableHeadNote(DrawableHoldNote holdNote, ManiaAction action)
                : base(holdNote.HitObject.Head, action)
            {
                this.holdNote = holdNote;

                GlowPiece.Alpha = 0;
            }

            public override bool OnPressed(ManiaAction action)
            {
                if (!base.OnPressed(action))
                    return false;

                // If the key has been released too early, the user should not receive full score for the release
                if (Judgements.Any(j => j.Result == HitResult.Miss))
                    holdNote.hasBroken = true;

                // The head note also handles early hits before the body, but we want accurate early hits to count as the body being held
                // The body doesn't handle these early early hits, so we have to explicitly set the holding state here
                holdNote.holdStartTime = Time.Current;

                return true;
            }

            protected override void UpdateState(ArmedState state)
            {
                // The holdnote keeps scrolling through for now, so having the head disappear looks weird
            }
        }

        /// <summary>
        /// The tail note of a hold.
        /// </summary>
        private class DrawableTailNote : DrawableNote
        {
            private readonly DrawableHoldNote holdNote;

            public DrawableTailNote(DrawableHoldNote holdNote, ManiaAction action)
                : base(holdNote.HitObject.Tail, action)
            {
                this.holdNote = holdNote;

                GlowPiece.Alpha = 0;
            }

            protected override void CheckForJudgements(bool userTriggered, double timeOffset)
            {
                if (!userTriggered)
                {
                    if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    {
                        AddJudgement(new HoldNoteTailJudgement
                        {
                            Result = HitResult.Miss,
                            HasBroken = holdNote.hasBroken
                        });
                    }

                    return;
                }

                var result = HitObject.HitWindows.ResultFor(timeOffset);
                if (result == HitResult.None)
                    return;

                AddJudgement(new HoldNoteTailJudgement
                {
                    Result = result,
                    HasBroken = holdNote.hasBroken
                });
            }

            protected override void UpdateState(ArmedState state)
            {
                // The holdnote keeps scrolling through, so having the tail disappear looks weird
            }
        }
    }
}
