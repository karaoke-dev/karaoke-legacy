// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Edit.Tools;
using osu.Game.Rulesets.Osu.Edit.Masks.SliderMasks;
using osu.Game.Rulesets.Osu.Objects;

namespace osu.Game.Rulesets.Osu.Edit
{
    public class SliderCompositionTool : HitObjectCompositionTool
    {
        public SliderCompositionTool()
            : base(nameof(Slider))
        {
        }

        public override PlacementMask CreatePlacementMask() => new SliderPlacementMask();
    }
}
