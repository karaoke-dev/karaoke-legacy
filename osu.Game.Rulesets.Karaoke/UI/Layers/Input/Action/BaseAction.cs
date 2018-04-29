// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action
{
    /// <summary>
    /// Base action
    /// </summary>
    public class BaseAction : RecordChangeObject, ICopyable
    {
        public virtual T Copy<T>() where T : class, ICopyable, new()
        {
            throw new NotImplementedException();
        }
    }
}
