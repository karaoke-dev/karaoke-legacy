using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using osu.Framework.Configuration;
using osu.Game.Configuration;
using osu.Game.Rulesets.Configuration;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class BaseKaraokeConfigManager<T> : RulesetConfigManager<T> where T : struct
    {
        public BaseKaraokeConfigManager(SettingsStore settings, RulesetInfo ruleset, int variant)
            : base(settings, ruleset, variant)
        {

        }

        /*
        public Bindable<U> SetObject<U>(T lookup, U value) where U : class
        {
            string bindableValue = JsonConvert.SerializeObject(value);
            return Set()
        }
        */
    }
}
