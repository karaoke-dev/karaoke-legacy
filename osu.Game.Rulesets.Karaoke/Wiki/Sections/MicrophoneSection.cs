using Symcol.Rulesets.Core.Wiki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Wiki.Sections
{
    /// <summary>
    /// [4] introduce v2 system
    ///     4.1 : open the microphone button
    ///     4.1 : device
    ///     4.2 : volumn
    ///     4.3 : echo
    /// </summary>
    class MicrophoneSection : WikiSection
    {
        public override string Title => "Microphone";

        public MicrophoneSection()
        {
            Content.Add(new WikiTextSection("TODO : Introduce about V2 system"));

            //TODO : show panel

            Content.Add(new WikiSubSectionHeader("BeforeSetting"));
            //TODO : noeified press button to open microphone

            Content.Add(new WikiSubSectionHeader("Device"));
            //TODO : show settingTemplate

            Content.Add(new WikiSubSectionHeader("Volumn"));
            //TODO : show singer
        }
    }
}
