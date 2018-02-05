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
                "It's a third-party ruleset that can let everyone make their karaoke songs and share it on osu! \n" +
                "It has three special frature designed for foreign people \n" +
                "   1. Romaji support \n" +
                "   2. Lyric trsnslate support \n" +
                "   3. Kanji support \n" +
                "At early stage it is just support english and japanese karaoke songs, but it will support more language in the future."));

            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
