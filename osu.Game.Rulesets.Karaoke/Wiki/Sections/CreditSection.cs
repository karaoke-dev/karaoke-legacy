using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    class CreditSection : WikiSection
    {
        public override string Title => "Credit";

        public CreditSection()
        {
            Content.Add(new WikiTextSection("The people wants to thanks to : )"));
            Content.Add(new WikiTextSection(" \n\n"));


            Content.Add(new WikiSubSectionHeader("Shawdooow"));
            Content.Add(new WikiTextSection("    if no Shawdooow, no this fuxking awsome page.\n    I love this <3"));
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("赫蘿"));
            Content.Add(new WikiTextSection("    赫蘿的屁屁讚啦\\ODO/"));
            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
