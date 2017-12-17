using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// Dragable item source container
    /// </summary>
    public class ItemsScrollContainer : OsuScrollContainer
    {
        public Action<IHasPrimaryKey> OnSelect;

        private readonly SearchContainer search;
        private readonly FillFlowContainer<DrawableItems> items;

        public ItemsScrollContainer()
        {
            Children = new Drawable[]
            {
                search = new SearchContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        items = new ItemSearchContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                        },
                    }
                }
            };
        }

        public IEnumerable<IHasPrimaryKey> Sets
        {
            get { return items.Select(x => x.BeatmapSetInfo).ToList(); }
            set
            {
                items.Clear();
                value.ForEach(AddBeatmapSet);
            }
        }

        public string SearchTerm
        {
            get { return search.SearchTerm; }
            set { search.SearchTerm = value; }
        }

        public virtual void AddBeatmapSet(IHasPrimaryKey beatmapSet)
        {
            items.Add(new DrawableItems(beatmapSet)
            {
                OnSelect = set => OnSelect?.Invoke(set),
                Depth = items.Count
            });
        }

        public virtual void RemoveBeatmapSet(IHasPrimaryKey beatmapSet)
        {
            var itemToRemove = items.FirstOrDefault(i => i.BeatmapSetInfo.ID == beatmapSet.ID);
            if (itemToRemove != null)
                items.Remove(itemToRemove);
        }

        public IHasPrimaryKey SelectedSet
        {
            get { return items.FirstOrDefault(i => i.Selected)?.BeatmapSetInfo; }
            set
            {
                foreach (DrawableItems s in items.Children)
                    s.Selected = s.BeatmapSetInfo.ID == value?.ID;
            }
        }

        public IHasPrimaryKey FirstVisibleSet => items.FirstOrDefault(i => i.MatchingFilter)?.BeatmapSetInfo;
        public IHasPrimaryKey NextSet => (items.SkipWhile(i => !i.Selected).Skip(1).FirstOrDefault() ?? items.FirstOrDefault())?.BeatmapSetInfo;
        public IHasPrimaryKey PreviousSet => (items.TakeWhile(i => !i.Selected).LastOrDefault() ?? items.LastOrDefault())?.BeatmapSetInfo;

        private Vector2 nativeDragPosition;
        private DrawableItems draggedItem;

        protected override bool OnDragStart(InputState state)
        {
            nativeDragPosition = state.Mouse.NativeState.Position;
            draggedItem = items.FirstOrDefault(d => d.IsDraggable);
            return draggedItem != null || base.OnDragStart(state);
        }

        protected override bool OnDrag(InputState state)
        {
            nativeDragPosition = state.Mouse.NativeState.Position;
            if (draggedItem == null)
                return base.OnDrag(state);
            return true;
        }

        protected override bool OnDragEnd(InputState state)
        {
            nativeDragPosition = state.Mouse.NativeState.Position;
            var handled = draggedItem != null || base.OnDragEnd(state);
            draggedItem = null;

            return handled;
        }

        protected override void Update()
        {
            base.Update();

            if (draggedItem == null)
                return;

            updateScrollPosition();
            updateDragPosition();
        }

        private void updateScrollPosition()
        {
            const float start_offset = 10;
            const double max_power = 50;
            const double exp_base = 1.05;

            var localPos = ToLocalSpace(nativeDragPosition);

            if (localPos.Y < start_offset)
            {
                if (Current <= 0)
                    return;

                var power = Math.Min(max_power, Math.Abs(start_offset - localPos.Y));
                ScrollBy(-(float)Math.Pow(exp_base, power));
            }
            else if (localPos.Y > DrawHeight - start_offset)
            {
                if (IsScrolledToEnd())
                    return;

                var power = Math.Min(max_power, Math.Abs(DrawHeight - start_offset - localPos.Y));
                ScrollBy((float)Math.Pow(exp_base, power));
            }
        }

        private void updateDragPosition()
        {
            var itemsPos = items.ToLocalSpace(nativeDragPosition);

            int srcIndex = (int)draggedItem.Depth;

            // Find the last item with position < mouse position. Note we can't directly use
            // the item positions as they are being transformed
            float heightAccumulator = 0;
            int dstIndex = 0;
            for (; dstIndex < items.Count; dstIndex++)
            {
                // Using BoundingBox here takes care of scale, paddings, etc...
                heightAccumulator += items[dstIndex].BoundingBox.Height;
                if (heightAccumulator > itemsPos.Y)
                    break;
            }

            dstIndex = MathHelper.Clamp(dstIndex, 0, items.Count - 1);

            if (srcIndex == dstIndex)
                return;

            if (srcIndex < dstIndex)
            {
                for (int i = srcIndex + 1; i <= dstIndex; i++)
                    items.ChangeChildDepth(items[i], i - 1);
            }
            else
            {
                for (int i = dstIndex; i < srcIndex; i++)
                    items.ChangeChildDepth(items[i], i + 1);
            }

            items.ChangeChildDepth(draggedItem, dstIndex);
        }

        /// <summary>
        /// searchable container
        /// </summary>
        public class ItemSearchContainer : FillFlowContainer<DrawableItems>, IHasFilterableChildren
        {
            public IEnumerable<string> FilterTerms => new string[] { };

            public bool MatchingFilter
            {
                set
                {
                    if (value)
                        InvalidateLayout();
                }
            }

            // Compare with reversed ChildID and Depth
            protected override int Compare(Drawable x, Drawable y) => base.Compare(y, x);

            public IEnumerable<IFilterable> FilterableChildren => Children;

            public ItemSearchContainer()
            {
                LayoutDuration = 200;
                LayoutEasing = Easing.OutQuint;
            }
        }

        /// <summary>
        /// drawable Item
        /// </summary>
        public class DrawableItems : Container, IFilterable, IDraggable
        {
            private const float fade_duration = 100;
            public IHasPrimaryKey BeatmapSetInfo { get; set; }
            public DrawableItems(IHasPrimaryKey beatmapSetInfo)
            {
                BeatmapSetInfo = beatmapSetInfo;
            }

            private bool matching = true;

            public bool MatchingFilter
            {
                get { return matching; }
                set
                {
                    if (matching == value) return;
                    matching = value;
                    this.FadeTo(matching ? 1 : 0, 200);
                }
            }

            private bool selected;
            public bool Selected
            {
                get { return selected; }
                set
                {
                    if (value == selected) return;
                    selected = value;

                    FinishTransforms(true);
                    /*
                    foreach (SpriteText s in titleSprites)
                        s.FadeColour(Selected ? hoverColour : Color4.White, fade_duration);
                        */
                }
            }

            private SpriteIcon handle;
            public bool IsDraggable => handle.IsHovered;

            public IEnumerable<string> FilterTerms { get; private set; }
            public Action<IHasPrimaryKey> OnSelect;

            private Color4 hoverColour;
            private Color4 artistColour;

            [BackgroundDependencyLoader]
            private void load(OsuColour colours, LocalisationEngine localisation)
            {
                hoverColour = colours.Yellow;
                artistColour = colours.Gray9;


                Children = new Drawable[]
                {
                    handle = new PlaylistItemHandle
                    {
                        Colour = colours.Gray5
                    },
                };
            }

            protected override bool OnHover(InputState state)
            {
                handle.FadeIn(fade_duration);

                return base.OnHover(state);
            }

            protected override void OnHoverLost(InputState state)
            {
                handle.FadeOut(fade_duration);
            }

            protected override bool OnClick(InputState state)
            {
                OnSelect?.Invoke(BeatmapSetInfo);
                return true;
            }

            private class PlaylistItemHandle : SpriteIcon
            {

                public PlaylistItemHandle()
                {
                    Anchor = Anchor.TopLeft;
                    Origin = Anchor.TopLeft;
                    Size = new Vector2(12);
                    Icon = FontAwesome.fa_bars;
                    Alpha = 0f;
                    Margin = new MarginPadding { Left = 5, Top = 2 };
                }
            }
        }
    }
}
