using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.Difficulty
{
    public class KaraokeDifficultyAttributes : DifficultyAttributes
    {
        public KaraokeDifficultyAttributes(Mod[] mods, double starRating)
            : base(mods, starRating)
        {
        }
    }
}
