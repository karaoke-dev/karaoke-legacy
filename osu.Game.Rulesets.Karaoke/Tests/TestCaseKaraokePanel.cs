// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.UI.Layer.ControlPanel.Desktop;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test Karaoke panel")]
    public class TestCaseKaraokePanel : OsuTestCase
    {
        /// <summary>
        /// Drawable Object
        /// </summary>
        public KaraokePanelOverlay KaraokePanelOverlay { get; set; }

        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Add(KaraokePanelOverlay = new KaraokePanelOverlay()
            {
                RelativeSizeAxes = Axes.X,
                Origin = Anchor.BottomCentre,
                Anchor = Anchor.BottomCentre,
            });

            AddStep("Toggle", KaraokePanelOverlay.ToggleVisibility);
        }
    }
}
