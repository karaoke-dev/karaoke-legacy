using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [7]show the develpment progress
    ///     7.1 : TODO : will get the newest markdown style progress from github or gitbook
    /// </summary>
    class DevelopmentHistorySection : WikiSection
    {
        public override string Title => "Development History";

        public DevelopmentHistorySection()
        {
            Content.Add(new WikiTextSection("History of development"));
            Content.Add(new WikiLinkTextSection("And source code can be found in https://github.com/osu-Karaoke/osu-Karaoke")
            {
                URL= "https://github.com/osu-Karaoke/osu-Karaoke",
                TooltipText = "https://github.com/osu-Karaoke/osu-Karaoke",
            });
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("History"));
            //TODO : MarkdownContainer





            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
