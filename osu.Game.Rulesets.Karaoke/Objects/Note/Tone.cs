using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Karaoke.Objects.Note
{
    public class Tone 
    {
        public int Scale { get; set; }

        public bool Helf { get; set; }

        public Tone()
        {

        }

        public Tone(int scale,bool helf = false)
        {
            Scale = scale;
            Helf = helf;
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
