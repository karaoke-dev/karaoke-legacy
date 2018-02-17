using System;
namespace osu.Game.Rulesets.Karaoke.Configuration
{
    /// <summary>
    /// Karaoke note config.
    /// </summary>
    public class KaraokeNoteConfig
    {
        public KaraokeNoteConfig()
        {
            
        }

        /// <summary>
        /// show subText
        /// </summary>
        public bool SubTextVislbility { get; set; }

        /// <summary>
        /// show romaji
        /// will replace mainText
        /// </summary>
        public bool RomajiVislbility { get; set; }
    }
}
