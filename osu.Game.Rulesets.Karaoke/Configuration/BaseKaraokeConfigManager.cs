// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

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

        /// <summary>
        /// serialize and set object
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="lookup"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Bindable<string> SetObject<U>(T lookup, U value) where U : class
        {
            string bindableValue = JsonConvert.SerializeObject(value);
            return Set(lookup, bindableValue);
        }

        /// <summary>
        /// get obejct and deserialize object
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="lookup"></param>
        /// <returns></returns>
        public U GetObject<U>(T lookup) where U : class
        {
            var jsonString = Get<string>(lookup);
            return JsonConvert.DeserializeObject<U>(jsonString);
        }
    }
}
