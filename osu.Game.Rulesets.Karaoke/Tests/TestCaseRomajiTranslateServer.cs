// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using NUnit.Framework;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test Translate Server")]
    public class TestCaseRomajiTranslateServer : OsuTestCase
    {
        /// <summary>
        /// constructor
        /// </summary>
        public TestCaseRomajiTranslateServer()
        {
            AddStep(@"Translate", () => { TranslateKanjiToRomaji(); });
        }

        /// <summary>
        /// 
        /// </summary>
        protected void TranslateKanjiToRomaji()
        {

        }
    }

    
}
