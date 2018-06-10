// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Game.Graphics.UserInterface;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Text;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables
{
    /// <summary>
    /// Editable karaoke Drawable Object
    /// Right click :
    /// Translate >> Add
    /// </summary>
    public class DrawableEditableKaraokeObject : DrawableLyric, IHasContextMenu
    {
        protected DrawableKaraokeThumbnail DrawableKaraokeThumbnail { get; set; }
        protected EditableMainKaraokeText EditableMainKaraokeText { get; set; } = new EditableMainKaraokeText(null, null);
        protected bool IsDrag = false;

        public MenuItem[] ContextMenuItems => new MenuItem[]
        {
            new OsuMenuItem(@"Some option"),
            new OsuMenuItem(@"Highlighted option", MenuItemType.Highlighted),
            new OsuMenuItem(@"Another option"),
            new OsuMenuItem(@"Choose me please"),
            new OsuMenuItem(@"And me too"),
            new OsuMenuItem(@"Trying to fill"),
            new OsuMenuItem(@"Destructive option", MenuItemType.Destructive),
        };

        public DrawableEditableKaraokeObject(BaseLyric hitObject)
            : base(hitObject)
        {
            DrawableKaraokeThumbnail = new DrawableKaraokeThumbnail(Lyric)
            {
                Position = new Vector2(0, -100),
                Width = 300,
                Height = 100,
            };
            AddInternal(EditableMainKaraokeText);
            AddInternal(DrawableKaraokeThumbnail);
        }

        protected override void UpdateDrawable()
        {
            base.UpdateDrawable();
            EditableMainKaraokeText.MainTextObject = Lyric.Lyric.ToDictionary(k => k.Key, v => (TextComponent)v.Value);
            EditableMainKaraokeText.TextObject = Template?.Value?.MainText;
            EditableMainKaraokeText.Alpha = 1f;
        }

        #region Input

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            IsDrag = true;
            int index = GetPointedText(state);
            EditableMainKaraokeText.StartSelectIndex = index;

            return base.OnMouseDown(state, args);
        }

        //detect whith text is picked
        protected override bool OnMouseMove(InputState state)
        {
            if (!IsDrag)
            {
                int index = GetPointedText(state);
                EditableMainKaraokeText.HoverIndex = index;
            }
            else
            {
                int index = GetPointedText(state);
                EditableMainKaraokeText.EndSelectIndex = index;
            }

            return base.OnMouseMove(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            IsDrag = false;
            int index = GetPointedText(state);
            AddPoint(index);
            EditableMainKaraokeText.StartSelectIndex = null;
            EditableMainKaraokeText.EndSelectIndex = null;

            return base.OnMouseUp(state, args);
        }

        protected override void OnHoverLost(InputState state)
        {
            base.OnHoverLost(state);
            EditableMainKaraokeText.HoverIndex = null;
        }

        protected int GetPointedText(InputState state)
        {
            var mousePosition = ToLocalSpace(state.Mouse.NativeState.Position);
            return EditableMainKaraokeText.GetIndexByPosition(mousePosition.X);
        }

        #endregion

        public void AddPoint(int index)
        {
            KeyValuePair<int, LyricTimeLine> previousPoint = Lyric.TimeLines.GetFirstProgressPointByIndex(index);
            KeyValuePair<int, LyricTimeLine> nextPoint = Lyric.TimeLines.GetLastProgressPointByIndex(index);
            double deltaTime = ((previousPoint.Value?.RelativeTime ?? 0) + (nextPoint.Value?.RelativeTime ?? (previousPoint.Value?.RelativeTime ?? 0) + 500)) / 2;
            LyricTimeLine point = new LyricTimeLine(deltaTime);
            Lyric.TimeLines.Add(index, point);
            DrawableKaraokeThumbnail.UpdateView();
        }

        public void AddTranslate(TranslateCode code, string translateResult)
        {
            //Add it into Karaoke object
            Lyric.Translates.Add(code, new LyricTranslate(translateResult));
        }
    }
}
