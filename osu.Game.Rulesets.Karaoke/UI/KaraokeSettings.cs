// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Tools.Translator;
using osu.Game.Rulesets.Karaoke.Wiki;
using Symcol.Rulesets.Core;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public class KaraokeSettings : SymcolSettingsSubsection<KaraokeWikiOverlay>
    {
        protected override string Header => "Karaoke!";

        //public static KaraokeConfigManager KaraokeConfigManager;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = "Show Translate by google",
                    //Bindable = config.GetBindable<bool>(OsuSetting.SnakingInSliders)
                },
                new SettingsEnumDropdown<TranslateCode>
                {
                    LabelText = "Translate to...",
                    //Bindable = config.GetBindable<SelectionRandomType>(OsuSetting.SelectionRandomType),
                },
                new SettingsCheckbox
                {
                    LabelText = "Always shows Karaoke panel",
                    //Bindable = config.GetBindable<bool>(OsuSetting.SnakingOutSliders)
                },
                new SettingsCheckbox
                {
                    LabelText = "Karaoke Effect",
                    //Bindable = config.GetBindable<bool>(OsuSetting.SnakingOutSliders)
                },
                new SettingsCheckbox
                {
                    LabelText = "Enable HotKey",
                    //Bindable = config.GetBindable<bool>(OsuSetting.SnakingOutSliders)
                },
                new SettingsButton
                {
                    Text = "Open In-game Wiki",
                    Action = () => { ShowWiki(); },
                },
            };
        }
    }
}
