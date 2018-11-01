// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Helps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Karaoke.Beatmaps
{
    internal class KaraokeBeatmapConverter : BeatmapConverter<Lyric>
    {
        public KaraokeBeatmapConverter(IBeatmap beatmap)
            : base(beatmap)
        {
        }

        protected override IEnumerable<Type> ValidConversionTypes { get; } = new[] { typeof(IHasPosition) };

        protected override IEnumerable<Lyric> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            yield return (Lyric)original;
        }

        protected override Beatmap<Lyric> CreateBeatmap()
        {
            return new KaraokeBeatmap();
        }

        /// <summary>
        ///     Performs the conversion of a Beatmap using this Beatmap Converter.
        /// </summary>
        /// <param name="original">The un-converted Beatmap.</param>
        /// <returns>The converted Beatmap.</returns>
        protected override Beatmap<Lyric> ConvertBeatmap(IBeatmap original)
        {
            var newBratmaps = new Beatmap<Lyric>
            {
                BeatmapInfo = original.BeatmapInfo,
                ControlPointInfo = original.ControlPointInfo,
                HitObjects = Convert(original.HitObjects.ToList()),
            };

            //slow down the note speed
            newBratmaps.BeatmapInfo.BaseDifficulty.OverallDifficulty = newBratmaps.BeatmapInfo.BaseDifficulty.OverallDifficulty / 2;

            return newBratmaps;
        }

        protected List<Lyric> Convert(List<HitObject> originalHitOjects)
        {
            var listRerturn = new List<Lyric>();

            for (var i = 0; i < originalHitOjects.Count; i++)
                if (i % 5 == 4)
                {
                    var duration = originalHitOjects[i].StartTime - originalHitOjects[i - 4].StartTime;
                    var karaokeObject = DemoKaraokeObject.GenerateWithStartAndDuration(originalHitOjects[i].StartTime, duration);
                    listRerturn.Add(karaokeObject);
                }

            return listRerturn;
        }
    }
}
