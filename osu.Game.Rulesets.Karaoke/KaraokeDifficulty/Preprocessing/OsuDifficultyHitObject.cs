// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.KaraokeDifficulty.Preprocessing
{
    /// <summary>
    /// A wrapper around <see cref="OsuHitObject"/> extending it with additional data required for difficulty calculation.
    /// </summary>
    public class OsuDifficultyHitObject
    {
        /// <summary>
        /// The <see cref="OsuHitObject"/> this <see cref="OsuDifficultyHitObject"/> refers to.
        /// </summary>
        public KaraokeObject BaseObject { get; }

        /// <summary>
        /// Normalized distance from the <see cref="OsuHitObject.StackedPosition"/> of the previous <see cref="OsuDifficultyHitObject"/>.
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// Milliseconds elapsed since the StartTime of the previous <see cref="OsuDifficultyHitObject"/>.
        /// </summary>
        public double DeltaTime { get; private set; }

        /// <summary>
        /// Number of milliseconds until the <see cref="OsuDifficultyHitObject"/> has to be hit.
        /// </summary>
        public double TimeUntilHit { get; set; }

        private const int normalized_radius = 52;

        private readonly KaraokeObject[] t;

        /// <summary>
        /// Initializes the object calculating extra data required for difficulty calculation.
        /// </summary>
        public OsuDifficultyHitObject(KaraokeObject[] triangle)
        {
            t = triangle;
            BaseObject = t[0];
            setDistances();
            setTimingValues();
            // Calculate angle here
        }

        private void setDistances()
        {
           
        }

        private void setTimingValues()
        {
            // Every timing inverval is hard capped at the equivalent of 375 BPM streaming speed as a safety measure.
            DeltaTime = Math.Max(40, t[0].StartTime - t[1].StartTime);
            TimeUntilHit = 450; // BaseObject.PreEmpt;
        }
    }
}
