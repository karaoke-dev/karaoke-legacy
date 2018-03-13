// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    internal class CreditSection : BaseWikiSection
    {
        public override string Title => "Credit";

        protected override void InitialView()
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
