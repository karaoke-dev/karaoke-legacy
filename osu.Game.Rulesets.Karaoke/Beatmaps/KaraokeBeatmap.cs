using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    /// <summary>
    /// karaoke beatmap
    /// </summary>
    public class KaraokeBeatmap
    {
        public string SongName { get; set; }

        public string AuthorName { get; set; }

        public List<string> Languages { get; set; } = new List<string>();

        public List<KaraokeSinger> ListSinger { get; set; } = new List<KaraokeSinger>();
    }
}
