using osu.Framework.Graphics.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Mods
{
    /// <summary>
    /// if this mod has new layer
    /// use this
    /// </summary>
    public interface IHasLayer
    {
        Container CreateNewLayer();
    }
}
