// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Karaoke.Objects
{
    /// <summary>
    /// Inherit this object can record the number of property has changed
    /// </summary>
    public class RecordChangeObject
    {
        private readonly Dictionary<string, object> mOriginalValues = new Dictionary<string, object>();

        public RecordChangeObject()
        {
            Initialize();
        }

        public void Initialize()
        {
            var properties = GetType().GetProperties();

            mOriginalValues.Clear();

            // Save the current value of the properties to our dictionary.
            foreach (var property in properties) mOriginalValues.Add(property.Name, property.GetValue(this));
        }

        public Dictionary<string, object> GetChanges()
        {
            var properties = GetType().GetProperties();
            var latestChanges = new Dictionary<string, object>();

            // Get all properties
            var tempProperties = GetType().GetProperties().ToArray();

            // Filter properties by only getting what has changed
            properties = tempProperties.Where(p => !Equals(p.GetValue(this, null), mOriginalValues[p.Name])).ToArray();

            foreach (var property in properties)
                if (!latestChanges.Keys.Contains(property.Name))
                    latestChanges.Add(property.Name, property.GetValue(this));

            return latestChanges;
        }
    }
}
