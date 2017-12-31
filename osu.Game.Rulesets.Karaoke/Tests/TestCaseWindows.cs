// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using NUnit.Framework;
using osu.Game.Rulesets.Karaoke.Edit.Dialog;
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
            DialogContainer WindowsContainer = new DialogContainer();
            Add(WindowsContainer);
        }
    }
}
