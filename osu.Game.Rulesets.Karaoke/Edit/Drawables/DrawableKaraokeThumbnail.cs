// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using OpenTK.Input;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables
{
    /// <summary>
    /// Karaoke's Thumbnail
    /// will show the word's seperate word and seperate time
    /// </summary>
    public class DrawableKaraokeThumbnail : Container
    {
        public KaraokeObject KaraokeObject { get; set; }

        //if change zoon,call this
        public float Ratio = 0.3f;

        public float Zoon = 1;

        protected float PointYPosition = 30;

        //Start and end selected point
        public EditableProgressPoint HoverSelectedPoint { get; set; }

        protected EditableProgressPoint StartSelectedPoint { get; set; }
        protected EditableProgressPoint EndSelectedPoint { get; set; }
        protected bool IsDraging { get; set; }
        protected bool IsSelecting { get; set; } = false;

        protected FillFlowContainer<EditableProgressPoint> ListEditableProgressPoint { get; set; } = new FillFlowContainer<EditableProgressPoint>();

        public DrawableKaraokeThumbnail(KaraokeObject karaokeObject)
        {
            KaraokeObject = karaokeObject;
            Add(ListEditableProgressPoint);
            UpdateView();
        }

        /// <summary>
        /// update UI
        /// </summary>
        public void UpdateView()
        {
            //1. show the whole bar with start time and end time

            //2. fix time
            KaraokeObject.ListProgressPoint.FixTime();

            //3. show each point with text start and end time
            ListEditableProgressPoint.Direction = FillDirection.Horizontal;
            ListEditableProgressPoint.Clear();
            foreach (var single in KaraokeObject.ListProgressPoint)
            {
                var editableProgressPoint = new EditableProgressPoint(this, single);
                ListEditableProgressPoint.Add(editableProgressPoint);
            }

            //update position
            UpdatePosition();
            //update color
            UpdateColor();
        }

        /// <summary>
        /// just update progresspoint's position and startEndPosition
        /// </summary>
        public void UpdatePosition()
        {
            float totalRelativeTime = 0;

            //drawing position
            foreach (var single in ListEditableProgressPoint)
            {
                //update position
                var progressPoint = single.ProgressPoint;
                single.Width = ((float)progressPoint.RelativeTime - totalRelativeTime) * Ratio * Zoon;
                single.Height = 30;
                totalRelativeTime = (float)progressPoint.RelativeTime;
            }
            Width = totalRelativeTime * Ratio * Zoon;
        }

        /// <summary>
        /// update selected or dragging color
        /// </summary>
        public void UpdateColor()
        {
            //reset color
            foreach (var single in ListEditableProgressPoint)
            {
                single.ResetColor();
            }

            //update hover color
            if (!IsDraging && !IsSelecting)
                if (HoverSelectedPoint != null)
                    HoverSelectedPoint.Hover = true;

            //update select color
            var startIndex = GetObjectIndex(StartSelectedPoint);
            var endIndex = GetObjectIndex(EndSelectedPoint);
            for (int i = 0; i < ListEditableProgressPoint.Count; i++)
            {
                if (i >= startIndex && i <= endIndex)
                {
                    ListEditableProgressPoint[i].Selected = true;
                }
            }
        }

        /// <summary>
        /// update position
        /// </summary>
        public void UpdateTime(float deltaPosition)
        {
            var minimumTime = KaraokeObject.ListProgressPoint.MinimumTime;

            double deltaTime = deltaPosition / Ratio / Zoon;

            //filter invalid time
            var startIndex = GetObjectIndex(StartSelectedPoint);
            var endIndex = GetObjectIndex(EndSelectedPoint);
            /*
            double time = 0;
            for (int i = 0; i < ListEditableProgressPoint.Count; i++)
            {
                if (time + minimumTime > ListEditableProgressPoint[i].ProgressPoint.RelativeTime)
                    return;

                if (i >= startIndex && i <= endIndex)
                    time = ListEditableProgressPoint[i].ProgressPoint.RelativeTime + deltaPosition;
                else
                    time = ListEditableProgressPoint[i].ProgressPoint.RelativeTime;
            }
            */

            //update time
            for (int i = 0; i < ListEditableProgressPoint.Count; i++)
            {
                if (i >= startIndex && i <= endIndex)
                {
                    //not out of range
                    ListEditableProgressPoint[i].ProgressPoint.RelativeTime = ListEditableProgressPoint[i].ProgressPoint.RelativeTime + deltaPosition;
                }
            }

            KaraokeObject.ListProgressPoint.FixTime();
        }

        /// <summary>
        /// Delete single point
        /// </summary>
        public void DeletePoint(ProgressPoint point)
        {
            if (KaraokeObject.ListProgressPoint.Count > 1)
            {
                KaraokeObject.ListProgressPoint.Remove(point);
            }
            UpdateView();
        }

        public EditableProgressPoint GetPointedObjectByPosition(InputState state)
        {
            var mousePosition = ToLocalSpace(state.Mouse.NativeState.Position);

            foreach (var single in ListEditableProgressPoint)
            {
                if (single.Position.X + single.Width > mousePosition.X)
                    return single;
            }
            return null;
        }

        protected float GetXPointPosition(InputState state)
        {
            var mousePosition = ToLocalSpace(state.Mouse.NativeState.Position);
            return mousePosition.X;
        }

        protected bool IsSelectKeyPressed(InputState state)
        {
            //if press control,return true
            return state.Keyboard.Keys.Contains(Key.LShift);
        }

        //add s single point
        protected void PlusSelectedPoint(EditableProgressPoint newPoint)
        {
            if (StartSelectedPoint == null)
            {
                StartSelectedPoint = newPoint;
                EndSelectedPoint = newPoint;
                return;
            }
            var startIndex = GetObjectIndex(StartSelectedPoint);
            var endIndex = GetObjectIndex(EndSelectedPoint);
            if (GetObjectIndex(newPoint) < startIndex)
                StartSelectedPoint = newPoint;
            if (GetObjectIndex(newPoint) > endIndex)
                EndSelectedPoint = newPoint;

            if (startIndex > endIndex)
            {
                //switch two point
                var tempPoint = StartSelectedPoint;
                StartSelectedPoint = EndSelectedPoint;
                EndSelectedPoint = tempPoint;
            }
        }

        protected void ClearSelectedPoint()
        {
            StartSelectedPoint = null;
            EndSelectedPoint = null;
        }

        protected int GetObjectIndex(EditableProgressPoint point)
        {
            return point != null ? ListEditableProgressPoint.IndexOf(point) : -1;
        }

        #region Input

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            //get selected point
            var selectedPoint = GetPointedObjectByPosition(state);
            if (selectedPoint != null)
            {
                if (IsSelectKeyPressed(state))
                {
                    PlusSelectedPoint(selectedPoint);
                }
                else
                {
                    if (StartSelectedPoint == null)
                    {
                        PlusSelectedPoint(selectedPoint);
                    }
                    IsDraging = true;
                }
            }
            UpdateColor();

            return base.OnMouseDown(state, args);
        }

        /// <summary>
        /// moving
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override bool OnMouseMove(InputState state)
        {
            //not selecting point,means moving
            if (!IsSelectKeyPressed(state) && IsDraging)
            {
                //Adjust position
                UpdateTime(state.Mouse.Delta.X * 3);
                UpdatePosition();
            }

            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            //if didn't in choose mode
            if (!IsSelectKeyPressed(state))
            {
                IsDraging = false;
                ClearSelectedPoint();
                UpdatePosition();
                UpdateColor();
            }

            return base.OnMouseUp(state, args);
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            IsSelecting = IsSelectKeyPressed(state);
            UpdateColor();
            return base.OnKeyDown(state, args);
        }

        protected override bool OnKeyUp(InputState state, KeyUpEventArgs args)
        {
            IsSelecting = IsSelectKeyPressed(state);
            UpdateColor();
            return base.OnKeyUp(state, args);
        }

        #endregion
    }
}
