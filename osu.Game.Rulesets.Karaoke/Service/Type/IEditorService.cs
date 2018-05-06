using System;

namespace osu.Game.Rulesets.Karaoke.Service.Type
{
    /// <summary>
    /// Define some common editor service's interface
    /// T is the upload item
    /// </summary>
    public interface IEditorService<T> where T : IEditItem
    {

        /// <summary>
        /// Get edit item from beatmap
        /// </summary>
        /// <returns>The edit item.</returns>
        /// <param name="beatmap">Beatmap.</param>
        T GetEditItem(Beatmap beatmap);

        /// <summary>
        /// Syncs the progress from cloud.
        /// </summary>
        /// <returns>The progress from cloud.</returns>
        void SyncProgressFromCloud();

        /// <summary>
        /// Save the progress to local
        /// </summary>
        void SaveToLocal();

        /// <summary>
        /// Save the progress to cloud
        /// </summary>
        void CommitToCloud();

        /// <summary>
        /// Commit change to cloud for checking
        /// </summary>
        void CommitToPublish();

        /// <summary>
        /// Gets the public result.
        /// </summary>
        /// <returns>The public result.</returns>
        /// <param name="beatmap">Beatmap.</param>
        PublicCheckResult GetPublicResult(Beatmap beatmap);
    }
}
