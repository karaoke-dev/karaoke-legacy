// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Objects;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Extension
{
    /// <summary>
    /// get the "Karaokeobject" of playField
    /// </summary>
    public static class PlayFieldObjectExtension
    {
        /// <summary>
        /// update combo by last object
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="karaokeObject"></param>
        public static void UpdateObjectCombo(this IAmKaraokeField karaokeField, KaraokeObject karaokeObject)
        {
        }

        public static void UpdateObjectTemplate(this IAmKaraokeField karaokeField, DrawableKaraokeObject drawableKaraokeObject)
        {
            //get template 
            KaraokeTemplate template = null;
            if (drawableKaraokeObject.KaraokeObject.TemplateIndex != null)
            {
                template = karaokeField.GetListKaraokeTemplate()[drawableKaraokeObject.KaraokeObject.TemplateIndex.Value];
            }

            //setting drawable by template
            if (template != null)
            {
                drawableKaraokeObject.Template = template;
            }
        }

        /// <summary>
        /// update position
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="karaokeObject"></param>
        public static void UpdateObjectAutomaticallyPosition(this IAmKaraokeField karaokeField, DrawableKaraokeObject drawableKaraokeObject)
        {
            //get position
            KaraokePosition position = null;
            int index = karaokeField.GetListKaraokeObjects().IndexOf(drawableKaraokeObject.HitObject);
            if (index % 2 == 0)
                drawableKaraokeObject.KaraokeObject.PositionIndex = 0;
            else
                drawableKaraokeObject.KaraokeObject.PositionIndex = 1;

            if (drawableKaraokeObject.KaraokeObject.PositionIndex != null)
            {
                position = karaokeField.GetListKaraokePosition()[drawableKaraokeObject.KaraokeObject.PositionIndex.Value];

                drawableKaraokeObject.Position = position.Position;
            }
        }

        /// <summary>
        /// automatically update preemptive time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="karaokeObject"></param>
        public static void UpdateObjectPreemptiveTime(this IAmKaraokeField karaokeField, DrawableKaraokeObject karaokeObject)
        {
        }

        /// <summary>
        /// get list template
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static List<KaraokeTemplate> GetListKaraokeTemplate(this IAmKaraokeField karaokeField)
        {
            List<KaraokeTemplate> listTemplates = new List<KaraokeTemplate>
            {
                new KaraokeTemplate()
            };
            return listTemplates;
        }

        /// <summary>
        /// get list position template
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static List<KaraokePosition> GetListKaraokePosition(this IAmKaraokeField karaokeField)
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
        /// get list karaoke object
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static List<KaraokeObject> GetListKaraokeObjects(this IAmKaraokeField karaokeField)
        {
            return karaokeField.KaraokeRulesetContainer.Beatmap.HitObjects;
        }


        /// <summary>
        /// get list HitObjects
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static List<HitObject> GetListHitObjects(this IAmKaraokeField karaokeField)
        {
            return karaokeField.WorkingBeatmap.Beatmap.HitObjects;
        }
    }
}
