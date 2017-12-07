using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Framework.Input;
using OpenTK.Graphics;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Framework.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables
{
    class DrawableEditableKaraokeObject : DrawableKaraokeObject
    {
        protected EditableMainKaraokeText EditableMainKaraokeText { get; set; } = new EditableMainKaraokeText(null);
        protected bool IsDrag = false;

        public DrawableEditableKaraokeObject(KaraokeObject hitObject) : base(hitObject)
        {
            Add(EditableMainKaraokeText);
        }

        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();
            EditableMainKaraokeText.TextObject = Template?.MainText + KaraokeObject.MainText;
            EditableMainKaraokeText.Alpha = 1f;
        }

        //detect whith text is picked
        protected override bool OnMouseMove(InputState state)
        {
            if (!IsDrag)
            {
                int index = GetPointedText(state);
                EditableMainKaraokeText.HoverIndex = index;
            }
            else
            {
                int index = GetPointedText(state);
                EditableMainKaraokeText.EndSelectIndex = index;
            }
            return base.OnMouseMove(state);
        }

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            IsDrag = true;
            int index = GetPointedText(state);
            EditableMainKaraokeText.StartSelectIndex = index;

            return base.OnMouseDown(state, args);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            IsDrag = false;
            int index = GetPointedText(state);
            EditableMainKaraokeText.StartSelectIndex = null;
            EditableMainKaraokeText.EndSelectIndex = null;

            return base.OnMouseUp(state, args);
        }

        protected int GetPointedText(InputState state)
        {
            var mousePosition = this.ToLocalSpace(state.Mouse.NativeState.Position);
            return EditableMainKaraokeText.GetIndexByPosition(mousePosition.X); ;
        }
    }
}
