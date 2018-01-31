using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [6] introduce develpopers
    /// </summary>
    class DeveloperSection : WikiSection
    {
        public override string Title => "Development And Credit";

        public DeveloperSection()
        {
            Content.Add(new WikiTextSection("This is karaoke developers and really wants to thanks to : )"));
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("Main Developer"));
            Content.Add(new WikiTextSection("andy84019"));
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("Credit"));
            Content.Add(new WikiTextSection("Shawdooow\n    if no Shawdooow, no this fuxking awsome page.\n    I love this <3"));
            Content.Add(new WikiTextSection(" \n\n"));
            Content.Add(new WikiTextSection("赫蘿\n    赫蘿的屁屁讚啦\\ODO/"));
            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
