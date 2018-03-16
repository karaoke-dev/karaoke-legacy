using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Type
{
    /// <summary>
    /// All layer should inherit this
    /// </summary>
    public interface ILayer<TConfig> where TConfig : LayerConfig
    {
        /// <summary>
        /// Platform
        /// </summary>
        PlatformType PlatformType { get; set; }

        /// <summary>
        /// Config
        /// </summary>
        BindableObject<TConfig> Config { get; set; }
    }
}
