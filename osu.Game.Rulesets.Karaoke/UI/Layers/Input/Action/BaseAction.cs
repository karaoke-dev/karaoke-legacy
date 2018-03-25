using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action
{
    /// <summary>
    /// Base action
    /// </summary>
    public class BaseAction : RecordChangeObject , ICopyable
    {
        public virtual T Copy<T>() where T : class, ICopyable, new()
        {
            throw new NotImplementedException();
        }
    }
}
