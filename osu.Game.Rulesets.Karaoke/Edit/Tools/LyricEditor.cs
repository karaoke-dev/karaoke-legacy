using System;
using System.Collections.Generic;
using System.Text;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Lyric;
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

        public void AddText(MainText insertAfter, MainText insertValue)
        {
            var index = TargetLyric.Lyric.Count;
            TargetLyric.Lyric.Add(index,insertValue);
            ReArrangeKey(TargetLyric);
        }

        public void RemoveText(MainText removeValue)
        {
            if (TargetLyric.Lyric.TryGetKey(removeValue, out int value))
            {
                //TODO : remove logic
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

        public void AddFurigana(BaseLyric lyric, MainText addIn, FuriganaText furiganaText)
        {

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

        public void AddTimeline(BaseLyric lyric, MainText addIn, TimeLine timeline)
        {

        }

        public void RemoveTimeline(BaseLyric lyric, TimeLine timeline)
        {

        }

        public void AdjustTime(BaseLyric lyric, TimeLine timeline,double newTime)
        {

        }

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

        }
    }
}
