

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using osu.Game.Graphics;
using osu.Game.Rulesets.Karaoke.UI.Layer.ShowEffect;
using osu.Game.Rulesets.Mods;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// if click this mod
    /// will introduce how to use karaoke panel and other setting
    /// will ignore the songs you select
    /// maybe
    /// </summary>
    public class KaraokeTutorial : ModNoFail
    {
        public override string Name => "Tutorial";
        public override string ShortenedName => "Tu";
        public override double ScoreMultiplier => 1;
        public override string Description => "Will introduce how to use karaoke panel and other setting.";
        public override bool Ranked => true;
    }
}