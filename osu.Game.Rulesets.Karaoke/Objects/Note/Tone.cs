// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

namespace osu.Game.Rulesets.Karaoke.Objects.Note
{
    public class Tone
    {
        public int Scale { get; set; }

        public bool Helf { get; set; }

        public Tone()
        {
        }

        public Tone(int scale, bool helf = false)
        {
            Scale = scale;
            Helf = helf;
        }

        /// <summary>
        ///     operator
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static Tone operator +(Tone object1, Tone object2)
        {
            if (object1 == null && object2 == null)
                return null;

            if (object1 == null)
                return object2;

            if (object2 == null)
                return object1;

            return new Tone
            {
                Scale = object1.Scale + object2.Scale + (object1.Helf && object2.Helf ? 1 : 0),
                Helf = object1.Helf ^ object2.Helf
            };
        }

        /// <summary>
        ///     operator
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static Tone operator -(Tone object1, Tone object2)
        {
            return object1 + -object2;
        }

        /// <summary>
        ///     operator
        /// </summary>
        /// <param name="object1"></param>
        /// <returns></returns>
        public static Tone operator -(Tone object1)
        {
            if (object1 == null)
                return null;

            return new Tone
            {
                Scale = -object1.Scale,
                Helf = object1.Helf
            };
        }

        public void RaiseScale()
        {
            if (Helf)
                Scale++;

            Helf = !Helf;
        }

        public void ReduceScales()
        {
            if (Helf)
                Scale--;

            Helf = !Helf;
        }
    }
}
