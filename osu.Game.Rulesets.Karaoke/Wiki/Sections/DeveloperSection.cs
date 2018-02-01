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
        public override string Title => "Development";

        public DeveloperSection()
        {
            Content.Add(new WikiTextSection("osu!karaoke developer"));
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("andy84019"));
            Content.Add(new WikiTextSection("    Main Developer"));
            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
