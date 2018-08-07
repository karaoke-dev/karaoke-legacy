using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Note;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Selection.Overlays
{
    public class NoteMask : HitObjectMask
    {
        public NoteMask(DrawableEditableKaraokeNoteGroup hitObject)
            : base(hitObject)
        {
        }
    }
}
