// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Game.Rulesets.Karaoke.Configuration.Types;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action
{
    /// <summary>
    ///     Base action
    /// </summary>
    public class BaseAction : ICloneable, IEquatable<BaseAction> , IJsonString
    {
        public DateTime ActionTime = DateTime.Now;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual bool Equals(BaseAction other)
        {
            return ActionTime == other.ActionTime;
        }
    }
}
