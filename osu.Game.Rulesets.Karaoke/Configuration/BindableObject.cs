// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class BindableObject<T> : Bindable<T>
        where T :class, IEquatable<T> , ICloneable, new() 
    {
        public override T Value
        {
            get => base.Value;
            set
            {
                if (value == null && base.Value == null)
                    return;

                //create clone object
                var cloneValue = (T)value?.Clone();

                //if class priperty changed
                /*
                if(!PublicInstancePropertiesEqual<T>(base.Value, cloneValue))
                {
                    base.Value = cloneValue;
                }
                */
                
                if (!(base.Value?.Equals(cloneValue) ?? false)) 
                {
                    base.Value = cloneValue;
                }
            }
        }

        /// <summary>
        /// use this method will not need <see cref="IEquatable{T}"/> , <see cref="ICloneable"/> anymore
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        protected bool PublicInstancePropertiesEqual<U>(T self, T to) where U : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                    object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        return false;
                    }
                }
                return true;
            }
            return self == to;
        }

        public BindableObject(T value)
            : base(value)
        {

        }

        /// <summary>
        ///     Raise <see cref="Bindable{T}.ValueChanged" /> and <see cref="Bindable{T}.DisabledChanged" /> once, without any
        ///     changes actually occurring.
        ///     This does not propagate to any outward bound bindables.
        /// </summary>
        public override void TriggerChange()
        {
            base.TriggerChange();
            /*
            TriggerValueChange(false);
            TriggerDisabledChange(false);
            */
        }

        public static implicit operator T(BindableObject<T> value) => value?.Value ?? throw new InvalidCastException($"Casting a null {nameof(BindableObject<T>)} to a {nameof(T)} is likely a mistake");

        public override string ToString()
        {
            var bindableValue = JsonConvert.SerializeObject(Value);
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
