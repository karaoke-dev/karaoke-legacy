// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Screens.Play.ReplaySettings;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test KaraokeObject By Slider")]
    public class TestCaseKaraokeObjectBySlider : OsuTestCase
    {
        /// <summary>
        /// Drawable Object
        /// </summary>
        public DrawableKaraokeObject DrawableKaraokeObject { get; set; }

        public KaraokeObject KaraokeObject { get; set; }

        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {
            ExampleContainer container;

            KaraokeObject = DemoKaraokeObject.GenerateDemo001();

            DrawableKaraokeObject = new DrawableKaraokeObject(KaraokeObject)
            {
                Position = KaraokeObject.Position,
                ProgressUpdateByTime = false,
            };

            Add(container = new ExampleContainer());

            var slider = new SettingsSlider<double>()
            {
                LabelText = "Background dim ",
                Bindable = new BindableDouble
                {
                    MinValue = 0,
                    MaxValue = 500,
                    Default = 300,
                    Value = DrawableKaraokeObject.Progress,
                },
                Width = 0.5f
            };
            slider.Bindable.ValueChanged += (v) => { DrawableKaraokeObject.Progress = v; };

            Children = new Drawable[]
            {
                slider,
            };

            Add(DrawableKaraokeObject);
        }

        private class ExampleContainer : ReplayGroup
        {
            protected override string Title => @"example";
        }
    }
}
