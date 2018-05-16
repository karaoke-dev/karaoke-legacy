// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Configuration.Tracking;
using osu.Game.Configuration;
using osu.Game.Rulesets.Karaoke.Service.Translator;
using osu.Game.Rulesets.Karaoke.Tools.Romaji;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class KaraokeConfigManager : BaseKaraokeConfigManager<KaraokeSetting>
    {
        public KaraokeConfigManager(SettingsStore settings, RulesetInfo ruleset, int variant = 0)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            //language
            Set(KaraokeSetting.NeedTranslateService, false);
            Set(KaraokeSetting.TranslateService, TranslatorProviderType.Google);
            Set(KaraokeSetting.DefaultTranslateLanguage, TranslateCode.English);


            //Romaji
            Set(KaraokeSetting.NeedRomajiService, false);
            Set(KaraokeSetting.RomajiService, RomajiServiceProviderType.KaraokeRomajiServer);

            //karaoke
            Set(KaraokeSetting.Platform, PlatformType.Desktop);
            Set(KaraokeSetting.ShowKarokePanel, false);
            Set(KaraokeSetting.DisableHotKay, false);

            //Style
            SetObject(KaraokeSetting.Template, new LyricTemplate());
            SetObject(KaraokeSetting.LyricStyle, new KaraokeLyricConfig()
            {
                SubTextVislbility = true,
                RomajiVislbility = true,
                RomajiFirst = false,
            });

            //singer
            SetObject(KaraokeSetting.SingerTemplate, new SingerTemplate());

            //Style
            Set(KaraokeSetting.Microphone, -1);
            Set(KaraokeSetting.MicrophoneVolumn, 0.5);
            Set(KaraokeSetting.Echo, 0.5);
            Set(KaraokeSetting.Tone, 0);

            //Device
            Set(KaraokeSetting.Device, PlatformType.Desktop);
            SetObject(KaraokeSetting.TouchScreen, new MobileScrollAnixConfig());

            //Tone
            Set(KaraokeSetting.NoteSpeed, 1500.0);
        }

        public override TrackedSettings CreateTrackedSettings() => new TrackedSettings
        {
        };
    }

    /// <summary>
    /// karaoke setting
    /// </summary>
    public enum KaraokeSetting
    {
        //language
        TranslateService = 0, //[int]use which api to translate
        DefaultTranslateLanguage = 1, //[enum]
        NeedTranslateService = 2, //[bool]false

        //Romaji
        NeedRomajiService = 12,
        RomajiService = 11, //[int]use which api to get romaji

        //karaoke control panel.
        Platform = 22,
        ShowKarokePanel = 21, //[bool]show panel at the beginning
        DisableHotKay = 24, //[bool]enable hotkey

        //Style
        Template = 31, //[object]
        LyricStyle = 32, //[Object]

        //singler
        SingerTemplate = 41, //[object]

        //Microphone (V2 system)
        Microphone = 51, //[int]select microphone device
        MicrophoneVolumn = 52, //[double]Volumn
        Echo = 53, //[double]Echo
        Tone = 54, //Future work ,adjust how voice microphone sounds like

        //device
        Device = 61, //which device
        TouchScreen = 62, //touch screen action

        //Note
        NoteSpeed = 71,//noteSpeed
    }
}
