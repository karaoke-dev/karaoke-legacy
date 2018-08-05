using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Beatmaps;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Note;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.UI.Layers.Note;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Karaoke.Edit.Layers.Note
{
    public class KaraokeToneEditPlayfield : KaraokeTonePlayfield
    {
        public KaraokeToneEditPlayfield(List<KaraokeStageDefinition> stageDefinitions)
            : base(stageDefinitions)
        {
        }

        public override void Add(DrawableHitObject h)
        {
            //Create object
            var drawableNote = new DrawableEditableKaraokeNoteGroup(h.HitObject as BaseLyric)
            {
                AccentColour = Color4.Blue
            };

            //regist event
            drawableNote.NoteSpeed.BindTo(VisibleTimeRange);

            //然後根據事件去做物件的加減
            GetStageByColumn(drawableNote.HitObject.SingerIndex ?? 0).Add(drawableNote);
        }
    }
}
