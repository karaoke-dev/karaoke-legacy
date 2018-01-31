using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [5] introduce editor
    ///     5.1 : TODO : if has any setting , add in here
    /// </summary>
    class EditorSection : WikiSection
    {
        public override string Title => "Editor";

        public EditorSection()
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
