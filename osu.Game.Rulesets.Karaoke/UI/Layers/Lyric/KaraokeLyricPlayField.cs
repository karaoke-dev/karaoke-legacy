// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric.Types;
using osu.Game.Rulesets.Karaoke.UI.Layers.Type;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.UI;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Lyric
{
    /// <summary>
    ///     use to manage karaoke lyric's position arrangement
    ///     1.
    ///     |                   |
    ///     |                   |
    ///     |   karaoke         |
    ///     |           karaoke |
    ///     2.
    ///     |                       |
    ///     |      <!--scrolling--> |
    ///     |  karaoke   karaoke    |
    ///     |                       |
    ///     3.
    ///     |            ^  |
    ///     |   karaoke  |  |
    ///     |   karaoke  |  |
    ///     |   karaoke  |  |
    ///     4. more
    ///     2. 3. 4. will be implement until release
    /// </summary>
    public class KaraokeLyricPlayField : Playfield, IDrawableLyricBindable, ILayer
    {
        public KaraokeRulesetContainer KaraokeRulesetContainer { get; set; }
        public List<IDrawableLyricParameter> ListDrawableKaraokeObject { get; set; } = new List<IDrawableLyricParameter>();

        //bindable
        public BindableObject<KaraokeLyricConfig> Style { get; set; } = new BindableObject<KaraokeLyricConfig>(new KaraokeLyricConfig());

        public BindableObject<LyricTemplate> Template { get; set; } = new BindableObject<LyricTemplate>(new LyricTemplate());
        public BindableObject<SingerTemplate> SingerTemplate { get; set; } = new BindableObject<SingerTemplate>(new SingerTemplate());
        public Bindable<TranslateCode> TranslateCode { get; set; } = new Bindable<TranslateCode>();

        /// <summary>
        ///     update combo by last object
        /// </summary>
        /// <param name="lyric"></param>
        public static void UpdateObjectCombo(BaseLyric lyric)
        {
        }

        /// <summary>
        ///     automatically update preemptive time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="karaokeObject"></param>
        public static void UpdateObjectPreemptiveTime(DrawableLyric karaokeObject)
        {
        }

        public override void Add(DrawableHitObject h)
        {
            h.Depth = (float)h.HitObject.StartTime;

            if (h is DrawableLyric drawableLyric)
            {
                //binding
                Style.BindTo(Style);
                Template.BindTo(Template);
                SingerTemplate.BindTo(SingerTemplate);
                TranslateCode.BindTo(TranslateCode);

                //update template
                UpdateObjectTemplate(drawableLyric);

                //update position
                UpdateObjectAutomaticallyPosition(drawableLyric);

                //add to list
                ListDrawableKaraokeObject.Add(drawableLyric);

                base.Add(h);
            }
        }

        public void UpdateObjectTemplate(DrawableLyric drawableKaraokeObject)
        {
            /*
            //get template 
            LyricTemplate template = null;
            if (drawableKaraokeObject.BaseLyric.TemplateIndex != null)
            {
                template = GetListKaraokeTemplate()[drawableKaraokeObject.BaseLyric.TemplateIndex.Value];
            }

            //setting drawable by template
            if (template != null)
            {
                drawableKaraokeObject.Template = template;
            }
            */
        }

        /// <summary>
        ///     get list template
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public List<LyricTemplate> GetListKaraokeTemplate()
        {
            var listTemplates = new List<LyricTemplate>
            {
                new LyricTemplate()
            };
            return listTemplates;
        }

        /// <summary>
        ///     update position
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="karaokeObject"></param>
        public void UpdateObjectAutomaticallyPosition(DrawableLyric drawableKaraokeObject)
        {
            //get position
            KaraokePosition position = null;
            var index = GetListKaraokeObjects().IndexOf(drawableKaraokeObject.HitObject);
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
        ///     get list karaoke object
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public List<BaseLyric> GetListKaraokeObjects()
        {
            return KaraokeRulesetContainer.Beatmap.HitObjects;
        }

        /// <summary>
        ///     get list position template
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public List<KaraokePosition> GetListKaraokePosition()
        {
            var listTemplates = new List<KaraokePosition>
            {
                new KaraokePosition
                {
                    Position = new Vector2(200, 300),
                    Anchor = Anchor.CentreLeft
                },
                new KaraokePosition
                {
                    Position = new Vector2(400, 370),
                    Anchor = Anchor.CentreRight
                }
            };
            return listTemplates;
        }
    }
}
