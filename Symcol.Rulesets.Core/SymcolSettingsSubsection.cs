using osu.Framework.Allocation;
using osu.Game;
using osu.Game.Overlays.Settings;
using Symcol.Rulesets.Core.Wiki;

namespace Symcol.Rulesets.Core
{
    public abstract class SymcolSettingsSubsection : SettingsSubsection
    {
        public virtual WikiOverlay Wiki => null;

        private OsuGame osu;

        [BackgroundDependencyLoader]
        private void load(OsuGame osu)
        {
            this.osu = osu;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            if (Wiki != null)
                osu.Add(Wiki);
        }
    }
}
