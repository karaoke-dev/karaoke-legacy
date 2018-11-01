﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;
using osu.Game.Graphics.UserInterface;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog
{
    /// <summary>
    ///     windows type
    /// </summary>
    public enum DialogContainerStatus
    {
        Onscreen, //using this Dialog
        Offscreen, //mouse does not focus on this Dialoh
        Lock //Cannot use this Dialog until other Dialog Closed
    }

    /// <summary>
    ///     use as windows type dialog
    ///     refrence : osu.Framework.Graphics.Visualisation
    /// </summary>
    public class DialogContainer : Container, IStateful<DialogContainerStatus>
    {
        public Action CloseAction;

        //Title
        public virtual string Title { get; set; }

        //content of dialog should be write in here
        public virtual Container MainContext { get; set; } = new Container
        {
            Padding = new MarginPadding(0),
            RelativeSizeAxes = Axes.Y,
            Width = 600,
            Children = new Drawable[]
            {
                new ScrollContainer
                {
                    Padding = new MarginPadding(10),
                    RelativeSizeAxes = Axes.Y
                    //Width = this.Width
                }
            }
        };

        public DialogContainerStatus State
        {
            get => state;

            set
            {
                if (state == value)
                    return;
                state = value;

                switch (state)
                {
                    case DialogContainerStatus.Offscreen:
                        this.Delay(500).FadeTo(0.7f, 300);
                        break;
                    case DialogContainerStatus.Onscreen:
                        this.FadeIn(300, Easing.OutQuint);
                        break;
                }

                StateChanged?.Invoke(State);
            }
        }

        //Context
        protected override Container<Drawable> Content => this;


        protected Container TitleBar;

        private DialogContainerStatus state;

        public DialogContainer()
        {
            //can be modified
            Width = 600;
            Height = 300;
            Position = new Vector2(100, 100);

            //suggest not to modified this
            Masking = true;
            CornerRadius = 5;
            //AutoSizeAxes = Axes.X;

            InitialDialog();
        }

        #region Disposal

        protected override void Dispose(bool isDisposing)
        {
            //before close, do some effect in here

            base.Dispose(isDisposing);
        }

        #endregion

        /// <summary>
        ///     Dialog will be initialize in here
        /// </summary>
        public virtual void InitialDialog()
        {
            AddRangeInternal(new Drawable[]
            {
                new Box
                {
                    Colour = new Color4(50, 50, 50, 255), //Colour = new Color4(15, 15, 15, 255),
                    RelativeSizeAxes = Axes.Both,
                    Depth = 0
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical,
                    Children = new Drawable[]
                    {
                        TitleBar = new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1, 25),
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.BlueViolet
                                },
                                new SpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Text = Title,
                                    Alpha = 0.8f
                                },
                                new TriangleButton
                                {
                                    Anchor = Anchor.CentreRight,
                                    Origin = Anchor.Centre,
                                    Width = 16,
                                    Height = 16,
                                    Text = "X",
                                    Position = new Vector2(-15, 0),
                                    Action = () =>
                                    {
                                        if (Parent is Container container)
                                            container.Remove(this);

                                        CloseAction?.Invoke();
                                    }
                                }
                            }
                        }
                    }
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Padding = new MarginPadding { Top = 25 },
                    Children = new Drawable[]
                    {
                        MainContext
                    }
                }
            });
        }

        /// <summary>
        ///     Show Dialog
        /// </summary>
        public virtual void ShowDialog()
        {
            //TODO : Adding Dialog effect in here
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            State = DialogContainerStatus.Offscreen;
        }

        public event Action<DialogContainerStatus> StateChanged;

        #region Input

        protected override bool OnHover(HoverEvent e)
        {
            State = DialogContainerStatus.Onscreen;
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            State = DialogContainerStatus.Offscreen;
            base.OnHoverLost(e);
        }

        protected override bool OnDragStart(DragStartEvent e)
        {
            //TODO : implement
            return false;
            //return TitleBar.ReceiveMouseInputAt(e.ScreenSpaceMousePosition);
        }

        protected override bool OnDrag(DragEvent e)
        {
            Position += e.Delta;
            return base.OnDrag(e);
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            return true;
        }

        protected override bool OnClick(ClickEvent e)
        {
            return true;
        }

        #endregion
    }
}
