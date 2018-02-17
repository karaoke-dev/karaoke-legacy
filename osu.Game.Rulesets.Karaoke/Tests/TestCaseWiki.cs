// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Threading.Tasks;
using NUnit.Framework;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Wiki;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test Karaoke Object")]
    public class TestCaseWiki : OsuTestCase
    {
        public KaraokeWikiOverlay Wiki { get; set; }

        public TestCaseWiki()
        {
            AddStep("Add Wiki Overlay", () =>
            {
                Wiki = new KaraokeWikiOverlay();
                Wiki.StateChanged += (state) =>
                {
                    if (state == Visibility.Hidden)
                    {
                        DisplseWiki();
                    }
                };
                Wiki.OnLoadComplete += (a) => { Wiki.Show(); };
                Add(Wiki);
            });
        }

        protected void DisplseWiki()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                Remove(Wiki);
                Wiki.Dispose();
            });
        }
    }
}
