using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [3] introduce which style can be setting
    ///     3.1 : template
    ///     3.2 : singer(maybe)
    /// </summary>
    class StyleSection : WikiSection
    {
        public override string Title => "Style";

        public StyleSection()
        {
            Content.Add(new WikiTextSection("TODO : Introduce"));

            //TODO : show panel

            Content.Add(new WikiSubSectionHeader("Template"));
            //TODO : show settingTemplate

            Content.Add(new WikiSubSectionHeader("Singer"));
            //TODO : show singer
        }
    }
}
