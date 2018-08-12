using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Lyric
{
    /// <summary>
    /// Lacagy lyric arrangement.
    /// </summary>
    public class LyricLagacyLayoutContainer : FlowContainer<DrawableLyric>
    {
        public List<float> LineSpacing { get; set; }

        public LyricLagacyLayoutContainer()
        {
            LineSpacing = new List<float>()
            {
                0,100
            };
        }

        public override void Add(DrawableLyric drawable)
        {
            base.Add(drawable);

            if (IsLoaded)
                ComputeLyricLayout();
        }

        public override bool Remove(DrawableLyric drawable)
        {
            var remove = base.Remove(drawable);

            if (IsLoaded)
                ComputeLyricLayout();

            return remove;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            ComputeLyricLayout();
        }

        /// <summary>
        /// Set object's margin and padding(left and right)
        /// </summary>
        protected virtual void ComputeLyricLayout()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                var lyric = Children[i];
                var layoutIndex = i % LineSpacing.Count;

                lyric.DisplayLayer = layoutIndex;
                lyric.Padding
                = new MarginPadding { Left = LineSpacing[layoutIndex], Right = 100 };
            }
        }


        /// <summary>
        /// re-compute layout
        /// </summary>
        protected override IEnumerable<Vector2> ComputeLayoutPositions()
        {
            //the child that only display on the screen
            var children = FlowingChildren.ToArray();
            if (children.Length == 0)
                return new List<Vector2>();

            Vector2[] result = new Vector2[children.Length];
            for (int i = 0; i < children.Length; i++)
            {
                var drawableLyric = Children.FirstOrDefault(x => x == children[i]);
                if (drawableLyric != null)
                {
                    if (drawableLyric.DisplayLayer != 0)
                    {
                        var pervous = Children.GetPrevious(drawableLyric);
                        var next = Children.GetNext(drawableLyric);
                        var perviousHeight = pervous?.Y + pervous?.DrawHeight ?? 0;
                        var nextHeight = next?.Y + next?.DrawHeight ?? 0;
                        result[i].Y = Math.Max(perviousHeight, nextHeight);
                    }
                }
            }


            return result;
        }
    }
}
