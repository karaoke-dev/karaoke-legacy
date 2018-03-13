// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [8]show the develpment progress
    ///     8.1 : TODO : will get the newest markdown style progress from github or gitbook
    /// </summary>
    class DevelopmentHistorySection : BaseWikiSection
    {
        public override string Title => "Development History";

        public DevelopmentHistorySection()
        {
            Content.Add(new WikiTextSection("History of development"));
            Content.Add(new WikiLinkTextSection("And source code can be found in https://github.com/osu-Karaoke/osu-Karaoke")
            {
                Url = "https://github.com/osu-Karaoke/osu-Karaoke",
                TooltipText = "https://github.com/osu-Karaoke/osu-Karaoke",
            });
            Content.Add(new WikiTextSection(" \n\n"));

            Content.Add(new WikiSubSectionHeader("History"));
            //TODO : MarkdownContainer


            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
