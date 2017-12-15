using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// Karaoke info
    /// </summary>
    public class KaraokeInfo
    {
        public string SongName { get; set; }

        public string AuthorName { get; set; }

        public List<string> Languages { get; set; } = new List<string>();

        public List<KaraokeSinger> ListSinger { get; set; } = new List<KaraokeSinger>();

        public KaraokeTemplate KaraokeTemplate { get; set; } = new KaraokeTemplate();
    }
}
