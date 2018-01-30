using osu.Framework.Allocation;
using osu.Game;
using osu.Game.Overlays.Settings;
using Symcol.Rulesets.Core.Wiki;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using System.Threading.Tasks;

namespace Symcol.Rulesets.Core
{
    public abstract class SymcolSettingsSubsection<T> : SettingsSubsection where T : WikiOverlay , new()
    {
        public T Wiki { get; set; }

        private OsuGame osu;

        [BackgroundDependencyLoader]
        private void load(OsuGame osu)
        {
            this.osu = osu;
        }

        protected void ShowWiki()
        {
            Wiki = new T();
            Wiki.StateChanged += (state) =>
              {
                  if (state == Visibility.Hidden)
                  {
                      DisplseWiki();
                  }
              };
            Wiki.OnLoadComplete += (a) =>
              {
                  Wiki.Show();
              };
            osu.Add(Wiki);
        }

        protected void DisplseWiki()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                osu.Remove(Wiki);
                Wiki.Dispose();
            });
            
        }
    }
}
