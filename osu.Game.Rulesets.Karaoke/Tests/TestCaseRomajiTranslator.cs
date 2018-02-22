using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Tools.Romaji;
using osu.Game.Tests.Visual;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test karaoke romaji translate")]
    public class TestCaseRomajiTranslator : OsuTestCase
    {
        private GoogleRomajiTranslator googleRomajiTranslator { get; set; }

        public TestCaseRomajiTranslator()
        {
            googleRomajiTranslator = new GoogleRomajiTranslator();

            var translateResult = googleRomajiTranslator.Translate("終わるまでは終わらないよ");

            Add(new OsuSpriteText
            {
                Text = translateResult,
                //Font = @"Venera",
                UseFullGlyphHeight = false,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                TextSize = 20,
                Alpha = 1,
                //ShadowColour = _textColor,
                Position = new Vector2(100, 100),
                //BorderColour = _textColor,
            });
            
        }
    }
}
