using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;
using osu.Game.Rulesets.Karaoke.Objects.Lyric.Types;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;

namespace osu.Game.Rulesets.Karaoke.Edit.Tools
{
    public class LyricEditor
    {
        public LyricEditor()
        {

        }

        private BaseLyric _lyric;
        public BaseLyric TargetLyric
        {
            get => _lyric;
            set { _lyric = value; }
        }

        #region Method

        public void AddText(MainText insertAfter, MainText insertValue)
        {
            var index = TargetLyric.Lyric.Count;
            TargetLyric.Lyric.AddOrReplace(index, insertValue);
            ReArrangeKey(TargetLyric);
        }

        public void RemoveText(MainText removeValue)
        {
            if (TargetLyric.Lyric.TryGetKey(removeValue, out int key))
            {
                TargetLyric.Lyric.Remove(key);

                //TODO : timelines

                if (TargetLyric is IHasRomaji romajiLyric)
                {
                    romajiLyric.Romaji.TryToRemove(key);
                }
                if (TargetLyric is IHasFurigana furiganaLyric)
                {
                    furiganaLyric.Furigana.TryToRemove(key);
                }
            }

            ReArrangeKey(TargetLyric);
        }

        public void ReArrangedByText(BaseLyric lyric, string lyricArrangementText, string seperator)
        {
            //TODO : to complex

            ReArrangeKey(TargetLyric);
        }

        public void CombineText(BaseLyric lyric, MainText combineFrom, MainText combineTo)
        {
            ReArrangeKey(TargetLyric);
        }

        public void SeperateText(BaseLyric lyric, MainText seperateText, int from)
        {
            ReArrangeKey(TargetLyric);
        }

        public void AddFurigana(MainText addIn, FuriganaText furiganaText)
        {
            if (TargetLyric.Lyric.TryGetKey(addIn, out int key))
            {
                if (TargetLyric is IHasFurigana furiganaLric)
                {
                    furiganaLric.Furigana.AddOrReplace(key,furiganaText);
                }
            }
        }

        public void RemoveFurigana(BaseLyric lyric, MainText addIn)
        {

        }

        public void AddRomaji(BaseLyric lyric, MainText addIn, RomajiText romajiText)
        {

        }

        public void RemoveRomaji(BaseLyric lyric, MainText addIn)
        {

        }

        public void AddTimeline(MainText addIn, TimeLineIndex index)
        {
            var previousPoint = TargetLyric.TimeLines.GetFirstProgressPointByIndex(index);
            var nextPoint = TargetLyric.TimeLines.GetLastProgressPointByIndex(index);
            var deltaTime = ((previousPoint.Value?.RelativeTime ?? 0) + (nextPoint.Value?.RelativeTime ?? (previousPoint.Value?.RelativeTime ?? 0) + 500)) / 2;
            var point = new TimeLine(deltaTime);
        }

        public void AddTimeline(MainText addIn, TimeLine timeline)
        {
            //TargetLyric.TimeLines.Add();
        }

        public void RemoveTimeline(BaseLyric lyric, TimeLine timeline)
        {

        }

        public void AdjustTime(TimeLine timeline, double newTime)
        {

            AutoFixTime(TargetLyric);
        }

        public bool LyricFormatIsValid()
        {
            return false;
        }

        public void FixLyricFormat()
        {
            //TODO : do somethinig
        }

        #endregion

        #region Utilities

        protected void ReArrangeKey(BaseLyric lyric)
        {
            //move to new index
            int moveIndexFrom = 1000;
            foreach (var singleLyric in lyric.Lyric)
            {
                ReassignKey(lyric, singleLyric.Value, moveIndexFrom);
                moveIndexFrom++;
            }

            //rearrange new index
            int newStartIndex = 0;
            foreach (var singleLyric in lyric.Lyric)
            {
                ReassignKey(lyric, singleLyric.Value, newStartIndex);
                newStartIndex++;
            }

            //TODO : notified list changed
        }

        protected void ReassignKey(BaseLyric lyric, MainText text, int newIndex)
        {
            //if old key is in Lyrics
            if (TargetLyric.Lyric.TryGetKey(text, out int key))
            {
                TargetLyric.Lyric.ReassignKey(key, newIndex);

                //TODO : timelines

                if (TargetLyric is IHasRomaji romajiLyric)
                {
                    romajiLyric.Romaji.ReassignKey(key, newIndex);
                }
                if (TargetLyric is IHasFurigana furiganaLyric)
                {
                    furiganaLyric.Furigana.ReassignKey(key, newIndex);
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(text) + "does not in the Lyric");
            }
        }

        protected void AutoFixTime(BaseLyric lyric)
        {

        }

        #endregion


    }
}
