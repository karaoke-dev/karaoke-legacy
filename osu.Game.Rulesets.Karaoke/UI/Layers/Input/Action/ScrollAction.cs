// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action
{
    public class ScrollAction : RecordChangeObject, ICopyable
    {
        public KaraokeScrollAction KaraokeScrollAction { get; set; }

        public bool Touch { get; set; }

        public double Value { get; set; }

        /// <summary>
        /// Copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Copy<T>() where T : class, ICopyable, new()
        {
            T result = new T();
            if (result is ScrollAction keyAction)
            {
                keyAction.KaraokeScrollAction = KaraokeScrollAction;
                keyAction.Touch = Touch;
                keyAction.Value = Value;
                keyAction.Initialize();
            }
            return result;
        }
    }
}
