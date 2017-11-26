// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Karaoke.Objects;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.Karaoke.Replays
{
    public class KaraokeAutoGenerator : KaraokeAutoGeneratorBase
    {
        #region Parameters

        /// <summary>
        /// If delayed movements should be used, causing the cursor to stay on each hitobject for as long as possible.
        /// Mainly for Autopilot.
        /// </summary>
        public bool DelayedMovements; // ModManager.CheckActive(Mods.Relax2);

        #endregion

        #region Constants

        /// <summary>
        /// The "reaction time" in ms between "seeing" a new hit object and moving to "react" to it.
        /// </summary>
        private readonly double reactionTime;

        /// <summary>
        /// What easing to use when moving between hitobjects
        /// </summary>
        private Easing preferredEasing => DelayedMovements ? Easing.InOutCubic : Easing.Out;

        #endregion

        #region Construction / Initialisation

        public KaraokeAutoGenerator(Beatmap<KaraokeObject> beatmap)
            : base(beatmap)
        {
            // Already superhuman, but still somewhat realistic
            reactionTime = ApplyModsToRate(100);
        }

        #endregion

        #region Generator

        /// <summary>
        /// Which button (left or right) to use for the current hitobject.
        /// Even means LMB will be used to click, odd means RMB will be used.
        /// This keeps track of the button previously used for alt/singletap logic.
        /// </summary>
        private int buttonIndex;

        public override Replay Generate()
        {
            buttonIndex = 0;

            AddFrameToReplay(new ReplayFrame(-100000, 256, 500, ReplayButtonState.None));
            AddFrameToReplay(new ReplayFrame(Beatmap.HitObjects[0].StartTime - 1500, 256, 500, ReplayButtonState.None));
            AddFrameToReplay(new ReplayFrame(Beatmap.HitObjects[0].StartTime - 1000, 256, 192, ReplayButtonState.None));

            //Karaoke 根本沒三洨reply阿wwww

            return Replay;
        }


        #endregion
    }
}
