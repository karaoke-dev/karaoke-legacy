// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [0] introduce karaoke
    /// </summary>
    internal class GameplaySection : BaseWikiSection
    {
        public override string Title => "GamePlay";

        protected override void InitialView()
        {
            Content.Add(new WikiTextSection("osu!Karaoke \n" +
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
