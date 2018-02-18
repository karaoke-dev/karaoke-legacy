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
    /// will hide the lyrics
    /// </summary>
    public class KaraokeHidden : ModHidden
    {
        public override string Name => "Hidden";
        public override string ShortenedName => "HD";
        public override double ScoreMultiplier => 1;
        public override string Description => "Hidden the lyric at the start time.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_hidden;
        public override bool Ranked => true;
    }
}