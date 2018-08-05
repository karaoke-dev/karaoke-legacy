using System;
using System.Collections.Generic;
using System.Linq;
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

        public LyricEditor(BaseLyric lyric)
        {
            TargetLyric = lyric;
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

            if (TargetLyric.Lyric.AddOrReplace(index, insertValue))
            {
                CreateSingleTimeLine(TargetLyric, index);
                ReArrangeKey(TargetLyric);
            }
           
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

                ReArrangeKey(TargetLyric);
            }
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

        public void AddFurigana(int key, FuriganaText furiganaText)
        {
            if (TargetLyric is IHasFurigana furiganaLric)
            {
                if (TargetLyric.Lyric.ContainsKey(key))
                {
                    furiganaLric.Furigana.AddOrReplace(key, furiganaText);
                }
            }
        }

        public void RemoveFurigana(MainText removeIn)
        {
            if (TargetLyric is IHasFurigana furiganaLric)
            {
                if (TargetLyric.Lyric.TryGetKey(removeIn, out int key))
                {
                    furiganaLric.Furigana.Remove(key);
                }
            }
        }

        public void AddRomaji(int key, RomajiText romajiText)
        {
            if (TargetLyric is IHasRomaji romajiLyric)
            {
                if (TargetLyric.Lyric.ContainsKey(key))
                {
                    romajiLyric.Romaji.Add(key, romajiText);
                }
            }
        }

        public void RemoveRomaji(MainText removeIn)
        {
            if (TargetLyric is IHasRomaji romajiLyric)
            {
                if (TargetLyric.Lyric.TryGetKey(removeIn, out int key))
                {
                    romajiLyric.Romaji.Remove(key);
                }
            }
        }

        public void AddTimeline(TimeLineIndex index)
        {
            var previousPoint = TargetLyric.TimeLines.GetFirstProgressPointByIndex(index);
            var nextPoint = TargetLyric.TimeLines.GetLastProgressPointByIndex(index);
            var deltaTime = ((previousPoint.Value?.RelativeTime ?? 0) + (nextPoint.Value?.RelativeTime ?? (previousPoint.Value?.RelativeTime ?? 0) + 500)) / 2;
            var point = new TimeLine(deltaTime);
            AddTimeline(index, point);
        }

        public void AddTimeline(TimeLineIndex index, TimeLine timeline)
        {
            if (!TargetLyric.TimeLines.ContainsKey(index))
            {
                TargetLyric.TimeLines.AddOrReplace(index, timeline);

                //TODO : check
            }
        }

        public void RemoveTimeline(TimeLineIndex index)
        {
            if (TargetLyric.TimeLines.ContainsKey(index))
            {
                if(index.Percentage == 1)
                    return;

                var keysInLyricPart = TargetLyric.TimeLines.Keys.Where(x => x.Index != index.Index);
                if (keysInLyricPart.Count() >= 2)
                    TargetLyric.TimeLines.Remove(index);
            }
        }

        public void AdjustTime(TimeLineIndex index,double newTime)
        {
            if (TargetLyric.TimeLines.ContainsKey(index))
            {
                var periousTimeLine = TargetLyric.TimeLines.GetPrevious(index);
                var nextTimeLine = TargetLyric.TimeLines.GetNext(index);

                var previousTime = periousTimeLine?.Value.RelativeTime ?? 0;
                var nextTime = nextTimeLine?.Value?.RelativeTime ?? previousTime + 100000;

                if (newTime > previousTime && newTime < nextTime)
                {
                    TargetLyric.TimeLines[index].RelativeTime = newTime;
                    AutoFixTime(TargetLyric);
                }
            }
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
            foreach (var lyricPart in lyric.Lyric)
            {
                var keysInLyricPart = lyric.TimeLines.Keys.Where(x => x.Index != lyricPart.Key);

                if (keysInLyricPart.Any())
                {
                    CreateSingleTimeLine(lyric, lyricPart.Key);
                }
                else if (keysInLyricPart.Last().Percentage != 1)
                {
                    keysInLyricPart.Last().Percentage = 1;
                }
            }
        }

        protected void CreateSingleTimeLine(BaseLyric lyric, int key)
        {
            var newTimeLine = new TimeLineIndex(key);
            var previusRelativeTime = lyric.TimeLines.GetPrevious(newTimeLine)?.Value.RelativeTime ?? 0;
            var nextRelativeTime = lyric.TimeLines.GetPrevious(newTimeLine)?.Value.RelativeTime ?? previusRelativeTime + 100;
            lyric.TimeLines.Add(newTimeLine, new TimeLine
            {
                RelativeTime = (previusRelativeTime + nextRelativeTime) / 2
            });
        }

        #endregion


    }
}
