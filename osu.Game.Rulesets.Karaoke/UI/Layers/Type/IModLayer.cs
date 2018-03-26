using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Configuration;
using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Type
{
    public interface IModLayer : ILayer
    {
        Bindable<IEnumerable<Mod>> Mods { get; set; }
    }
}
