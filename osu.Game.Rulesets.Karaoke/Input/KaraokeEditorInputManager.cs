using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Karaoke.Input
{
    public class KaraokeEditorInputManager : RulesetInputManager<KaraokeKeyAction>
    {
        public KaraokeEditorInputManager(RulesetInfo ruleset)
            : base(ruleset, 1, SimultaneousBindingMode.Unique)
        {
        }
    }
}
