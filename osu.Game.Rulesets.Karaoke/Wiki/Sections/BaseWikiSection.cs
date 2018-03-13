using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Allocation;
using osu.Game.Configuration;
using osu.Game.Rulesets.Karaoke.Configuration;
using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    public abstract class BaseWikiSection : WikiSection
    {
        public override string Title => "Base Wiki";

        protected KaraokeConfigManager RulesetConfig { get; set; }

        [BackgroundDependencyLoader(true)]
        private void load(RulesetStore rulesetStore, SettingsStore settings)
        {
            var karaokeRuleset = rulesetStore.AvailableRulesets.Where(x => x.ShortName == "karaoke").FirstOrDefault();

            RulesetConfig = new KaraokeConfigManager(settings, karaokeRuleset);

            //initial view
            InitialView();
        }

        /// <summary>
        /// Initial view
        /// </summary>
        protected abstract void InitialView();
    }
}
