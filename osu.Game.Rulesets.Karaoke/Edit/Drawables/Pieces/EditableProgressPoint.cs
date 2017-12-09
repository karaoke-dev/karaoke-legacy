using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces
{
    public class EditableProgressPoint : Drawable
    {
        //public 
        public ProgressPoint ProgressPoint { get; set; }
        public DrawableKaraokeThumbnail DrawableKaraokeThumbnail { get; set; }//Parent

        //protected value
        protected bool IsFocus = false;
        protected float? PressedPositionX;
        protected Color4 BackgroundIdolColor { get; set; } = Color4.White;
        protected Color4 BackgroundHoverColor { get; set; } = Color4.Purple;
        protected Color4 BackgroundPressColor { get; set; } = Color4.Blue;
        //protected culculater value
        public string ProgressText
        {
            get
            {
                int nowIndex = DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint.IndexOf(ProgressPoint);
                if (nowIndex == 0)
                    return DrawableKaraokeThumbnail.KaraokeObject.MainText.Text.Substring(ProgressPoint.CharIndex, 3);
                else
                    return "";
            }
        }
        public double RelativeToLastPointTime
        {
            get;
            set;
        }
        //Drawable component
        protected OsuSpriteText ProgressDrawableText { get; set; }
        

        public EditableProgressPoint(DrawableKaraokeThumbnail drawableKaraokeThumbnail)
        {
            DrawableKaraokeThumbnail = drawableKaraokeThumbnail;
            this.Colour = BackgroundIdolColor;
        }

        #region Input

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            this.Colour = BackgroundPressColor;
            PressedPositionX = GetXPointPosition(state);
            return base.OnMouseDown(state, args);
        }

        /// <summary>
        /// moving
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override bool OnMouseMove(InputState state)
        {
            if (state.Mouse.HasAnyButtonPressed)
            {
                //TODO : 1. Adjust position

                //TODO : 2. update DrawableEditableKaraokeObject
            }
            //TODO : Change background color

            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            PressedPositionX = null;
            this.Colour = BackgroundHoverColor;
            return base.OnMouseUp(state, args);
        }

        protected override bool OnHover(InputState state)
        {
            IsFocus = true;
            this.Colour = BackgroundHoverColor;
            return base.OnHover(state);
        }

        protected override void OnHoverLost(InputState state)
        {
            IsFocus = false;
            this.Colour = BackgroundIdolColor;
            base.OnHoverLost(state);
        }

        protected float GetXPointPosition(InputState state)
        {
            var mousePosition = this.ToLocalSpace(state.Mouse.NativeState.Position);
            return mousePosition.X;
        }

        #endregion



        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (IsFocus)
            {
                //delete itself
                if (args.Key == OpenTK.Input.Key.Delete)
                {
                    DrawableKaraokeThumbnail.DeletePoint(ProgressPoint);
                }
            }
            return base.OnKeyDown(state, args);
        }

        /// <summary>
        /// adjust position
        /// </summary>
        public void UpdatePosition()
        {

        }
    }
}
