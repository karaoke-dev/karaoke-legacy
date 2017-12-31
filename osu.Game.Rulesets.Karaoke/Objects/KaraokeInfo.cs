// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;

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
