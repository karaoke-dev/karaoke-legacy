using osu.Game.Rulesets.Karaoke.UI;
using osu.Game.Rulesets.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Karaoke.Edit.Drawables;

namespace osu.Game.Rulesets.Karaoke.Edit
{
    public class KaraokeEditRulesetContainer : KaraokeRulesetContainer
    {
        public KaraokeEditRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset) : base(ruleset, beatmap, isForCurrentRuleset)
        {

        }

        /// <summary>
        /// create editable HitObject
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        protected override DrawableHitObject<KaraokeObject> GetVisualRepresentation(KaraokeObject h)
        {
            if (h is KaraokeObject karaokeObject)
            {
                return new DrawableEditableKaraokeObject(karaokeObject);
            }
            return null;
        }

        protected override Playfield CreatePlayfield() => new KaraokeEditPlayfield(Ruleset, WorkingBeatmap, this);
    }
}
