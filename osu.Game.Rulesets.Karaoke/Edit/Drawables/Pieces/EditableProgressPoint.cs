using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces
{
    public class EditableProgressPoint : Container
    {
        //public 
        public ProgressPoint ProgressPoint { get; set; }
        public DrawableKaraokeThumbnail DrawableKaraokeThumbnail { get; set; }//Parent
        public int IndexOfObject => DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint.IndexOf(ProgressPoint);

        //Drawable component
        protected OsuSpriteText ProgressDrawableText { get; set; }
        protected Box Background { get; set; } = new Box() { Height=50,};
        protected Box StartLine { get; set; } = new Box()
        {
            Width = 3,
            Height = 50,
        };

        //protected value
        protected bool IsFocus = false;
        protected float? PressedRelativePositionX;
        protected Color4 BackgroundIdolColor { get; set; } = Color4.Black;
        protected Color4 BackgroundHoverColor { get; set; } = Color4.Purple;
        protected Color4 BackgroundPressColor { get; set; } = Color4.Blue;
        protected float ratio = 0.3f;

        //protected culculater value
        public string ProgressText
        {
            get
            {
                if (IndexOfObject == 0)
                    return DrawableKaraokeThumbnail.KaraokeObject.MainText.Text.Substring(0, ProgressPoint.CharIndex + 1);
                else
                {
                    var thisCharIndex = ProgressPoint.CharIndex;
                    var lastTime = DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint[IndexOfObject - 1].CharIndex;
                    return DrawableKaraokeThumbnail.KaraokeObject.MainText.Text.Substring(lastTime + 1, thisCharIndex- lastTime);
                }
            }
        }
        public double RelativeToLastPointTime
        {
            get
            {
                if (IndexOfObject == 0)
                    return DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint[IndexOfObject].RelativeTime;
                else
                {
                    var thisTime = DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint[IndexOfObject].RelativeTime;
                    var lastTime = DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint[IndexOfObject - 1].RelativeTime;
                    return thisTime - lastTime;
                } 
            }
            set
            {
                if (IndexOfObject == 0)
                    DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint[IndexOfObject].RelativeTime = value;
                else
                {
                    var lastTime = DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint[IndexOfObject - 1].RelativeTime;
                    DrawableKaraokeThumbnail.KaraokeObject.ListProgressPoint[IndexOfObject].RelativeTime = lastTime + value;
                }
            }
        }
        protected float ThisViewWidth
        {
            get => (float)RelativeToLastPointTime * ratio;
            set
            {
                //value cannot <50
                if (value <= 50)
                    return;

                RelativeToLastPointTime = value / RelativeToLastPointTime;
                this.Width = value;

                //update last object
            }
        }

        public EditableProgressPoint(DrawableKaraokeThumbnail drawableKaraokeThumbnail, ProgressPoint progressPoin)
        {
            DrawableKaraokeThumbnail = drawableKaraokeThumbnail;
            ProgressPoint = progressPoin;
            Background.Colour = BackgroundIdolColor;
            StartLine.Colour = Color4.Pink;
            ProgressDrawableText = new OsuSpriteText()
            {
                Text = ProgressText,
                Position=new OpenTK.Vector2(5,5), 
            };
            this.Height = 30;
            UpdatePosition();
            this.Add(Background);
            this.Add(StartLine);
            this.Add(ProgressDrawableText);
        }

        #region Input

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            PressedRelativePositionX = this.Width - GetXPointPosition(state);
            Background.Colour = BackgroundPressColor;
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
                try
                {
                    //TODO : 1. Adjust position
                    this.ThisViewWidth = PressedRelativePositionX.Value + GetXPointPosition(state);
                    //TODO : 2. update DrawableEditableKaraokeObject
                }
                catch
                {

                }
            }

            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            PressedRelativePositionX = null;
            Background.Colour = BackgroundHoverColor;
            return base.OnMouseUp(state, args);
        }

        protected override bool OnHover(InputState state)
        {
            IsFocus = true;
            Background.Colour = BackgroundHoverColor;
            return base.OnHover(state);
        }

        protected override void OnHoverLost(InputState state)
        {
            IsFocus = false;
            Background.Colour = BackgroundIdolColor;
            base.OnHoverLost(state);
        }

        protected float GetXPointPosition(InputState state)
        {
            var mousePosition = this.ToLocalSpace(state.Mouse.NativeState.Position);
            return mousePosition.X;
        }

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
        #endregion

        #region Override
        //will update view
        public override float Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                Background.Width = Width;
            }
        }

        public override float Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
                Background.Height = Height;
                StartLine.Height = Height;
            }
        }
        #endregion

        /// <summary>
        /// adjust position
        /// </summary>
        public void UpdatePosition()
        {
            this.Width = ThisViewWidth;
        }
    }
}
