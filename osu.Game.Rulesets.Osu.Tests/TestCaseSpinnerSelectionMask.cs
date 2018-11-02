// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Beatmaps.ControlPoints;
using osu.Game.Rulesets.Edit;
using osu.Game.Rulesets.Osu.Edit.Masks.SpinnerMasks;
using osu.Game.Rulesets.Osu.Edit.Masks.SpinnerMasks.Components;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Tests.Visual;
using OpenTK;

namespace osu.Game.Rulesets.Osu.Tests
{
    public class TestCaseSpinnerSelectionMask : HitObjectSelectionMaskTestCase
    {
        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(SpinnerSelectionMask),
            typeof(SpinnerPiece)
        };

        private readonly DrawableSpinner drawableSpinner;

        public TestCaseSpinnerSelectionMask()
        {
            var spinner = new Spinner
            {
                Position = new Vector2(256, 256),
                StartTime = -1000,
                EndTime = 2000
            };
            spinner.ApplyDefaults(new ControlPointInfo(), new BeatmapDifficulty { CircleSize = 2 });

            Add(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Size = new Vector2(0.5f),
                Child = drawableSpinner = new DrawableSpinner(spinner)
            });
        }

        protected override SelectionMask CreateMask() => new SpinnerSelectionMask(drawableSpinner) { Size = new Vector2(0.5f) };
    }
}
