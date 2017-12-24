using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// relative position
    /// </summary>
    public interface IHasRelativePosition
    {
        /// <summary>
        /// relative position can be null
        /// </summary>
        Vector2? RelativePosition { get; set; }
    }
}
