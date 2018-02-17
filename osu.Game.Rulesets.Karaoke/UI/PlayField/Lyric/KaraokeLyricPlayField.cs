using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using OpenTK;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Framework.Allocation;
using Newtonsoft.Json;

namespace osu.Game.Rulesets.Karaoke.UI.PlayField
{
    /// <summary>
    /// use to manage karaoke lyric's position arrangement
    /// 1. 
    /// |                   |
    /// |                   |
    /// |   karaoke         |
    /// |           karaoke |
    /// 
    /// 2.
    /// |                       |
    /// |      <!--scrolling--> |
    /// |  karaoke   karaoke    |
    /// |                       |
    /// 
    /// 3.
    /// |            ^  |
    /// |   karaoke  |  |
    /// |   karaoke  |  |
    /// |   karaoke  |  |
    /// 
    /// 4. more
    /// 
    /// 2. 3. 4. will be implement until release
    /// </summary>
    public class KaraokeLyricPlayField : Playfield
    {
        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }

        public List<IAmDrawableKaraokeObject> ListDrawableKaraokeObject { get; set; } = new List<IAmDrawableKaraokeObject>();

        public KaraokeLyricConfig Style { get; set; }
        public LyricTemplate Template { get; set; }
        public Singer Singer { get; set; }

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

        public void UpdateObjectTemplate(DrawableKaraokeObject drawableKaraokeObject)
        {
            //get template 
            LyricTemplate template = null;
            if (drawableKaraokeObject.Lyric.TemplateIndex != null)
            {
                template = this.GetListKaraokeTemplate()[drawableKaraokeObject.Lyric.TemplateIndex.Value];
            }

            //setting drawable by template
            if (template != null)
            {
                drawableKaraokeObject.Template = template;
            }
        }

        /// <summary>
        /// get list template
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public List<LyricTemplate> GetListKaraokeTemplate()
        {
            List<LyricTemplate> listTemplates = new List<LyricTemplate>
            {
                new LyricTemplate()
            };
            return listTemplates;
        }

        /// <summary>
        /// update position
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="karaokeObject"></param>
        public void UpdateObjectAutomaticallyPosition(DrawableKaraokeObject drawableKaraokeObject)
        {
            //get position
            KaraokePosition position = null;
            int index = GetListKaraokeObjects().IndexOf(drawableKaraokeObject.HitObject);
            if (index % 2 == 0)
                drawableKaraokeObject.Lyric.PositionIndex = 0;
            else
                drawableKaraokeObject.Lyric.PositionIndex = 1;

            if (drawableKaraokeObject.Lyric.PositionIndex != null)
            {
                position = GetListKaraokePosition()[drawableKaraokeObject.Lyric.PositionIndex.Value];

                drawableKaraokeObject.Position = position.Position;
            }
        }

        /// <summary>
        /// get list karaoke object
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public List<Lyric> GetListKaraokeObjects()
        {
            return KaraokeRulesetContainer.Beatmap.HitObjects;
        }

        /// <summary>
        /// get list position template
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public List<KaraokePosition> GetListKaraokePosition()
        {
            List<KaraokePosition> listTemplates = new List<KaraokePosition>
            {
                new KaraokePosition()
                {
                    Position = new Vector2(0, 200),
                    Anchor = Anchor.CentreLeft
                },
                new KaraokePosition()
                {
                    Position = new Vector2(200, 270),
                    Anchor = Anchor.CentreRight
                }
            };
            return listTemplates;
        }

        /// <summary>
        /// update combo by last object
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="lyric"></param>
        public static void UpdateObjectCombo(Lyric lyric)
        {

        }

        /// <summary>
        /// automatically update preemptive time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="karaokeObject"></param>
        public static void UpdateObjectPreemptiveTime(DrawableKaraokeObject karaokeObject)
        {

        }
    }
}
