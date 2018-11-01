using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit.Masks.LyricMasks
{
    public class LyricPlacementMask : PlacementMask
    {
        public new BaseLyric HitObject => (BaseLyric)base.HitObject;

        public LyricPlacementMask()
            : base(new BaseLyric())
        {
            //InternalChild = new HitCirclePiece(HitObject);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // Fixes a 1-frame position discrpancy due to the first mouse move event happening in the next frame
            HitObject.Position = GetContainingInputManager().CurrentState.Mouse.Position;
        }

        protected override bool OnClick(ClickEvent e)
        {
            HitObject.StartTime = EditorClock.CurrentTime;
            EndPlacement();
            return true;
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            HitObject.Position = e.MousePosition;
            return true;
        }
    }
}
