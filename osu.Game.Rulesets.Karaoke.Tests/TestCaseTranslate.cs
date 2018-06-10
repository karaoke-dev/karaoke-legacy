// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Service.Translate;
using osu.Game.Tests.Visual;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    /// <summary>
    /// test case for translate string to 
    /// </summary>
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test Karaoke translate")]
    public class TestCaseTranslate : OsuTestCase
    {
        public TestCaseTranslate()
        {
            //Run();

            GoogleTranslator googleTranslator = new GoogleTranslator();
            googleTranslator.OnTranslateSuccess += (a, totalTranslate) =>
            {
                Add(new OsuSpriteText
                {
                    Text = totalTranslate,
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
            };
            googleTranslator.OnTranslateFail += (a, errorMessage) =>
            {
                Add(new OsuSpriteText
                {
                    Text = errorMessage,
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
            };

            googleTranslator.Translate(TranslateCode.English, TranslateCode.Chinese_Traditional, "倪好 world!===");
        }


        /*
        private async void Run()
        {
            // Create the service.
            var service = new TranslateService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyB9tomdvp8WmySkEWIhjhVYO3rkhzKOPMc",
                ApplicationName = "Translate API Sample"
            });

            string[] srcText = new[] { "Hello world!" };
            var response = await service.Translations.List(srcText, "en").ExecuteAsync();
            var translations = new List<string>();

            response = service.Translations.List(translations, "tw").Execute();

            string totalTranslate = "";
            foreach (var translation in response.Translations)
            {
                totalTranslate = totalTranslate + translation.TranslatedText;
            }

            Add(new OsuSpriteText
            {
                Text = totalTranslate,
                //Font = @"Venera",
                UseFullGlyphHeight = false,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                TextSize = 20,
                Alpha = 1,
                //ShadowColour = _textColor,
                Position = new OpenTK.Vector2(100, 100),
                //BorderColour = _textColor,
            });
        }
        */
    }
}
