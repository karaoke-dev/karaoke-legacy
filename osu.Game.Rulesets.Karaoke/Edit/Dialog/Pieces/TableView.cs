// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Framework.Localisation;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Overlays.Music;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    ///     Dragable item source container
    /// </summary>
    public class TableView<TItem, TDrawable> : OsuScrollContainer where TItem : class, IHasPrimaryKey where TDrawable : TableViewCell<TItem>, new()
    {
        public TItem FirstVisibleSet => Items.FirstOrDefault(i => i.MatchingFilter)?.BeatmapSetInfo;
        public TItem NextSet => (Items.SkipWhile(i => !i.Selected).Skip(1).FirstOrDefault() ?? Items.FirstOrDefault())?.BeatmapSetInfo;
        public TItem PreviousSet => (Items.TakeWhile(i => !i.Selected).LastOrDefault() ?? Items.LastOrDefault())?.BeatmapSetInfo;
        public Action<TItem> OnSelect;

        public IEnumerable<TItem> Sets
        {
            get { return Items.Select(x => x.BeatmapSetInfo).ToList(); }
            set
            {
                Items.Clear();
                value.ForEach(AddBeatmapSet);
            }
        }

        public string SearchTerm
        {
            get => Search.SearchTerm;
            set => Search.SearchTerm = value;
        }

        public TItem SelectedSet
        {
            get { return Items.FirstOrDefault(i => i.Selected)?.BeatmapSetInfo; }
            set
            {
                foreach (var s in Items.Children)
                    s.Selected = s.BeatmapSetInfo.ID == value?.ID;
            }
        }

        protected readonly SearchContainer Search;
        protected readonly FillFlowContainer<TDrawable> Items;

        private Vector2 nativeDragPosition;
        private TDrawable draggedItem;

        public TableView()
        {
            Children = new Drawable[]
            {
                Search = new SearchContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Children = new Drawable[]
                    {
                        Items = new TableViewContainer<TItem, TDrawable>
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y
                        }
                    }
                }
            };
        }

        public virtual void AddBeatmapSet(TItem beatmapSet)
        {
            Items.Add(new TDrawable
            {
                BeatmapSetInfo = beatmapSet,
                OnSelect = set => OnSelect?.Invoke(set),
                Depth = Items.Count
            });
        }

        public virtual void RemoveBeatmapSet(TItem beatmapSet)
        {
            var itemToRemove = Items.FirstOrDefault(i => i.BeatmapSetInfo.ID == beatmapSet.ID);
            if (itemToRemove != null)
                Items.Remove(itemToRemove);
        }

        protected override bool OnDragStart(InputState state)
        {
            nativeDragPosition = state.Mouse.NativeState.Position;
            draggedItem = Items.FirstOrDefault(d => d.IsDraggable);
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
            var itemsPos = Items.ToLocalSpace(nativeDragPosition);

            var srcIndex = (int)draggedItem.Depth;

            // Find the last item with position < mouse position. Note we can't directly use
            // the item positions as they are being transformed
            float heightAccumulator = 0;
            var dstIndex = 0;
            for (; dstIndex < Items.Count; dstIndex++)
            {
                // Using BoundingBox here takes care of scale, paddings, etc...
                heightAccumulator += Items[dstIndex].BoundingBox.Height;
                if (heightAccumulator > itemsPos.Y)
                    break;
            }

            dstIndex = MathHelper.Clamp(dstIndex, 0, Items.Count - 1);

            if (srcIndex == dstIndex)
                return;

            if (srcIndex < dstIndex)
                for (var i = srcIndex + 1; i <= dstIndex; i++)
                    Items.ChangeChildDepth(Items[i], i - 1);
            else
                for (var i = dstIndex; i < srcIndex; i++)
                    Items.ChangeChildDepth(Items[i], i + 1);

            Items.ChangeChildDepth(draggedItem, dstIndex);
        }

        /// <summary>
        ///     searchable container
        /// </summary>
        public class TableViewContainer<TItem, TDrawable> : FillFlowContainer<TDrawable>, IHasFilterableChildren where TDrawable : TableViewCell<TItem> where TItem : IHasPrimaryKey
        {
            public IEnumerable<string> FilterTerms => new string[] { };

            public IEnumerable<IFilterable> FilterableChildren => Children;

            public bool MatchingFilter
            {
                set
                {
                    if (value)
                        InvalidateLayout();
                }
            }

            public TableViewContainer()
            {
                LayoutDuration = 200;
                LayoutEasing = Easing.OutQuint;
                Direction = FillDirection.Vertical;
            }

            // Compare with reversed ChildID and Depth
            protected override int Compare(Drawable x, Drawable y)
            {
                return base.Compare(y, x);
            }
        }
    }

    /// <summary>
    ///     drawable Item
    ///     From : osu.Game.Overlays.Music : PlaylistItem.cs
    /// </summary>
    public class TableViewCell<TItem> : Container, IFilterable, IDraggable where TItem : IHasPrimaryKey
    {
        public bool IsDraggable => handle.IsHovered;
        public Action<TItem> OnSelect;
        public virtual TItem BeatmapSetInfo { get; set; }

        public bool MatchingFilter
        {
            get => matching;
            set
            {
                if (matching == value) return;
                matching = value;
                this.FadeTo(matching ? 1 : 0, 200);
            }
        }

        public bool Selected
        {
            get => selected;
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

        public IEnumerable<string> FilterTerms { get; } = new List<string> { "add" };

        protected Color4 HoverColour;
        protected Color4 ArtistColour;
        private const float fade_duration = 100;

        private bool matching = true;

        private bool selected;

        private SpriteIcon handle;


        public TableViewCell()
        {
            Height = 20;
            RelativeSizeAxes = Axes.X;
            InitialView();
        }

        protected virtual void InitialView()
        {
            Children = new Drawable[]
            {
                handle = new PlaylistItemHandle
                {
                    Colour = OsuColour.FromHex(@"999")
                }
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

        [BackgroundDependencyLoader]
        private void load(OsuColour colours, LocalisationEngine localisation)
        {
            HoverColour = colours.Yellow;
            ArtistColour = colours.Gray9;
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
