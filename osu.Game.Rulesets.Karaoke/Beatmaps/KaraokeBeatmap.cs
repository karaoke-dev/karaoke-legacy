// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
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
