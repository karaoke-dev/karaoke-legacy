using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    public interface IHasPreemptiveTime
    {
        double? PreemptiveTime { get; set; }
    }
}
