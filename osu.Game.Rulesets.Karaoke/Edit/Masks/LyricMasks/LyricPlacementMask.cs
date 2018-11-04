using osu.Framework.Input.Events;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Tools;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Edit.Masks.LyricMasks
{
    public class LyricPlacementMask : PlacementMask
    {
        public new Lyric HitObject => (Lyric)base.HitObject;

        public LyricPlacementMask()
            : base(new LyricCreator().CreateDefaultLyric())
        {
            //InternalChild = new HitCirclePiece(HitObject);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            // Fixes a 1-frame position discrpancy due to the first mouse move event happening in the next frame
            //HitObject.Position = GetContainingInputManager().CurrentState.Mouse.Position;
        }

        protected override bool OnClick(ClickEvent e)
        {
            HitObject.StartTime = EditorClock.CurrentTime;
            EndPlacement();
            return true;
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            //HitObject.Position = e.MousePosition;
            return true;
        }
    }
}
