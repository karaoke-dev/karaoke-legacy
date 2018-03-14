using System;
using Newtonsoft.Json;
using osu.Framework.Configuration;

namespace osu.Game.Rulesets.Karaoke.Configuration
{
    public class BindableObject<T> : Bindable<T>
        where T : class 
    {
        public BindableObject(T value)
            : base(value)
        {

        }

        public static implicit operator T(BindableObject<T> value) => value?.Value ?? throw new InvalidCastException($"Casting a null {nameof(BindableObject<T>)} to a bool is likely a mistake");

        public override string ToString()
        {
            string bindableValue = JsonConvert.SerializeObject(Value);
            return bindableValue;
        }

        public override void Parse(object input)
        {
            Value = JsonConvert.DeserializeObject<T>(input as string);
        }
    }
}
