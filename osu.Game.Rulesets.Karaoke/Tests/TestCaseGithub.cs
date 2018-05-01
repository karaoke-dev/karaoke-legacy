// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Tests.Visual;
using OpenTK;


namespace osu.Game.Rulesets.Karaoke.Tests
{
    /// <summary>
    /// tase case about : 
    /// 1. login to github and get auth-code
    /// 2. fork project
    /// 3. checkout project
    /// 4. read file and write (Stream)
    /// 5. commit 
    /// 6. pull request 
    /// 7. compare pull request change (using git api or two branch)
    /// 8. commit pull request / view commit
    /// 9. appropve / decline request 
    /// 10.get webhock(optional)
    /// .
    /// refrence project :
    /// https://github.com/libgit2/libgit2sharp
    /// https://github.com/octokit/octokit.net
    /// </summary>
    [TestFixture]
    [Ignore("getting CI working")]
    [System.ComponentModel.Description("Test Github")]
    public class TestCaseGithub
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public TestCaseGithub()
        {
            
        }
    }
}
