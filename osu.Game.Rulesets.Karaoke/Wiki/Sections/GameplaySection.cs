using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Containers;
using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [0] introduce karaoke
    /// </summary>
    class GameplaySection : WikiSection
    {
        public override string Title => "GamePlay";

        public GameplaySection()
        {
            Content.Add(new WikiTextSection("osu!Karaoke \n"+
                "Hmm.... is Karaoke ?\n" +
                "I don't know how to introduce it."));
        }
    }
}
