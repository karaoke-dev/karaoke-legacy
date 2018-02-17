using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Containers;

namespace Symcol.Rulesets.Core.Wiki
{
    public class WikiTextSection : Container
    {
        OsuTextFlowContainer texContainer;

        public WikiTextSection(string text)
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            Children = new Drawable[]
            {
                texContainer = new OsuTextFlowContainer(t => { t.TextSize = 20; })
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                }
            };

            Text = text;
        }

        public string Text
        {
            set { texContainer.Text = value; }
        }
    }
}
