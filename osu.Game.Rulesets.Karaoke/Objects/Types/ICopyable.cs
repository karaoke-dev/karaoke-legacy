// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Objects.Types
{
    /// <summary>
    /// ICopyable
    /// </summary>
    public interface ICopyable
    {
        /// <summary>
        /// copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Copy<T>() where T : class, ICopyable, new();
    }
}
