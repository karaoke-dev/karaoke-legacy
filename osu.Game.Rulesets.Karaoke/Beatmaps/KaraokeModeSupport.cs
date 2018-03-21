using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    public class KaraokeModeSupport
    {
        /// <summary>
        /// Show tome visualization
        /// </summary>
        public bool Tome { get; set; }

        /// <summary>
        /// Has vocal track that can has off-vocal mod
        /// </summary>
        public bool VocalTrack { get; set; }
    }
}
