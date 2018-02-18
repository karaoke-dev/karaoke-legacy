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
    /// will force close the translate for lyrics
    /// even you are open it in the config.
    /// </summary>
    public class KaraokeModCloseTranslate : Mod
    {
        public override string Name => "OffTranslate";
        public override string ShortenedName => "Tr_Close";
        public override double ScoreMultiplier => 1;
        public override string Description => "will force close the translate for lyrics, even you are open it in the config.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_hardrock;
        public override bool Ranked => true;
    }
}
