// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Karaoke.Input;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action
{
    public class TapAction : BaseAction
    {
        public KaraokeTapAction KaraokeTapAction { get; set; }

        public bool Tap { get; set; }

        /// <summary>
        /// Copy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T Copy<T>()
        {
            T result = new T();
            if (result is TapAction keyAction)
            {
                keyAction.KaraokeTapAction = KaraokeTapAction;
                keyAction.Tap = Tap;
                keyAction.Initialize();
            }
            return result;
        }
    }
}
