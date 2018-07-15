// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using Newtonsoft.Json;
using osu.Game.Rulesets.Karaoke.Configuration.Types;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class SingerTemplate : ICloneable, IEquatable<SingerTemplate> , IJsonString
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(SingerTemplate other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SingerTemplate)obj);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /*
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        */
    }
}
