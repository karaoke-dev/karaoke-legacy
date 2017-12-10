using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Karaoke.Edit.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Edit.Drawables
{
    /// <summary>
    /// Karaoke's Thumbnail
    /// will show the word's seperate word and seperate time
    /// </summary>
    public class DrawableKaraokeThumbnail : Container
    {
        public KaraokeObject KaraokeObject { get; set; }

        public FillFlowContainer<EditableProgressPoint> ListEditableProgressPoint { get; set; } = new FillFlowContainer<EditableProgressPoint>()
        {
            Width=300,
            Height=100,
        };

        public DrawableKaraokeThumbnail(KaraokeObject karaokeObject)
        {
            KaraokeObject = karaokeObject;
            Add(ListEditableProgressPoint);
            UpdateView();
        }
        /// <summary>
        /// update UI
        /// </summary>
        public void UpdateView()
        {
            //1. show the whole bar with start time and end time

            //2. show each point with text start and end time
            ListEditableProgressPoint.Direction = FillDirection.Horizontal;
            ListEditableProgressPoint.Clear();
            foreach (var single in KaraokeObject.ListProgressPoint)
            {
                var editableProgressPoint = new EditableProgressPoint(this, single);
                ListEditableProgressPoint.Add(editableProgressPoint);
            }
        }

        /// <summary>
        /// just update progresspoint's position and startEndPosition
        /// </summary>
        public void UpdatePosition()
        {

        }

        /// <summary>
        /// Delete single point
        /// </summary>
        public void DeletePoint(ProgressPoint point)
        {
            if (KaraokeObject.ListProgressPoint.Count > 1)
            {
                KaraokeObject.ListProgressPoint.Remove(point);
            }
            UpdateView();
        }

        
    }
}
