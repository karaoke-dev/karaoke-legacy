using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Rulesets.Karaoke.UI.Tool;
using OpenTK;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Karaoke.UI.Extension;
using osu.Framework.Graphics;

namespace osu.Game.Rulesets.Karaoke.UI
{
    public class KaraokeBasePlayfield : Playfield, IAmKaraokeField
    {
        public Ruleset Ruleset { get; set; }
        public WorkingBeatmap WorkingBeatmap { get; set; }
        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }
        public KaraokeFieldTool KaraokeFieldTool { get; } = new KaraokeFieldTool();
        public List<IAmDrawableKaraokeObject> ListDrawableKaraokeObject { get; set; } = new List<IAmDrawableKaraokeObject>();

        public static readonly Vector2 BASE_SIZE = new Vector2(512, 384);

        public override Vector2 Size
        {
            get
            {
                var parentSize = Parent.DrawSize;
                var aspectSize = parentSize.X * 0.75f < parentSize.Y ? new Vector2(parentSize.X, parentSize.X * 0.75f) : new Vector2(parentSize.Y * 4f / 3f, parentSize.Y);

                return new Vector2(aspectSize.X / parentSize.X, aspectSize.Y / parentSize.Y) * base.Size;
            }
        }

        public KaraokeBasePlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container)
            : base(BASE_SIZE.X)
        {
            Ruleset = ruleset;
            WorkingBeatmap = beatmap;
            KaraokeRulesetContainer = container;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        public override void Add(DrawableHitObject h)
        {
            h.Depth = (float)h.HitObject.StartTime;

            //update template
            this.UpdateObjectTemplate(h as DrawableKaraokeObject);

            //update position
            this.UpdateObjectAutomaticallyPosition(h as DrawableKaraokeObject);

            //add to list
            ListDrawableKaraokeObject.Add(h as DrawableKaraokeObject);

            base.Add(h);
        }
    }
}
