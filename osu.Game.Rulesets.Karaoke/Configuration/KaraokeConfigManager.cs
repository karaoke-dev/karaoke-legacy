using osu.Framework.Configuration.Tracking;
using osu.Game.Configuration;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class KaraokeConfigManager : BaseKaraokeConfigManager<KaraokeSetting>
    {
        public KaraokeConfigManager(SettingsStore settings, RulesetInfo ruleset, int variant)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            //language
            Set(KaraokeSetting.TranslateEngine, -1);
            Set(KaraokeSetting.DefaultTranslateLanguage, TranslateCode.English);
            Set(KaraokeSetting.NeedTranslate, false);

            //Romaji
            Set(KaraokeSetting.RomajiEngine, -1);

            //karaoke
            Set(KaraokeSetting.ShowKarokePanel, false);
            Set(KaraokeSetting.DisableHotKay, false);

            //Style
            SetObject(KaraokeSetting.Template, new KaraokeTemplate());
            SetObject(KaraokeSetting.LyricStyle, new KaraokeTextStyle()
            {
                SubTextVislbility = true,
                RomajiVislbility = true,
                RomajiFirst = false,
            });

            //singer
            //Set(KaraokeSetting.Singer, new KaraokeTemplate());

            //Style
            Set(KaraokeSetting.Microphone, -1);
            Set(KaraokeSetting.MicrophoneVolumn, 0.5);
            Set(KaraokeSetting.Echo, 0.5);
            Set(KaraokeSetting.Tone, 0);


        }

        /*
        public override TrackedSettings CreateTrackedSettings()
        {
            return base.CreateTrackedSettings();
        }
        */
    }

    public enum KaraokeSetting
    {
        //language
        TranslateEngine,//[int]use which api to translate
        DefaultTranslateLanguage,//[enum]
        NeedTranslate,//[bool]false

        //Romaji
        RomajiEngine,//[int]use which api to get romaji

        //karaoke
        ShowKarokePanel,//[bool]show panel at the beginning
        DisableHotKay,//[bool]enable hotkey

        //Style
        Template,//[object]
        LyricStyle,//[Object]

        //singler
        Singer,//[object]

        //Microphone (V2 system)
        Microphone,//[int]select microphone device
        MicrophoneVolumn,//[double]Volumn
        Echo,//[double]Echo
        Tone,//Future work ,adjust how voice microphone sounds like


    }
}
