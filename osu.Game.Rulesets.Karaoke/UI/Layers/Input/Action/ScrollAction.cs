using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Input;

namespace osu.Game.Rulesets.Karaoke.UI.Layer.Input.Action
{
    public class ScrollAction
    {
        public KaraokeScrollAction KaraokeScrollAction { get; set; }

        public bool Touch { get; set; }

        public double Value { get; set; }
    }
}
