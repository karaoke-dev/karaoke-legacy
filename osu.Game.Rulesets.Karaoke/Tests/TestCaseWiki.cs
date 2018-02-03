using NUnit.Framework;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Wiki;
using osu.Game.Tests.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                 Wiki.OnLoadComplete += (a) =>
                 {
                     Wiki.Show();
                 };
                 this.Add(Wiki);
             });
        }

        protected void DisplseWiki()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                this.Remove(Wiki);
                Wiki.Dispose();
            });

        }
    }
}
