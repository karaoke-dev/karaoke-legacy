// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Diagnostics;
using System.IO;
using LibGit2Sharp;

namespace osu.Game.Rulesets.Karaoke.Online.API.Git
{
    /// <summary>
    ///     it chntains several finction
    ///     1. checkout
    ///     2. push
    ///     3. pull
    ///     4. commit
    /// </summary>
    public class GitApi
    {
        /// <summary>
        ///     Clone project
        /// </summary>
        /// <returns></returns>
        public bool CloneProject(string cloneUrl, string cloneDir, string userName = null, string password = null)
        {
            try
            {
                if (userName != null)
                {
                    var co = new CloneOptions
                    {
                        CredentialsProvider = (url, user, cred) => new UsernamePasswordCredentials { Username = userName, Password = password }
                    };
                    Repository.Clone(cloneUrl, cloneDir, co);
                }
                else
                {
                    Repository.Clone(cloneUrl, cloneDir);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        ///     Pull project
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool PullProject(string dir, string userName = null, string password = null, string email = null)
        {
            using (var repo = new Repository(dir))
            {
                var options = new PullOptions();

                //Create provider
                options.FetchOptions = new FetchOptions
                {
                    CredentialsProvider = (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials
                        {
                            Username = userName,
                            Password = password
                        }
                };

                //if pull success, create a commit
                options.MergeOptions.CommitOnSuccess = true;

                //create signature
                var signature = new Signature(userName, email, new DateTimeOffset(DateTime.Now));

                //pull 
                repo.Network.Pull(signature, options);
            }

            return true;
        }

        /// <summary>
        ///     Write a file to working dictionary
        /// </summary>
        /// <returns></returns>
        public bool WriteFile(string dir, string filename, string content)
        {
            using (var repo = new Repository("path/to/your/repo"))
            {
                // Write content to file system
                File.WriteAllText(Path.Combine(repo.Info.WorkingDirectory, filename), content);
            }
            return true;
        }

        /// <summary>
        ///     Commit project
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CommitProject(string dir, string[] fileNames, string userName = null, string password = null, string email = null)
        {
            using (var repo = new Repository("path/to/your/repo"))
            {
                // Stage the file
                foreach (var filename in fileNames)
                    Commands.Stage(repo, filename);

                // Create the committer's signature and commit
                var author = new Signature("James", "@jugglingnutcase", DateTime.Now);
                var committer = author;

                // Commit to the repository
                var commit = repo.Commit("Here's a commit i made!", author, committer);
            }

            return true;
        }

        /// <summary>
        ///     Push
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="branchName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool PushProject(string dir, string branchName, string userName = null, string password = null)
        {
            using (var repo = new Repository("path/to/your/repo"))
            {
                var options = new PushOptions();
                options.CredentialsProvider = (url, usernameFromUrl, types) =>
                    new UsernamePasswordCredentials
                    {
                        Username = userName,
                        Password = password
                    };
                repo.Network.Push(repo.Branches[branchName], options);
            }

            return true;
        }

        /// <summary>
        ///     TODO : I'm not sure it can use as pull request
        /// </summary>
        /// <returns></returns>
        public bool PushToRemote(string dir, string remoteUrl, string branchName, string userName = null, string password = null)
        {
            using (var repo = new Repository(dir))
            {
                var remote = repo.Network.Remotes[branchName];
                var options = new PushOptions
                {
                    CredentialsProvider = (url, user, cred) =>
                        new UsernamePasswordCredentials { Username = userName, Password = password }
                };
                repo.Network.Push(remote, @"refs/heads/master", options);
            }

            return true;
        }
    }
}
