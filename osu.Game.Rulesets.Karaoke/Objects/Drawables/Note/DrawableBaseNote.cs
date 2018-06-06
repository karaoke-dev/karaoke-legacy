using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public abstract class DrawableBaseNote<TObject> : DrawableHitObject<BaseLyric>
        where TObject : BaseLyric
    {
        public new TObject HitObject;

        protected DrawableBaseNote(TObject hitObject)
            : base(hitObject)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;

            HitObject = hitObject;
        }
    }
}
