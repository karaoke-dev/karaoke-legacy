using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework.Input.Events;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.Edit.Blueprints
{
    public class KaraokeSelectionBlueprint : SelectionBlueprint
    {
        public KaraokeSelectionBlueprint(DrawableHitObject hitObject)
            : base(hitObject)
        {
        }

        public override void AdjustPosition(DragEvent dragEvent)
        {
            
        }
    }
}
