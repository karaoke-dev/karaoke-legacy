using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// ICopyable
    /// </summary>
    interface ICopyable
    {
        /// <summary>
        /// copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Copy<T>() where T : new();
    }
}
