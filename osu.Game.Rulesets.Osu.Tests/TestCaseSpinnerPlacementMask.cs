// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Edit.Masks.SpinnerMasks;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Osu.Tests
{
    public class TestCaseSpinnerPlacementMask : HitObjectPlacementMaskTestCase
    {
        protected override DrawableHitObject CreateHitObject(HitObject hitObject) => new DrawableSpinner((Spinner)hitObject);

        protected override PlacementMask CreateMask() => new SpinnerPlacementMask();
    }
}
