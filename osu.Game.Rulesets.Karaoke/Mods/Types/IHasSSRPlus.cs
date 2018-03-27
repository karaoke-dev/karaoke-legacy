using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Mods.Types
{
    /// <summary>
    /// SSR Add precentage
    /// </summary>
    public interface IHasSsrPlus
    {
        /// <summary>
        /// Name
        /// </summary>
        string SsrPlusAchieveName { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        string SsrId { get; set; }

        /// <summary>
        /// Precentage
        /// </summary>
        string SsrAddPrecetage { get; set; }
    }
}
