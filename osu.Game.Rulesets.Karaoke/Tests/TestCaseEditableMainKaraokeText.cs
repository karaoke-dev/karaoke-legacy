using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Screens.Play.ReplaySettings;
using osu.Game.Tests.Visual;
using OpenTK;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Framework.Timing;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Edit.Drawables;

namespace osu.Game.Rulesets.Karaoke.Tests
{
    /// <summary>
    /// test
    /// </summary>
    /// [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test EditableMainKaraokeText class")]
    public class TestCaseEditableMainKaraokeText : OsuTestCase
    {
        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesets)
        {

            Add(new DrawableEditableKaraokeObject(DemoKaraokeObject.GenerateWithStartAndDuration(0, 100000))
            {
                Position = new Vector2(100, 100),
                Template = new KaraokeTemplate(),
                //ProgressUpdateByTime = false,
            });
        }
    }
}
