using System;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    public class KaraokeModPractice : Mod
    {
        public override string Name => "Practice";
        public override string ShortenedName => "Practice";
        public override double ScoreMultiplier => 0.0f;
        public override Type[] IncompatibleMods => new[] { typeof(ModDoubleTime), typeof(ModDoubleTime) };
        public override FontAwesome Icon => FontAwesome.fa_play_circle;
    }
}
