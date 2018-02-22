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
    /// make lytric Transparent.
    /// </summary>
    public class KaraokeModTransparentLyrics : Mod
    {
        public override string Name => "Transparent";
        public override string ShortenedName => "Transparent";
        public override double ScoreMultiplier => 1;
        public override string Description => "make lytric Transparent.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_flashlight;
        public override bool Ranked => true;
    }
}