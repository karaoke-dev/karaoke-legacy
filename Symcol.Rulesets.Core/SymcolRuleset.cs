using osu.Game.Rulesets;
using Symcol.Rulesets.Core.Wiki;

namespace Symcol.Rulesets.Core
{
    public abstract class SymcolRuleset : Ruleset
    {
        protected SymcolRuleset(RulesetInfo rulesetInfo = null) : base(rulesetInfo) { }

        public virtual WikiOverlay GetWiki() => null;
    }
}
