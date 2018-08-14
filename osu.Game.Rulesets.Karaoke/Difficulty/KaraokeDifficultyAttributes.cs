// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

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
