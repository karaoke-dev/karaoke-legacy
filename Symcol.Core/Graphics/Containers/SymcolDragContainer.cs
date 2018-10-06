// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Input.Events;
using OpenTK;
using OpenTK.Input;

namespace Symcol.Core.Graphics.Containers
{
    public class SymcolDragContainer : SymcolContainer
    {
        protected override bool OnDragStart(DragStartEvent state) => true;

        public bool AllowLeftClickDrag { get; set; } = true;

        private bool drag;

        private Vector2 startPosition;

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            startPosition = Position;

            if (e.Button == MouseButton.Left && AllowLeftClickDrag || e.Button == MouseButton.Right)
                drag = true;

            return base.OnMouseDown(e);
        }

        protected override bool OnDrag(DragEvent e)
        {
            if (drag)
                Position = startPosition + e.ScreenSpaceMousePosition - e.MouseDownPosition;

            return base.OnDrag(e);
        }

        protected override bool OnMouseUp(MouseUpEvent e)
        {
            if (e.Button == MouseButton.Left && AllowLeftClickDrag || e.Button == MouseButton.Right)
                drag = false;

            return base.OnMouseUp(e);
        }
    }
}
