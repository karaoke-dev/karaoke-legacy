

namespace osu.Game.Rulesets.Karaoke.Service.RomajiEditor
{
    public class TranslatorServiceProvider
    {
        /// <summary>
        /// Get translator by providerType
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IRomajiEditor GetTranslatorByProvider(RomajiEditorProviderType providerType)
        {
            switch (providerType)
            {
                case RomajiEditorProviderType.Github:
                    return new GithubRomajiEditor();
                default:
                    return null;
            }
        }
    }

    /// <summary>
    /// List Provider
    /// </summary>
    public enum RomajiEditorProviderType
    {
        /// <summary>
        /// <see cref="GithubRomajiEditor"/>
        /// </summary>
        Github,
    }
}
