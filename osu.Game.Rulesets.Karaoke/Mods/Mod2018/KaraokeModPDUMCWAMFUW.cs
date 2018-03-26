using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Mods.Mod2018
{
    /// <summary>
    /// Window$
    /// </summary>
    public class KaraokeModPDUMCWAMFUW : Mod , IApplicableMod
    {
        public override string Name => "PDUMCWAMFUW";
        public override string ShortenedName => "PDUMCWAMFUW";
        public override double ScoreMultiplier => 1;
        public override string Description => "I already pay the money, PLEASE DONT UPGRADE MY COMPUTER WITHOUT ASKING FUXK You Window$.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_suddendeath;
        public override bool Ranked => true;
    }
}
