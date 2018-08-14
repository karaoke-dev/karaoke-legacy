// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    public abstract class DrawableBaseNote<TObject> : DrawableHitObject<BaseLyric>
        where TObject : BaseLyric
    {
        protected DrawableBaseNote(TObject hitObject)
            : base(hitObject)
        {
            Anchor = Anchor.CentreLeft;
            Origin = Anchor.CentreLeft;
        }
    }
}
