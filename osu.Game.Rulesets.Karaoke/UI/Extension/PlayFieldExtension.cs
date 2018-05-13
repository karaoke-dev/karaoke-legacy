// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using osu.Framework.Audio;
using osu.Game.Rulesets.Karaoke.Mods;
using osu.Game.Rulesets.Karaoke.UI.Interface;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.UI.Extension
{
    /// <summary>
    /// get the "state" of playField
    /// </summary>
    public static class PlayFieldExtension
    {
        /// <summary>
        /// if the number is larger , will have more preemp time
        /// </summary>
        public static double PrepareTime { get; set; } = 0;

        public static double Speed { get; set; } = 1;

        public static double Tone { get; set; } = 1;

        public static double Offset { get; set; } = 0;

        public static double Volumn { get; set; } = 1;

        /// <summary>
        /// NavigationToFirst
        /// </summary>
        /// <param name="karaokeField"></param>
        public static void NavigationToFirst(this IAmKaraokeField karaokeField)
        {
            double firstObject = karaokeField.FirstObjectTime();
            karaokeField.NavigateToTime(firstObject - PrepareTime);
        }

        /// <summary>
        /// NavigationToPrevious
        /// </summary>
        /// <param name="karaokeField"></param>
        public static void NavigationToPrevious(this IAmKaraokeField karaokeField)
        {
            int nowObjectIndex = karaokeField.FindObjectIndexByCurrentTime();
            if (nowObjectIndex > 1)
            {
                var list = karaokeField.GetListHitObjects();
                karaokeField.NavigateToTime(list[nowObjectIndex - 1].StartTime - PrepareTime);
            }
        }

        /// <summary>
        /// NavigationToNext
        /// </summary>
        /// <param name="karaokeField"></param>
        public static void NavigationToNext(this IAmKaraokeField karaokeField)
        {
            int nowObjectIndex = karaokeField.FindObjectIndexByCurrentTime();
            var list = karaokeField.GetListHitObjects();

            if (nowObjectIndex < list.Count - 2)
            {
                karaokeField.NavigateToTime(list[nowObjectIndex + 2].StartTime - PrepareTime);
            }
        }

        /// <summary>
        /// Play //TODO : still need to implement
        /// </summary>
        /// <param name="karaokeField"></param>
        public static void Play(this IAmKaraokeField karaokeField)
        {
            //karaokeField.WorkingBeatmap.Track.Start();
            karaokeField.WorkingBeatmap.Track.Rate = Speed;
            karaokeField.WorkingBeatmap.Track.Volume.Value = Volumn;
        }

        /// <summary>
        /// check is playing //TODO : still need to implement
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static bool IsPlaying(this IAmKaraokeField karaokeField)
        {
            return karaokeField.WorkingBeatmap.Track.IsRunning;
        }

        /// <summary>
        /// pause the song //TODO : still need to implement
        /// </summary>
        /// <param name="karaokeField"></param>
        public static void Pause(this IAmKaraokeField karaokeField)
        {
            //Play and pause are the same
            //karaokeField.WorkingBeatmap.Track.Stop();

            //use stupid method instead;
            karaokeField.WorkingBeatmap.Track.Rate = 0.1;
            Volumn = karaokeField.WorkingBeatmap.Track.Volume.Value;
            karaokeField.WorkingBeatmap.Track.Volume.Value = 0;
        }

        /// <summary>
        /// navigatte to target time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="value"></param>
        public static void NavigateToTime(this IAmKaraokeField karaokeField, double value)
        {
            karaokeField?.WorkingBeatmap?.Track?.Seek(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="value"></param>
        public static void AdjustSpeed(this IAmKaraokeField karaokeField, double value)
        {
            //refrence : IAdjustableClock.cs
            //TODO : fix if slide to fast will let song restart 
            Speed = value;
            karaokeField.WorkingBeatmap.Track.Rate = Speed;
        }

        public static double GetSpeed(this IAmKaraokeField karaokeField)
        {
            return karaokeField?.WorkingBeatmap?.Track?.Rate ?? 1;
        }

        public static void AdjustTone(this IAmKaraokeField karaokeField, double value)
        {
            if (karaokeField.WorkingBeatmap.Track is IHasPitchAdjust pitchAdjustTrack)
            {
                //karaokeField.WorkingBeatmap.Track.Reset();
                Tone = value;
                pitchAdjustTrack.PitchAdjust = Tone;
            }
        }

        public static double GetTone(this IAmKaraokeField karaokeField)
        {
            if (karaokeField?.WorkingBeatmap?.Track is IHasPitchAdjust pitchAdjustTrack)
            {
                return pitchAdjustTrack.PitchAdjust;
            }

            return 1;
        }

        /// <summary>
        /// Adjust offset
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <param name="value"></param>
        public static void AdjustlyricsOffset(this IAmKaraokeField karaokeField, double value)
        {
            //TODO : maybe use offset ?
            //1. adjust config.GetBindable<double>(OsuSetting.AudioOffset); ,but will change the offset to another modes,
            //2. get offsetClock from player
            Offset = value;
        }

        /// <summary>
        /// first Object's time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static double FirstObjectTime(this IAmKaraokeField karaokeField)
        {
            //RulesetContainer.Objects;
            //Refrenca : SongProgress.cs
            return karaokeField.WorkingBeatmap.Beatmap.HitObjects.First().StartTime;
        }

        /// <summary>
        /// Last object's time
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static double LastObjectTime(this IAmKaraokeField karaokeField)
        {
            var hitObjects = karaokeField.GetListHitObjects();
            return ((hitObjects.Last() as IHasEndTime)?.EndTime ?? hitObjects.Last().StartTime) + 1;
        }

        /// <summary>
        /// total time of the song
        /// calculate from first hitObject to last
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static double TotalTime(this IAmKaraokeField karaokeField)
        {
            return karaokeField.LastObjectTime() - karaokeField.FirstObjectTime();
        }

        /// <summary>
        /// use to get the current time
        /// </summary>
        /// <returns></returns>
        public static double GetCurrentTime(this IAmKaraokeField karaokeField)
        {
            return karaokeField.WorkingBeatmap.Track.CurrentTime;
        }

        /// <summary>
        /// FindObjectByCurrentTime
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static HitObject FindObjectByCurrentTime(this IAmKaraokeField karaokeField)
        {
            double currentTime = karaokeField.GetCurrentTime();
            var listObjects = karaokeField.GetListHitObjects();

            for (int i = 0; i < listObjects.Count; i++)
            {
                if (listObjects[i].StartTime >= currentTime + PrepareTime)
                {
                    if (i == 0)
                    {
                        return null;
                    }

                    return listObjects[i - 1];
                }
            }

            return null;
        }

        /// <summary>
        /// FindObjectIndexByCurrentTime
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static int FindObjectIndexByCurrentTime(this IAmKaraokeField karaokeField)
        {
            HitObject hitObject = karaokeField.FindObjectByCurrentTime();
            if (hitObject == null)
                return -1;

            var listObjects = karaokeField.GetListHitObjects();
            for (int i = 0; i < listObjects.Count; i++)
            {
                if (listObjects[i] == hitObject)
                    return i;
            }

            //404
            return -1;
        }


        /// <summary>
        /// if this beatmap need translate
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static bool NeedTranslate(this IAmKaraokeField karaokeField)
        {
            if (karaokeField.WorkingBeatmap.Mods.Value.OfType<KaraokeModOpenTranslate>().Any())
            {
                return true;
            }

            if (karaokeField.WorkingBeatmap.Mods.Value.OfType<KaraokeModCloseTranslate>().Any())
            {
                return false;
            }

            //TODO : get karaoke setting
            return true;
        }

        public static bool EnableHotKey(this IAmKaraokeField karaokeField)
        {
            return true;
        }

        public static bool ShowPanelAtBeginning(this IAmKaraokeField karaokeField)
        {
            return false;
        }

        /// <summary>
        /// get list HitObjects
        /// </summary>
        /// <param name="karaokeField"></param>
        /// <returns></returns>
        public static List<HitObject> GetListHitObjects(this IAmKaraokeField karaokeField)
        {
            return karaokeField.WorkingBeatmap.Beatmap.HitObjects.ToList();
        }
    }
}
