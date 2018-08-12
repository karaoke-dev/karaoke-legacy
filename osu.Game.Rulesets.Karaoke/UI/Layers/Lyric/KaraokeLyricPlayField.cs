// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Caching;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Extension;
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

        public KaraokeLyricPlayField()
        {
            RelativeSizeAxes = Axes.Both;
            Margin = new MarginPadding{Top = 350};
        }

        protected override HitObjectContainer CreateHitObjectContainer() => new LyricPlayFieldContainer();

        public class LyricPlayFieldContainer : HitObjectContainer
        {
            public List<float> LineSpacing { get; set; }
            private Cached layout = new Cached();
            protected void InvalidateLayout() => layout.Invalidate();

            public LyricPlayFieldContainer()
            {
                LineSpacing = new List<float>()
                {
                    0,100
                };
            }

            public override void Add(DrawableHitObject hitObject)
            {
                base.Add(hitObject);

                if (IsLoaded)
                    ComputeLyricLayout();
            }

            public override bool Remove(DrawableHitObject hitObject)
            {
                var remove = base.Remove(hitObject);

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
                for (int i = 0; i < InternalChildren.Count; i++)
                {
                    var lyric = InternalChildren[i] as DrawableLyric;
                    var layoutIndex = i % LineSpacing.Count;

                    if (lyric != null)
                    {
                        lyric.DisplayLayer = layoutIndex;
                        lyric.Padding
                            = new MarginPadding { Left = LineSpacing[layoutIndex], Right = 100 };
                    }
                }
            }

            public virtual void ComputeLayout()
            {
                foreach (var child in InternalChildren)
                {
                    if (child is DrawableLyric drawableLyric)
                    {
                        if (drawableLyric.DisplayLayer != 0)
                        {
                            var pervous = InternalChildren.GetPrevious(drawableLyric);
                            var next = InternalChildren.GetNext(drawableLyric);
                            var perviousHeight = pervous?.Y + pervous?.DrawHeight ?? 0;
                            var nextHeight = next?.Y + next?.DrawHeight ?? 0;
                            drawableLyric.Y = Math.Max(perviousHeight, nextHeight);
                        }
                    }
                }
            }

            #region FlowContainer

            protected override bool RequiresChildrenUpdate => base.RequiresChildrenUpdate || !layout.IsValid;

            private readonly Dictionary<Drawable, float> layoutChildren = new Dictionary<Drawable, float>();

            protected override void AddInternal(Drawable drawable)
            {
                layoutChildren.Add(drawable, 0f);
                // we have to ensure that the layout gets invalidated since Adding or Removing a child will affect the layout. The base class will not invalidate
                // if we are set to AutoSizeAxes.None, but even in that situation, the layout can and often does change when children are added/removed.
                InvalidateLayout();
                base.AddInternal(drawable);
            }

            protected override bool RemoveInternal(Drawable drawable)
            {
                layoutChildren.Remove(drawable);
                // we have to ensure that the layout gets invalidated since Adding or Removing a child will affect the layout. The base class will not invalidate
                // if we are set to AutoSizeAxes.None, but even in that situation, the layout can and often does change when children are added/removed.
                InvalidateLayout();
                return base.RemoveInternal(drawable);
            }

            protected override void ClearInternal(bool disposeChildren = true)
            {
                layoutChildren.Clear();
                // we have to ensure that the layout gets invalidated since Adding or Removing a child will affect the layout. The base class will not invalidate
                // if we are set to AutoSizeAxes.None, but even in that situation, the layout can and often does change when children are added/removed.
                InvalidateLayout();
                base.ClearInternal(disposeChildren);
            }

            protected override bool UpdateChildrenLife()
            {
                bool changed = base.UpdateChildrenLife();

                if (changed)
                    InvalidateLayout();

                return changed;
            }

            public override void InvalidateFromChild(Invalidation invalidation, Drawable source = null)
            {
                //Colour captures potential changes in IsPresent. If this ever becomes a bottleneck,
                //Invalidation could be further separated into presence changes.
                if ((invalidation & (Invalidation.RequiredParentSizeToFit | Invalidation.Colour)) > 0)
                    InvalidateLayout();

                base.InvalidateFromChild(invalidation, source);
            }

            protected override void UpdateAfterChildren()
            {
                base.UpdateAfterChildren();

                if (!layout.IsValid)
                {
                    ComputeLayout();
                    layout.Validate();
                }
            }

            #endregion
        }
    }
}
