// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Linq;
using osu.Game.Rulesets.Karaoke.Extension;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Karaoke.Objects.Localization;
using osu.Game.Rulesets.Karaoke.Objects.Localization.Types;
using osu.Game.Rulesets.Karaoke.Objects.TimeLine;

namespace osu.Game.Rulesets.Karaoke.Edit.Tools
{
    public class LyricEditor
    {
        public LyricEditor()
        {
        }

        public LyricEditor(Lyric lyric)
        {
            TargetLyric = lyric;
        }

        private Lyric _lyric;

        public Lyric TargetLyric
        {
            get => _lyric;
            set { _lyric = value; }
        }

        #region Method

        public void AddText(MainText insertAfter, MainText insertValue)
        {
            var index = TargetLyric.MainLyric.Count;

            if (TargetLyric.MainLyric.AddOrReplace(index, insertValue))
            {
                CreateSingleTimeLine(TargetLyric, index);
                ReArrangeKey(TargetLyric);
            }
        }

        public void RemoveText(MainText removeValue)
        {
            if (TargetLyric.MainLyric.TryGetKey(removeValue, out int key))
            {
                TargetLyric.MainLyric.Remove(key);

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

        public void ReArrangedByText(Lyric lyric, string lyricArrangementText, string seperator)
        {
            //TODO : to complex

            ReArrangeKey(TargetLyric);
        }

        public void CombineText(Lyric lyric, MainText combineFrom, MainText combineTo)
        {
            ReArrangeKey(TargetLyric);
        }

        public void SeperateText(Lyric lyric, MainText seperateText, int from)
        {
            ReArrangeKey(TargetLyric);
        }

        public void AddFurigana(int key, FuriganaText furiganaText)
        {
            if (TargetLyric is IHasFurigana furiganaLric)
            {
                if (TargetLyric.MainLyric.ContainsKey(key))
                {
                    furiganaLric.Furigana.AddOrReplace(key, furiganaText);
                }
            }
        }

        public void RemoveFurigana(MainText removeIn)
        {
            if (TargetLyric is IHasFurigana furiganaLric)
            {
                if (TargetLyric.MainLyric.TryGetKey(removeIn, out int key))
                {
                    furiganaLric.Furigana.Remove(key);
                }
            }
        }

        public void AddRomaji(int key, RomajiText romajiText)
        {
            if (TargetLyric is IHasRomaji romajiLyric)
            {
                if (TargetLyric.MainLyric.ContainsKey(key))
                {
                    romajiLyric.Romaji.Add(key, romajiText);
                }
            }
        }

        public void RemoveRomaji(MainText removeIn)
        {
            if (TargetLyric is IHasRomaji romajiLyric)
            {
                if (TargetLyric.MainLyric.TryGetKey(removeIn, out int key))
                {
                    romajiLyric.Romaji.Remove(key);
                }
            }
        }

        public void AddTimeline(int index)
        {
            var previousPoint = TargetLyric.TimeLines.GetFirstProgressPointByIndex(index);
            var nextPoint = TargetLyric.TimeLines.GetLastProgressPointByIndex(index);
            var deltaTime = ((previousPoint.Value?.RelativeTime ?? 0) + (nextPoint.Value?.RelativeTime ?? (previousPoint.Value?.RelativeTime ?? 0) + 500)) / 2;
            var point = new TimeLine(deltaTime);
            AddTimeline(index, point);
        }

        public void AddTimeline(int index, TimeLine timeline)
        {
            if (!TargetLyric.TimeLines.ContainsKey(index))
            {
                TargetLyric.TimeLines.AddOrReplace(index, timeline);

                //TODO : check
            }
        }

        public void RemoveTimeline(int index)
        {
            if (TargetLyric.TimeLines.ContainsKey(index))
            {
                if (index.Percentage == 1)
                    return;

                var keysInLyricPart = TargetLyric.TimeLines.Keys.Where(x => x != index);
                if (keysInLyricPart.Count() >= 2)
                    TargetLyric.TimeLines.Remove(index);
            }
        }

        public void AdjustTime(int index, double newTime)
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

        protected void ReArrangeKey(Lyric lyric)
        {
            //move to new index
            int moveIndexFrom = 1000;
            foreach (var singleLyric in lyric.MainLyric)
            {
                ReassignKey(lyric, singleLyric.Value, moveIndexFrom);
                moveIndexFrom++;
            }

            //rearrange new index
            int newStartIndex = 0;
            foreach (var singleLyric in lyric.MainLyric)
            {
                ReassignKey(lyric, singleLyric.Value, newStartIndex);
                newStartIndex++;
            }

            //TODO : notified list changed
        }

        protected void ReassignKey(Lyric lyric, MainText text, int newIndex)
        {
            //if old key is in Lyrics
            if (TargetLyric.MainLyric.TryGetKey(text, out int key))
            {
                TargetLyric.MainLyric.ReassignKey(key, newIndex);

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
                throw new ArgumentNullException(nameof(text) + "does not in the MainLyric");
            }
        }

        protected void AutoFixTime(Lyric lyric)
        {
            foreach (var lyricPart in lyric.MainLyric)
            {
                var keysInLyricPart = lyric.TimeLines.Keys.Where(x => x != lyricPart.Key);

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

        protected void CreateSingleTimeLine(Lyric lyric, int key)
        {
            var newTimeLine = key;
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
