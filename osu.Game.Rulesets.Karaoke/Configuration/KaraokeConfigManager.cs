// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Configuration.Tracking;
using osu.Game.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class KaraokeConfigManager : BaseKaraokeConfigManager<KaraokeSetting>
    {
        public KaraokeConfigManager(SettingsStore settings, RulesetInfo ruleset, int variant=0)
            : base(settings, ruleset, variant)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();

            //language
            Set(KaraokeSetting.TranslateService, TranslateService.Google);
            Set(KaraokeSetting.DefaultTranslateLanguage, TranslateCode.English);
            Set(KaraokeSetting.NeedTranslate, false);

            //Romaji
            Set(KaraokeSetting.RomajiService, RomajiService.KoroSiro);

            //karaoke
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
            SetObject(KaraokeSetting.Singer, new Singer());

            //Style
            Set(KaraokeSetting.Microphone, -1);
            Set(KaraokeSetting.MicrophoneVolumn, 0.5);
            Set(KaraokeSetting.Echo, 0.5);
            Set(KaraokeSetting.Tone, 0);

            //Device
            Set(KaraokeSetting.Device, DeviceType.Desktop);
            SetObject(KaraokeSetting.TouchScreen, new MobileScrollAnixConfig());
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
        TranslateService, //[int]use which api to translate
        DefaultTranslateLanguage, //[enum]
        NeedTranslate, //[bool]false

        //Romaji
        RomajiService, //[int]use which api to get romaji

        //karaoke
        ShowKarokePanel, //[bool]show panel at the beginning
        DisableHotKay, //[bool]enable hotkey

        //Style
        Template, //[object]
        LyricStyle, //[Object]

        //singler
        Singer, //[object]

        //Microphone (V2 system)
        Microphone, //[int]select microphone device
        MicrophoneVolumn, //[double]Volumn
        Echo, //[double]Echo
        Tone, //Future work ,adjust how voice microphone sounds like

        //device
        Device,//which device
        TouchScreen,//touch screen action
    }
}
