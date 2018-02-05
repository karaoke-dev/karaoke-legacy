using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Configuration;
using osu.Game.Configuration;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Karaoke.Objects;

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
            //language
            Set(KaraokeSetting.TranslateEngine, -1);
            Set(KaraokeSetting.DefaultTranslateLanguage, TranslateCode.English);

            //Romaji
            Set(KaraokeSetting.RomajiEngine, -1);

            //karaoke
            Set(KaraokeSetting.ShowKarokePanel, false);
            Set(KaraokeSetting.DisableHotKay, false);

            //Style
            Set(KaraokeSetting.Template, new KaraokeTemplate());
            Set(KaraokeSetting.SubTextVislbility, true);
            Set(KaraokeSetting.RomajiVislbility, true);
            Set(KaraokeSetting.RomajiFirst, false);

            //singer
            //Set(KaraokeSetting.Singer, new KaraokeTemplate());

            //Style
            Set(KaraokeSetting.Microphone, -1);
            Set(KaraokeSetting.MicrophoneVolumn, 0.5);
            Set(KaraokeSetting.Echo, 0.5);
            Set(KaraokeSetting.Tone, 0);


        }
    }

    public enum KaraokeSetting
    {
        //language
        TranslateEngine,//[int]use which api to translate
        DefaultTranslateLanguage,//[enum]

        //Romaji
        RomajiEngine,//[int]use which api to get romaji

        //karaoke
        ShowKarokePanel,//[bool]show panel at the beginning
        DisableHotKay,//[bool]enable hotkey

        //Style
        Template,//[object]
        SubTextVislbility,//[bool]
        RomajiVislbility,//[bool]
        RomajiFirst,//[bool]
        TranslateLyric,//[bool]

        //singler
        Singer,//[object]

        //Microphone (V2 system)
        Microphone,//[int]select microphone device
        MicrophoneVolumn,//[double]Volumn
        Echo,//[double]Echo
        Tone,//Future work ,adjust how voice microphone sounds like


    }
}
