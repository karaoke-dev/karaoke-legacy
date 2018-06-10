// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Service.Object
{
    public class PublicCheckResult
    {
        public PublicCheckResultTyle PublicCheckResultTyle { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    ///     Public check result tyle.
    /// </summary>
    public enum PublicCheckResultTyle
    {
        /// <summary>
        ///     Success commit and added to publish
        /// </summary>
        Syccess,

        /// <summary>
        ///     Rejected
        /// </summary>
        Rejected,

        /// <summary>
        ///     Other
        /// </summary>
        Other
    }
}
