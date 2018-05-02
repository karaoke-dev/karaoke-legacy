using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Online.API.Git
{
    /// <summary>
    /// it chntains several finction 
    /// 1. login and get auth code
    /// 2. download target file
    /// 3. pull request
    /// 4. get pull request commit message
    /// 5. commit pull request message 
    /// </summary>
    public class GithubApi
    {
        public GithubApi()
        {

        }

        /// <summary>
        /// Get file
        /// </summary>
        /// <param name="orgination"></param>
        /// <param name="repositoryName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<string> GetFileFrom(string orgination,string repositoryName,string path)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            string url = "https://raw.githubusercontent.com/" + orgination + "/" + repositoryName + "/" + path;
            string json = await client.DownloadStringTaskAsync(url);
            return json;
        }
    }
}
