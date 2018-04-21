using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Mods.Types
{
    /// <summary>
    /// SSR
    /// </summary>
    public interface IHasSsr
    {
        /// <summary>
        /// Name
        /// </summary>
        string SsrAchieveName { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        string SsrId { get; set; }

        /// <summary>
        /// Precentage
        /// </summary>
        string SsrPrecetage { get; set; }
    }
}
