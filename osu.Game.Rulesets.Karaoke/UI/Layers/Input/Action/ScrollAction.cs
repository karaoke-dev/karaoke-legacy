using osu.Game.Rulesets.Karaoke.Input;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Input.Action
{
    public class ScrollAction
    {
        public KaraokeScrollAction KaraokeScrollAction { get; set; }

        public bool Touch { get; set; }

        public double Value { get; set; }
    }
}
