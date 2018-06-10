﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    /// <summary>
    ///     karaoke beatmap
    ///     will contain the basic karaoke song info
    /// </summary>
    public class KaraokeBeatmap
    {
        #region SongInfo

        /// <summary>
        ///     song name
        /// </summary>
        public string SongName { get; set; }

        /// <summary>
        ///     song author
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        ///     list singer
        /// </summary>
        public List<Singer> ListSinger { get; set; } = new List<Singer>();

        /// <summary>
        ///     song languages
        /// </summary>
        public List<TranslateCode> SongLanguages { get; set; } = new List<TranslateCode>();

        /// <summary>
        ///     Mode Support
        /// </summary>
        public KaraokeModeSupport KaraokeModeSupport { get; set; }

        #endregion

        #region Language

        /// <summary>
        ///     available translate languages
        /// </summary>
        public List<TranslateCode> TranslateLanguages { get; set; } = new List<TranslateCode>();

        /// <summary>
        ///     Has Language
        /// </summary>
        public bool HasRomaji { get; set; }

        #endregion

        #region Copyright

        /// <summary>
        ///     if this song is sample, means it is not ranked
        /// </summary>
        public bool IsSample { get; set; } = false;

        /// <summary>
        ///     karaoke songs can be edit or play with V2 system
        ///     if contain Authorized code
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        ///     if upload beatmap it will auto-generate this
        ///     [reserve]
        /// </summary>
        public string PublishKey { get; set; }

        #endregion
    }
}
