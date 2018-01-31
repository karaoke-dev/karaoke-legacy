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
            Content.Add(new WikiTextSection("TODO : Introduce"));

            Content.Add(new WikiSubSectionHeader("History"));
            //TODO : MarkdownContainer

        }
    }
}
