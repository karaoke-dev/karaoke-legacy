using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Lyric;
using OpenTK;

namespace osu.Game.Rulesets.Karaoke.UI.Layers.Lyric.Components
{
    /// <summary>
    /// this container will auto-adjust <see cref="DrawableLyric"/>'s position
    /// </summary>
    public class LagacyKaraokeLyricContainer : Container<DrawableLyric> , ILyricContainer
    {
        public override void Add(DrawableLyric drawable)
        {
            //update position
            UpdateObjectAutomaticallyPosition(drawable);
            base.Add(drawable);
        }

        /// <summary>
        ///     get list karaoke object
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public List<DrawableLyric> Lyrics
        {
            get
            {
                return this.ToList();
            }
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
            var index = Lyrics.Select(x=>x.Lyric).IndexOf(drawableKaraokeObject.HitObject);
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
