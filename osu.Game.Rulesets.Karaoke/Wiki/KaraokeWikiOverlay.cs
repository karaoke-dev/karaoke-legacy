// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Wiki.Sections;
using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki
{
    public class KaraokeWikiOverlay : WikiOverlay
    {
        protected override WikiHeader Header => new KaraokeWikiHeader();

        protected override WikiSection[] Sections => new WikiSection[]
        {
            //[0] introduce karaoke
            new GameplaySection(),

            //[1] introduce about translate and romaji
            //     1.1 : select language translate API (default is google)
            //     1.2 : select romaji translator
            new LanguageSection(),

            //[2]introduce panel(app the panel, contain normal panel and microphone panel)
            //     2.1 : setting need to show panel at startup
            new PanelSection(),

            //[3] introduce which style can be setting
            //     3.1 : template
            //     3.2 : singer(maybe)
            new StyleSection(),

            //[4] introduce v2 system
            //     4.1 : open the microphone button
            //     4.1 : device
            //     4.2 : volumn
            //     4.3 : echo
            new MicrophoneSection(),

            //Device section
            new DeviceSection(),

            //[5] introduce editor
            //     5.1 : TODO : if has any setting , add in here
            new EditorSection(),

            //[6] introduce develpopers
            new DeveloperSection(),

            //[7] introduce credits
            new CreditSection(),

            //[8]show the develpment progress
            //     8.1 : TODO : will get the newest markdown style progress from github or gitbook
            new DevelopmentHistorySection(),
        };
    }
}
