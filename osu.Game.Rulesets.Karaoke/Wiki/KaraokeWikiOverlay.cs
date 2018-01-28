using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki
{
    public class KaraokeWikiOverlay : WikiOverlay
    {
        protected override WikiHeader Header => new KaraokeWikiHeader();

        protected override WikiSection[] Sections => new WikiSection[]
        {
            /*
            new GameplaySection(),
            new CharactersSection(),
            new EditorSection(),
            new RankingSection(),
            //new MultiplayerSection(),
            new CodeSection(),
            new CreditsSection(),
            */
        };
    }
}
