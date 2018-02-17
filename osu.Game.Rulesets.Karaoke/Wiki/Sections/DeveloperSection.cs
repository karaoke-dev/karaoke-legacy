// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Symcol.Rulesets.Core.Wiki;

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
