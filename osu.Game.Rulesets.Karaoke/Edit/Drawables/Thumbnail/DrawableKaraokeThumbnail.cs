// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Input.States;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using OpenTK.Input;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Thumbnail
{
    /// <summary>
    ///     Karaoke's Thumbnail
    ///     will show the word's seperate word and seperate time
    /// </summary>
    public class DrawableKaraokeThumbnail : Container
    {
        //if change zoon,call this
        public float Ratio = 0.3f;

        public float Zoon = 1;
        public BaseLyric Lyric { get; set; }

        //Start and end selected point
        public EditableProgressPoint HoverSelectedPoint { get; set; }

        protected float PointYPosition = 30;

        protected EditableProgressPoint StartSelectedPoint { get; set; }
        protected EditableProgressPoint EndSelectedPoint { get; set; }
        protected bool IsDraging { get; set; }
        protected bool IsSelecting { get; set; }

        protected FillFlowContainer<EditableProgressPoint> ListEditableProgressPoint { get; set; } = new FillFlowContainer<EditableProgressPoint>();

        public DrawableKaraokeThumbnail(BaseLyric lyric)
        {
            Lyric = lyric;
            Add(ListEditableProgressPoint);
            UpdateView();
        }

        /// <summary>
        ///     update UI
        /// </summary>
        public void UpdateView()
        {
            //1. show the whole bar with start time and end time

            //2. fix time
            Lyric.TimeLines.FixTime();

            //3. show each point with text start and end time
            ListEditableProgressPoint.Direction = FillDirection.Horizontal;
            ListEditableProgressPoint.Clear();


            foreach (var single in Lyric.TimeLines)
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
        ///     just update progresspoint's position and startEndPosition
        /// </summary>
        public void UpdatePosition()
        {
            float totalRelativeTime = 0;

            //drawing position
            foreach (var single in ListEditableProgressPoint)
            {
                //update position
                var progressPoint = single.LyricProgressPoint;
                single.Width = ((float)progressPoint.Value.RelativeTime - totalRelativeTime) * Ratio * Zoon;
                single.Height = 30;
                totalRelativeTime = (float)progressPoint.Value.RelativeTime;
            }

            Width = totalRelativeTime * Ratio * Zoon;
        }

        /// <summary>
        ///     update selected or dragging color
        /// </summary>
        public void UpdateColor()
        {
            //reset color
            foreach (var single in ListEditableProgressPoint)
                single.ResetColor();

            //update hover color
            if (!IsDraging && !IsSelecting)
                if (HoverSelectedPoint != null)
                    HoverSelectedPoint.Hover = true;

            //update select color
            var startIndex = GetObjectIndex(StartSelectedPoint);
            var endIndex = GetObjectIndex(EndSelectedPoint);
            for (var i = 0; i < ListEditableProgressPoint.Count; i++)
                if (i >= startIndex && i <= endIndex)
                    ListEditableProgressPoint[i].Selected = true;
        }

        /// <summary>
        ///     update position
        /// </summary>
        public void UpdateTime(float deltaPosition)
        {
            var minimumTime = Lyric.TimeLines.MinimumTime;

            double deltaTime = deltaPosition / Ratio / Zoon;

            //filter invalid time
            var startIndex = GetObjectIndex(StartSelectedPoint);
            var endIndex = GetObjectIndex(EndSelectedPoint);
            /*
            double time = 0;
            for (int i = 0; i < ListEditableProgressPoint.Count; i++)
            {
                if (time + minimumTime > ListEditableProgressPoint[i].LyricProgressPoint.RelativeTime)
                    return;

                if (i >= startIndex && i <= endIndex)
                    time = ListEditableProgressPoint[i].LyricProgressPoint.RelativeTime + deltaPosition;
                else
                    time = ListEditableProgressPoint[i].LyricProgressPoint.RelativeTime;
            }
            */

            //update time
            for (var i = 0; i < ListEditableProgressPoint.Count; i++)
                if (i >= startIndex && i <= endIndex)
                    ListEditableProgressPoint[i].LyricProgressPoint.Value.RelativeTime = ListEditableProgressPoint[i].LyricProgressPoint.Value.RelativeTime + deltaPosition;

            Lyric.TimeLines.FixTime();
        }

        /// <summary>
        ///     Delete single point
        /// </summary>
        public void DeletePoint(KeyValuePair<TimeLineIndex, TimeLine> point)
        {
            if (Lyric.TimeLines.Count > 1)
                Lyric.TimeLines.Remove(point.Key);

            UpdateView();
        }

        public EditableProgressPoint GetPointedObjectByPosition(MouseEvent e)
        {
            var mousePosition = ToLocalSpace(e.ScreenSpaceMousePosition);

            foreach (var single in ListEditableProgressPoint)
                if (single.Position.X + single.Width > mousePosition.X)
                    return single;

            return null;
        }

        protected bool IsSelectKeyPressed(UIEvent e)
        {
            if (e is KeyboardEvent kayboardEvent)
            {
                //if press control,return true
                return kayboardEvent.PressedKeys.Contains(Key.LShift);
            }
            return false;
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

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            //get selected point
            var selectedPoint = GetPointedObjectByPosition(e);
            if (selectedPoint != null)
                if (IsSelectKeyPressed(e))
                {
                    PlusSelectedPoint(selectedPoint);
                }
                else
                {
                    if (StartSelectedPoint == null)
                        PlusSelectedPoint(selectedPoint);

                    IsDraging = true;
                }

            UpdateColor();

            return base.OnMouseDown(e);
        }

        /// <summary>
        ///     moving
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            //not selecting point,means moving
            if (!IsSelectKeyPressed(e) && IsDraging)
            {
                //Adjust position
                UpdateTime(e.Delta.X * 3);
                UpdatePosition();
            }

            return base.OnMouseMove(e);
        }

        protected override bool OnMouseUp(MouseUpEvent e)
        {
            //if didn't in choose mode
            if (!IsSelectKeyPressed(e))
            {
                IsDraging = false;
                ClearSelectedPoint();
                UpdatePosition();
                UpdateColor();
            }

            return base.OnMouseUp(e);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            IsSelecting = IsSelectKeyPressed(e);
            UpdateColor();
            return base.OnKeyDown(e);
        }

        protected override bool OnKeyUp(KeyUpEvent e)
        {
            IsSelecting = IsSelectKeyPressed(e);
            UpdateColor();
            return base.OnKeyUp(e);
        }

        #endregion
    }
}
