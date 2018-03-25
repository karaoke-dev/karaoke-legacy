// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class BindableObject<T> : Bindable<T>
        where T : RecordChangeObject, ICopyable, new()
    {
        public BindableObject(T value)
            : base(value)
        {
        }

        public override T Value
        {
            get { return base.Value; }
            set
            {
                //if class changed
                if (value?.GetChanges()?.Any() ?? false)
                {
                    value.Initialize();
                    base.Value = value.Clone() as T;
                }
                else //class does not change
                {
                    base.Value = value;
                }
            }
        }

        /// <summary>
        /// Raise <see cref="Bindable{T}.ValueChanged"/> and <see cref="Bindable{T}.DisabledChanged"/> once, without any changes actually occurring.
        /// This does not propagate to any outward bound bindables.
        /// </summary>
        public override void TriggerChange()
        {
            TriggerValueChange(false);
            TriggerDisabledChange(false);
        }

        public static implicit operator T(BindableObject<T> value) => value?.Value ?? throw new InvalidCastException($"Casting a null {nameof(BindableObject<T>)} to a bool is likely a mistake");

        public override string ToString()
        {
            string bindableValue = JsonConvert.SerializeObject(Value);
            return bindableValue;
        }

        public override void Parse(object input)
        {
            try
            {
                Value = JsonConvert.DeserializeObject<T>(input as string);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }
    }
}
