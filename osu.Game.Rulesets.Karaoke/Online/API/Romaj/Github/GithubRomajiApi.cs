// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;

namespace osu.Game.Rulesets.Karaoke.Online.API.Romaj.Github
{
    public class GithubRomajiApi
    {
        public GithubRomajiApi()
        {
        }
    }

    public class GithubRomajiTranslate
    {
        /// <summary>
        /// Version
        /// </summary>
        public string Ver { get; set; } = "1.0";

        /// <summary>
        /// AuthorId
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// LangId
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Lyrics
        /// </summary>
        public List<GithubRomajiLyric> Lyrics { get; set; }
    }

    public class GithubRomajiLyric
    {
        /// <summary>
        /// Lyric id
        /// </summary>
        public string LyrucId { get; set; }

        /// <summary>
        /// Romajis
        /// </summary>
        public List<GithubRomaji> Romajis { get; set; }
    }

    public class GithubRomaji
    {
        /// <summary>
        /// Index
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Romaji
        /// </summary>
        public string Romaji { get; set; }
    }
}
