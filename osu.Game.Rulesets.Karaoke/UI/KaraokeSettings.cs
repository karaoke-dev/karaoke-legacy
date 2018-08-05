// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Wiki;
using Symcol.Rulesets.Core;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public class KaraokeSettings : SymcolSettingsSubsection<KaraokeWikiOverlay>
    {
        public static KaraokeConfigManager RulesetConfig;
        protected override string Header => "Karaoke!";

        [BackgroundDependencyLoader]
        private void load(RulesetStore rulesetStore, SettingsStore settings)
        {
            var karaokeRuleset = rulesetStore.AvailableRulesets.Where(x => x.ShortName == "karaoke").FirstOrDefault();
            RulesetConfig = new KaraokeConfigManager(settings, karaokeRuleset);

            Children = new Drawable[]
            {
                new SettingsEnumDropdown<TranslateCode>
                {
                    LabelText = "Translate to...",
                    Bindable = RulesetConfig.GetBindable<TranslateCode>(KaraokeSetting.DefaultTranslateLanguage)
                },
                new SettingsCheckbox
                {
                    LabelText = "Always shows Karaoke panel"
                    //Bindable = config.GetBindable<bool>(OsuSetting.SnakingOutSliders)
                },
                new SettingsCheckbox
                {
                    LabelText = "Karaoke Effect"
                    //Bindable = config.GetBindable<bool>(OsuSetting.SnakingOutSliders)
                },
                new SettingsCheckbox
                {
                    LabelText = "Enable HotKey"
                    //Bindable = config.GetBindable<bool>(OsuSetting.SnakingOutSliders)
                },
                new SettingsButton
                {
                    Text = "Open In-game Wiki",
                    Action = ShowWiki
                }
            };
        }

        public KaraokeSettings(Ruleset ruleset)
            : base(ruleset)
        {
        }
    }
}
