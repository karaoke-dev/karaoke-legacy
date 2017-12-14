using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    /// <summary>
    /// Test all the windows karaoke will use
    /// </summary>
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test windows")]
    class TestCaseWindows : OsuTestCase
    {
        public TestCaseWindows()
        {
            WindowsContainer WindowsContainer = new WindowsContainer();
            this.Add(WindowsContainer);
        }
    }
}
