using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Symcol.Rulesets.Core.Wiki
{
    public class WikiLinkTextSection : Container
    {
        LinkText texContainer;

        public WikiLinkTextSection(string text)
        {
            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;
            AutoSizeAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            Children = new Drawable[]
            {
                texContainer = new LinkText
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    //AutoSizeAxes = Axes.Y,
                    //RelativeSizeAxes = Axes.X,
                    
                    TextSize = 20
                },
            };

            Text = text;

            texContainer.AutoSizeAxes = Axes.None;
            texContainer.RelativeSizeAxes = Axes.None;
            texContainer.AutoSizeAxes = Axes.Y;
            texContainer.RelativeSizeAxes = Axes.X;
        }

        public string Text
        {
            set { texContainer.Text = value; }
        }

        public string URL
        {
            set { texContainer.Url = value; }
        }

        public string TooltipText
        {
            set { texContainer.TooltipText = value; }
        }
    }
}
