using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Configuration;
using osu.Game.Configuration;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class KaraokeConfigManager : RulesetConfigManager<KaraokeSetting>
    {
        public KaraokeConfigManager(SettingsStore settings, RulesetInfo ruleset, int variant)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            //Set(ManiaSetting.ScrollTime, 1500.0, 50.0, 10000.0, 50.0);
        }

        /*
        public override TrackedSettings CreateTrackedSettings() => new TrackedSettings
        {
            new TrackedSetting<double>(ManiaSetting.ScrollTime, v => new SettingDescription(v, "Scroll Time", $"{v}ms"))
        };
        */
    }

    public enum KaraokeSetting
    {
        //language
        TranslateEngine,//use which api to translate
        DefaultTranslateLanguage,

        //Romaji
        RomajiEngine,//use which api to get romaji

        //karaoke
        ShowKarokePanel,
        EnableHotKay,

        //Style
        Template,//[object]
        SubTextVislbility,//[bool]
        RomajiVislbility,//[bool]
        RomajiFirst,//[bool]

        Singer,

        //Microphone (V2 system)
        Microphone,//select microphone device
        MicrophoneVolumn,//Volumn
        Echo,//Echo
        Tone,//Future work ,adjust how voice microphone sounds like


    }
}
