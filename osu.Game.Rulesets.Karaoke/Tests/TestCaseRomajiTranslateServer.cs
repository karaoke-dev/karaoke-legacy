using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using osu.Framework.Allocation;
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
