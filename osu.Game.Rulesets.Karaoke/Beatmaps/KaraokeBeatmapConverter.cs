// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    internal class KaraokeBeatmapConverter : BeatmapConverter<Lyric>
    {
        protected override IEnumerable<Type> ValidConversionTypes { get; } = new[] { typeof(IHasPosition) };


        protected override IEnumerable<Lyric> ConvertHitObject(HitObject original, Beatmap beatmap)
        {
            var curveData = original as IHasCurve;
            var endTimeData = original as IHasEndTime;
            var positionData = original as IHasPosition;
            var comboData = original as IHasCombo;


            yield return (Lyric)original;

            /*
            if (curveData != null)
            {
                yield return new Slider
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    ControlPoints = curveData.ControlPoints,
                    CurveType = curveData.CurveType,
                    Distance = curveData.Distance,
                    RepeatSamples = curveData.RepeatSamples,
                    RepeatCount = curveData.RepeatCount,
                    Position = positionData?.Position ?? Vector2.Zero,
                    NewCombo = comboData?.NewCombo ?? false
                };
            }
            else if (endTimeData != null)
            {
                yield return new Spinner
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    EndTime = endTimeData.EndTime,

                    Position = positionData?.Position ?? KaraokePlayfield.BASE_SIZE / 2,
                };
            }
            else
            {
                yield return new HitCircle
                {
                    StartTime = original.StartTime,
                    Samples = original.Samples,
                    Position = positionData?.Position ?? Vector2.Zero,
                    NewCombo = comboData?.NewCombo ?? false
                };
            }
            
            */
        }


        /// <summary>
        /// Performs the conversion of a Beatmap using this Beatmap Converter.
        /// </summary>
        /// <param name="original">The un-converted Beatmap.</param>
        /// <returns>The converted Beatmap.</returns>
        protected override Beatmap<Lyric> ConvertBeatmap(Beatmap original)
        {
            //TODO : ・ｽﾒ考Mania・ｽ・ｽ・ｽ・ｽ・ｽ・ｽ・ｽ
            var newBratmaps = new Beatmap<Lyric>()
            {
                BeatmapInfo = original.BeatmapInfo,
                ControlPointInfo = original.ControlPointInfo,
                HitObjects = Convert(original.HitObjects),
            };
            //newBratmaps.HitObjects.BindingAll();
            return newBratmaps;
        }

        protected List<Lyric> Convert(List<HitObject> originalHitOjects)
        {
            List<Lyric> listRerturn = new List<Lyric>();

            for (int i = 0; i < originalHitOjects.Count; i++)
            {
                if (i % 5 == 4)
                {
                    double duration = originalHitOjects[i].StartTime - originalHitOjects[i - 4].StartTime;
                    var karaokeObject = DemoKaraokeObject.GenerateWithStartAndDuration(originalHitOjects[i].StartTime, duration);
                    listRerturn.Add(karaokeObject);
                }
            }

            return listRerturn;
        }
    }
}
