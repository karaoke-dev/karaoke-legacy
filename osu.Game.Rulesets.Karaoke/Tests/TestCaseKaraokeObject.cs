﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Screens.Play.ReplaySettings;
using osu.Game.Tests.Visual;
using OpenTK;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Framework.Timing;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test Karaoke Object")]
    public class TestCaseKaraokeObject : OsuTestCase
    {
        private FramedClock framedClock;
        private Container playfieldContainer;
        private Vector2 appearPosition;

        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {
            var rateAdjustClock = new StopwatchClock(true);
            framedClock = new FramedClock(rateAdjustClock);

            AddStep(@"KaraokeObject_Demo001", () => loadHitobjects(DemoKaraokeObject.GenerateDemo001()));

            AddStep(@"ResetPosition", () => { appearPosition = new Vector2(0, -200); });

            framedClock.ProcessFrame();

            var clockAdjustContainer = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Clock = framedClock,
                Children = new[]
                {
                    playfieldContainer = new KaraokeInputManager(rulesets.GetRuleset(0)) { RelativeSizeAxes = Axes.Both },
                }
            };

            Add(clockAdjustContainer);
        }

        private void loadHitobjects(KaraokeObject karaokeObject)
        {
            karaokeObject.StartTime = framedClock.CurrentTime + 160;
            playfieldContainer.Add(new DrawableKaraokeObject(karaokeObject)
            {
                Position = karaokeObject.Position+appearPosition,
            });

            appearPosition = appearPosition + new Vector2(0, 100);
        }
    }
}
