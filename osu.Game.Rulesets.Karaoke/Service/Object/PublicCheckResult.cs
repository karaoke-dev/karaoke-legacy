using System;
namespace osu.Game.Rulesets.Karaoke.Service.Object
{
    public class PublicCheckResult
    {
        public PublicCheckResult()
        {

        }

        public PublicCheckResultTyle PublicCheckResultTyle { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    /// Public check result tyle.
    /// </summary>
    public enum PublicCheckResultTyle
    {
        /// <summary>
        /// Success commit and added to publish
        /// </summary>
        Syccess,

        /// <summary>
        /// Rejected
        /// </summary>
        Rejected,

        /// <summary>
        /// Other
        /// </summary>
        Other
    }
}
