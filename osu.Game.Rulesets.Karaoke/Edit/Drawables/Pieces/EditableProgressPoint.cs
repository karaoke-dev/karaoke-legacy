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

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces
{
    public class EditableProgressPoint : Drawable
    {
        public OsuSpriteText ProgressText { get; set; }
        public double RelativeTime { get; set; }
        public ProgressPoint ProgressPoint { get; set; }
        public DrawableKaraokeThumbnail DrawableKaraokeThumbnail { get; set; }

        protected bool IsFocus = false;

        public EditableProgressPoint(DrawableKaraokeThumbnail drawableKaraokeThumbnail)
        {
            DrawableKaraokeThumbnail = drawableKaraokeThumbnail;
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

        protected override bool OnHover(InputState state)
        {
            IsFocus = true;
            return base.OnHover(state);
        }

        protected override void OnHoverLost(InputState state)
        {
            IsFocus = false;
            base.OnHoverLost(state);
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

        /// <summary>
        /// adjust position
        /// </summary>
        public void UpdatePosition()
        {

        }
    }
}
