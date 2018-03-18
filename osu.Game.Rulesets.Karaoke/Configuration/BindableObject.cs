using System;
using System.Collections.Generic;
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

        /*
        public override T Value
        {
            get { return base.Value; }
            set
            {
                
                if (Disabled)
                    throw new InvalidOperationException($"Can not set value to \"{value.ToString()}\" as bindable is disabled.");

                base.Value = value;

                //TODO : 會造成無限遞迴
                TriggerValueChange();
            }
        }
        */

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
