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
    /// if sound trach has two parts, open the vocal part
    /// </summary>
    public class OpenVocal : Mod
    {
        public override string Name => "OpenVocal";
        public override string ShortenedName => "OpenVocal";
        public override double ScoreMultiplier => 1;
        public override string Description => "if sound trach has two parts, open the vocal part.";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_relax;
        public override bool Ranked => true;
    }
    public override bool Ranked => true;
    }
}