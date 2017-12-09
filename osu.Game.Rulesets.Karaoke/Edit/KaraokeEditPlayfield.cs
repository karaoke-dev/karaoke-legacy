using osu.Game.Rulesets.Karaoke.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Beatmaps;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Edit.Drawables;
using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditPlayfield : KaraokeBasePlayfield
    {
        public int NowSelectedIndex { get; set; }

        /// <summary>
        /// Selected karaoke Object
        /// </summary>
        public DrawableEditableKaraokeObject NowSelectedKaraokeObject { get; set; }

        public KaraokeEditPlayfield(Ruleset ruleset, WorkingBeatmap beatmap, KaraokeRulesetContainer container) : base(ruleset, beatmap, container)
        {

        }

        /// <summary>
        /// Add : Add to editList
        /// 
        /// </summary>
        /// <param name="drawable"></param>
        public override void Add(DrawableHitObject h)
        {
            if(h is DrawableEditableKaraokeObject drawableEditableKaraokeObject)
            {

            }

            base.Add(h);
        }
    }
}
