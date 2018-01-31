using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [2]introduce panel(app the panel, contain normal panel and microphone panel)
    ///     2.1 : setting need to show panel at startup
    /// </summary>
    class PanelSection : WikiSection
    {
        public override string Title => "Panel";

        public PanelSection()
        {
            Content.Add(new WikiTextSection("TODO : Introduce"));
            Content.Add(new WikiTextSection(" \n\n"));

            //TODO : show panel

            Content.Add(new WikiSubSectionHeader("Panel"));
            //TODO : setting

            Content.Add(new WikiTextSection(" \n\n"));
        }
    }
}
