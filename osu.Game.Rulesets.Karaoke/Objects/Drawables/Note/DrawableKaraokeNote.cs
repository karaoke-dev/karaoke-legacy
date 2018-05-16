// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Objects.Drawables;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables.Note
{
    /// <summary>
    /// just copy DrawableManiaNote.cs
    /// and implement 
    /// 1. TextSets
    /// 2. singer
    /// </summary>
    public class DrawableKaraokeNote : DrawableHitObject<BaseLyric>
    {
        public LyricTimeLine TimeLine { get; }

        public DrawableKaraokeNote(BaseLyric lyric, LyricTimeLine timeLine) : base(lyric)
        {
            TimeLine = timeLine;
            //TODO : implement it until mania editor complete
        }

        protected override void UpdateState(ArmedState state)
        {
            throw new System.NotImplementedException();
        }
    }
}
