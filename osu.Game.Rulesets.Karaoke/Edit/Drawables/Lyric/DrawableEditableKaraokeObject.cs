// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Thumbnail;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;
using osu.Game.Rulesets.Karaoke.Objects.Translate;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables.Lyric
{
    /// <summary>
    ///     Editable karaoke Drawable Object
    ///     Right click :
    ///     Translate >> Add
    /// </summary>
    public class DrawableEditableKaraokeObject : DrawableLyric, IHasContextMenu
    {
        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new OsuMenuItem(@"Some option"),
            new OsuMenuItem(@"Highlighted option", MenuItemType.Highlighted),
            new OsuMenuItem(@"Another option"),
            new OsuMenuItem(@"Choose me please"),
            new OsuMenuItem(@"And me too"),
            new OsuMenuItem(@"Trying to fill"),
            new OsuMenuItem(@"Destructive option", MenuItemType.Destructive)
        };

        protected DrawableKaraokeThumbnail DrawableKaraokeThumbnail { get; set; }
        protected EditableLyricText EditableLyricText { get; set; }

        public DrawableEditableKaraokeObject(BaseLyric hitObject)
            : base(hitObject)
        {
            DrawableKaraokeThumbnail = new DrawableKaraokeThumbnail(Lyric)
            {
                Position = new Vector2(0, -100),
                Width = 300,
                Height = 100
            };

            EditableLyricText = new EditableLyricText
            {
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
            };


            EditableLyricText.Lyric = hitObject;
            EditableLyricText.AddPointAction += AddPoint;
            AddInternal(EditableLyricText);
            AddInternal(DrawableKaraokeThumbnail);
        }

        public void AddPoint(TimeLineIndex index)
        {
            var previousPoint = Lyric.TimeLines.GetFirstProgressPointByIndex(index);
            var nextPoint = Lyric.TimeLines.GetLastProgressPointByIndex(index);
            var deltaTime = ((previousPoint.Value?.RelativeTime ?? 0) + (nextPoint.Value?.RelativeTime ?? (previousPoint.Value?.RelativeTime ?? 0) + 500)) / 2;
            var point = new TimeLine(deltaTime);
            Lyric.TimeLines.Add(index, point);
            DrawableKaraokeThumbnail.UpdateView();
        }

        public void AddTranslate(TranslateCode code, string translateResult)
        {
            //Add it into Karaoke object
            Lyric.Translates.Add(code, new LyricTranslate(translateResult));
        }

        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();
            EditableLyricText.Alpha = 1f;
        }
    }
}
