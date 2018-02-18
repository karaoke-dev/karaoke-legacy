
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
    /// will force open the translate for lyrics
    /// even you are not open it in the config
    /// </summary>
    public class KaraokeModOpenTranslate : Mod
    {
        public override string Name => "Translate";
        public override string ShortenedName => "Tr";
        public override double ScoreMultiplier => 1;
        public override string Description => "Will force open the translate for lyrics, even you are not open it in the config.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_nofail;
        public override bool Ranked => true;
    }
}
