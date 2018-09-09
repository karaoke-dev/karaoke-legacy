// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Input.EventArgs;
using osu.Framework.Input.States;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Pieces;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric.Pieces
{
    public class EditableLyricContainer : LyricContainer
    {
        public int? HoverIndex { get; set; }
        public int? StartSelectIndex { get; set; }
        public int? EndSelectIndex { get; set; }

        public Color4 HoverColor { get; set; } = Color4.Red;
        public Color4 SelectColor { get; set; } = Color4.Purple;

        public Action<TimeLineIndex> AddPointAction;


        #region Input

        protected bool IsDrag;
        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            IsDrag = true;
            var index = GetPointedText(state);
            StartSelectIndex = index;

            return base.OnMouseDown(state, args);
        }

        //detect whith text is picked
        protected override bool OnMouseMove(InputState state)
        {
            if (!IsDrag)
            {
                var index = GetPointedText(state);
                HoverIndex = index;
            }
            else
            {
                var index = GetPointedText(state);
                EndSelectIndex = index;
            }

            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            IsDrag = false;
            var index = new TimeLineIndex(GetPointedText(state));
            AddPointAction.Invoke(index);
            StartSelectIndex = null;
            EndSelectIndex = null;
            return base.OnMouseUp(state, args);
        }

        protected override void OnHoverLost(InputState state)
        {
            base.OnHoverLost(state);
            HoverIndex = null;

            //reset drawable status
            Reset();
        }

        protected int GetPointedText(InputState state)
        {
            var mousePosition = ToLocalSpace(state.Mouse.NativeState.Position);
            return GetIndexByPosition(mousePosition.X);
        }

        #endregion

        protected override void Update()
        {
            Reset();
            base.Update();
        }

        protected void Reset()
        {
            base.Update();
            foreach (var single in Children)
            {
                single.Colour = new Color4(0f, 0f, 0f, 0f);
                single.Alpha = 0.001f;
            }

            try
            {
                if (HoverIndex != null)
                {
                    Children[HoverIndex.Value].Colour = HoverColor;
                    Children[HoverIndex.Value].Alpha = 1;
                }

                if (StartSelectIndex != null)
                    if (EndSelectIndex != null)
                    {
                        if (StartSelectIndex <= EndSelectIndex)
                            for (var i = StartSelectIndex.Value; i <= EndSelectIndex; i++)
                            {
                                Children[i].Colour = SelectColor;
                                Children[i].Alpha = 1;
                            }
                        else
                            for (var i = EndSelectIndex.Value; i <= StartSelectIndex; i++)
                            {
                                Children[i].Colour = SelectColor;
                                Children[i].Alpha = 1;
                            }
                    }
                    else
                    {
                        Children[StartSelectIndex.Value].Colour = SelectColor;
                        Children[StartSelectIndex.Value].Alpha = 1;
                    }
            }
            catch
            {
            }
        }

        protected int GetIndexByPosition(float xPosition)
        {
            float sum = 0;
            for(int i=0;i< Children.Count;i++)
            {
                var width = Children[i].Width;
                if (sum + width > xPosition)
                    return i;

                sum = sum + width;
            }

            return 1;
        }
    }
}
