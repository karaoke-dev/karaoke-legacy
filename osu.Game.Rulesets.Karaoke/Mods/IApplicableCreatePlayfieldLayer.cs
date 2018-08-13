// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicableCreatePlayfieldLayer : IApplicableMod
    {
        IModLayer CreateNewLayer(Playfield playfield);
    }
}
