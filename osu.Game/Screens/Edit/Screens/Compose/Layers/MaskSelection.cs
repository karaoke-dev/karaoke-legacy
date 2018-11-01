﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;
using osu.Game.Graphics;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Types;
using OpenTK;
using OpenTK.Input;

namespace osu.Game.Screens.Edit.Screens.Compose.Layers
{
    /// <summary>
    /// A box which surrounds <see cref="SelectionMask"/>s and provides interactive handles, context menus etc.
    /// </summary>
    public class MaskSelection : CompositeDrawable
    {
        public const float BORDER_RADIUS = 2;

        private readonly List<SelectionMask> selectedMasks;

        private Drawable outline;

        [Resolved]
        private IPlacementHandler placementHandler { get; set; }

        public MaskSelection()
        {
            selectedMasks = new List<SelectionMask>();

            RelativeSizeAxes = Axes.Both;
            AlwaysPresent = true;
            Alpha = 0;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            InternalChild = outline = new Container
            {
                Masking = true,
                BorderThickness = BORDER_RADIUS,
                BorderColour = colours.Yellow,
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    AlwaysPresent = true,
                    Alpha = 0
                }
            };
        }

        #region User Input Handling

        public void HandleDrag(SelectionMask m, Vector2 delta, InputState state)
        {
            // Todo: Various forms of snapping

            foreach (var mask in selectedMasks)
            {
                switch (mask.HitObject.HitObject)
                {
                    case IHasEditablePosition editablePosition:
                        editablePosition.OffsetPosition(delta);
                        break;
                }
            }
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Repeat)
                return base.OnKeyDown(e);

            switch (e.Key)
            {
                case Key.Delete:
                    foreach (var h in selectedMasks.ToList())
                        placementHandler.Delete(h.HitObject.HitObject);
                    return true;
            }

            return base.OnKeyDown(e);
        }

        #endregion

        #region Selection Handling

        /// <summary>
        /// Bind an action to deselect all selected masks.
        /// </summary>
        public Action DeselectAll { private get; set; }

        /// <summary>
        /// Handle a mask becoming selected.
        /// </summary>
        /// <param name="mask">The mask.</param>
        public void HandleSelected(SelectionMask mask) => selectedMasks.Add(mask);

        /// <summary>
        /// Handle a mask becoming deselected.
        /// </summary>
        /// <param name="mask">The mask.</param>
        public void HandleDeselected(SelectionMask mask)
        {
            selectedMasks.Remove(mask);

            // We don't want to update visibility if > 0, since we may be deselecting masks during drag-selection
            if (selectedMasks.Count == 0)
                UpdateVisibility();
        }

        /// <summary>
        /// Handle a mask requesting selection.
        /// </summary>
        /// <param name="mask">The mask.</param>
        public void HandleSelectionRequested(SelectionMask mask, InputState state)
        {
            if (state.Keyboard.ControlPressed)
            {
                if (mask.IsSelected)
                    mask.Deselect();
                else
                    mask.Select();
            }
            else
            {
                if (mask.IsSelected)
                    return;

                DeselectAll?.Invoke();
                mask.Select();
            }

            UpdateVisibility();
        }

        #endregion

        /// <summary>
        /// Updates whether this <see cref="MaskSelection"/> is visible.
        /// </summary>
        internal void UpdateVisibility()
        {
            if (selectedMasks.Count > 0)
                Show();
            else
                Hide();
        }

        protected override void Update()
        {
            base.Update();

            if (selectedMasks.Count == 0)
                return;

            // Move the rectangle to cover the hitobjects
            var topLeft = new Vector2(float.MaxValue, float.MaxValue);
            var bottomRight = new Vector2(float.MinValue, float.MinValue);

            bool hasSelection = false;

            foreach (var mask in selectedMasks)
            {
                topLeft = Vector2.ComponentMin(topLeft, ToLocalSpace(mask.SelectionQuad.TopLeft));
                bottomRight = Vector2.ComponentMax(bottomRight, ToLocalSpace(mask.SelectionQuad.BottomRight));
            }

            topLeft -= new Vector2(5);
            bottomRight += new Vector2(5);

            outline.Size = bottomRight - topLeft;
            outline.Position = topLeft;
        }
    }
}
