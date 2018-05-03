using System;
using osu.Game.Rulesets.Karaoke.Service.Romaji;

namespace osu.Game.Rulesets.Karaoke.Tools.Romaji
{
    public enum RomajiServiceProvider
    {
        /// <summary>
        /// The romaji server just for osu!Karaoke
        /// <see cref="RomajiServerTranslator"/>
        /// </summary>
        KaraokeRomajiServer,

        /// <summary>
        /// The service that upload by othe people.
        /// <see cref="GithubTranslator"/>
        /// </summary>
        Github,
    }
}
