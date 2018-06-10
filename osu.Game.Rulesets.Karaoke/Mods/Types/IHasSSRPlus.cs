// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Mods.Types
{
    /// <summary>
    ///     SSR Add precentage
    /// </summary>
    public interface IHasSsrPlus
    {
        /// <summary>
        ///     Name
        /// </summary>
        string SsrPlusAchieveName { get; set; }

        /// <summary>
        ///     Id
        /// </summary>
        string SsrId { get; set; }

        /// <summary>
        ///     Precentage
        /// </summary>
        string SsrAddPrecetage { get; set; }
    }
}
