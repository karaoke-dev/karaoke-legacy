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
    /// not even shows any lyrics
    /// </summary>
    public class KaraokeModCloseLyrics : Mod
    {
        public override string Name => "CloseLyrics";
        public override string ShortenedName => "Cl";
        public override double ScoreMultiplier => 1;
        public override string Description => "not even shows any lyrics.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_suddendeath;
        public override bool Ranked => true;
    }
}