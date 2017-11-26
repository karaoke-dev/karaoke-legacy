
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Tests.Visual;
using osu.Game.Graphics.Sprites;
using NUnit.Framework;
using osu.Framework.Graphics;
using System.Net;
using Newtonsoft.Json;
using osu.Game.Rulesets.Karaoke.Tools.Translator;

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
                    Position = new OpenTK.Vector2(100, 100),
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
                    Position = new OpenTK.Vector2(100, 100),
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
