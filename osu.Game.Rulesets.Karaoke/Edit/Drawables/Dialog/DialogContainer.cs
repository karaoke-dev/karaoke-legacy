using System;
using System.Linq;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Visualisation;
using osu.Framework;
using osu.Framework.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Dialog
{ 
    /// <summary>
    /// windows type
    /// </summary>
    public enum TreeContainerStatus
    {
        Onscreen,
        Offscreen
    }

    /// <summary>
    /// use as windows type dialog
    /// refrence : osu.Framework.Graphics.Visualisation
    /// </summary>
    public class DialogContainer : Container, IStateful<TreeContainerStatus>
    {
        //Title
        public String Title { get; set; }
        //Context
        protected override Container<Drawable> Content => scroll;
        public readonly ScrollContainer scroll;
        //Width
        private const float width = 400;
        //Height
        private const float height = 600;

        private readonly Container titleBar;
        private TreeContainerStatus state;
        public event Action<TreeContainerStatus> StateChanged;

        public TreeContainerStatus State
        {
            get { return state; }

            set
            {
                if (state == value)
                    return;
                state = value;

                switch (state)
                {
                    case TreeContainerStatus.Offscreen:
                        this.Delay(500).FadeTo(0.7f, 300);
                        break;
                    case TreeContainerStatus.Onscreen:
                        this.FadeIn(300, Easing.OutQuint);
                        break;
                }

                StateChanged?.Invoke(State);
            }
        }

        public DialogContainer()
        {
            Masking = true;
            CornerRadius = 5;
            Position = new Vector2(100, 100);

            AutoSizeAxes = Axes.X;
            Height = height;

            Color4 buttonBackground = new Color4(50, 50, 50, 255);
            Color4 buttonBackgroundHighlighted = new Color4(80, 80, 80, 255);

            AddRangeInternal(new Drawable[]
            {
                new Box
                {
                    Colour = new Color4(15, 15, 15, 255),
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
                        titleBar = new Container
                        {
                            RelativeSizeAxes = Axes.X,
                            Size = new Vector2(1, 25),
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.BlueViolet,
                                },
                                new SpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Text = Title,
                                    Alpha = 0.8f,
                                },
                            }
                        },
                    },
                },
                new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Padding = new MarginPadding { Top = 65 },
                    Children = new Drawable[]
                    {
                        scroll = new ScrollContainer
                        {
                            Padding = new MarginPadding(10),
                            RelativeSizeAxes = Axes.Y,
                            Width = width
                        },
                    }
                },
            });
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override bool OnHover(InputState state)
        {
            State = TreeContainerStatus.Onscreen;
            return true;
        }

        protected override void OnHoverLost(InputState state)
        {
            State = TreeContainerStatus.Offscreen;
            base.OnHoverLost(state);
        }

        protected override bool OnDragStart(InputState state) => titleBar.ReceiveMouseInputAt(state.Mouse.NativeState.Position);

        protected override bool OnDrag(InputState state)
        {
            Position += state.Mouse.Delta;
            return base.OnDrag(state);
        }

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args) => true;

        protected override bool OnClick(InputState state) => true;

        protected override void LoadComplete()
        {
            base.LoadComplete();
            State = TreeContainerStatus.Offscreen;
        }
    }
}
