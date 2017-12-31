// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.Edit.Dialog.Pieces
{
    /// <summary>
    /// use to show the size and spacing of the template / single Karaoke object
    /// </summary>
    public class DrawableKaraokeTemplate : DrawableKaraokeObject
    {
        public DrawableKaraokeTemplate(KaraokeObject hitObject, KaraokeTemplate template)
            : base(hitObject)
        {
            Template = template;
        }

        /// <summary>
        /// update drawable
        /// </summary>
        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();

            //1. get position (mainText and subText and translate text position)

            //2. update position

            //3. draw line (Zero position,)
        }
    }
}
