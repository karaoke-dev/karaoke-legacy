// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
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

        public List<float> LineSpacing { get; set; }

        public KaraokeLyricPlayField()
        {
            RelativeSizeAxes = Axes.Both;
            //Direction = FillDirection.Vertical;
            LineSpacing = new List<float>
            {
                0,100
            };
        }

        /*
        public override void Add(DrawableLyric drawable)
        {
            int index = Children.Count - 1;
            int spacingCount = LineSpacing.Count;
            if (index >= spacingCount)
            {
                var targetLyricEndTime = Children[index - spacingCount].Lyric.EndTime;

                //set preemptive time
                Children[index].PreemptiveTime = Children[index].Lyric.StartTime - targetLyricEndTime - 10;
            }
            else if(index >= 0)
            {
                Children[index].PreemptiveTime = Children[index].Lyric.StartTime - 10;
            }
            base.Add(drawable);
        }

        protected override IEnumerable<Vector2> ComputeLayoutPositions()
        {
            var positions = base.ComputeLayoutPositions().ToArray();

            for (int i = 0; i < positions.Length; i++)
            {
                var lyric = Children[i];

                //TODO : compute layout
                var layoutIndex = i % LineSpacing.Count;
                positions[i].Y = positions[layoutIndex].Y;
                lyric.Margin
                    = new MarginPadding { Left = LineSpacing[layoutIndex], Right = 0 };
            }
            return positions;
        }
        */
    }
}
