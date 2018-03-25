// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Input;
using osu.Game.Rulesets.Karaoke.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action
{
    public class KeyAction : BaseAction
    {
        public KaraokeKeyAction KaraokeKeyAction { get; set; }

        public bool Press { get; set; }

        /// <summary>
        /// Copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T Copy<T>()
        {
            T result = new T();
            if (result is KeyAction keyAction)
            {
                keyAction.KaraokeKeyAction = KaraokeKeyAction;
                keyAction.Press = Press;
                keyAction.Initialize();
            }
            return result;
        }
    }
}
