// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Symcol.Rulesets.Core.Wiki;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    ///     [5] introduce editor
    ///     5.1 : TODO : if has any setting , add in here
    /// </summary>
    internal class EditorSection : BaseWikiSection
    {
        public override string Title => "Editor";

        protected override void InitialView()
        {
            Content.Add(new WikiTextSection("TODO : Introduce"));
            Content.Add(new WikiTextSection(" \n\n"));


            Content.Add(new WikiSubSectionHeader("Editor"));
            //TODO
            Content.Add(new WikiTextSection("Waiting to implement"));
            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
