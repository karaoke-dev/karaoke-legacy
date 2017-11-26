
using System;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Karaoke.Objects.Drawables.Pieces;
using osu.Game.Rulesets.Karaoke.Tools.Translator;

namespace osu.Game.Rulesets.Karaoke.Objects.Drawables
{
    public interface IAmDrawableKaraokeObject
    {
        /// <summary>
        /// Object
        /// </summary>
        KaraokeObject KaraokeObject { get; }

        /// <summary>
        /// Template
        /// </summary>
        KaraokeTemplate Template { get; set; }

        /// <summary>
        /// singer
        /// </summary>
        KaraokeSinger Singer { get; set; }

        /// <summary>
        /// set preemptive time
        /// </summary>
        double PreemptiveTime { get; set; }

        /// <summary>
        /// show text and mask
        /// </summary>
        TextsAndMask TextsAndMaskPiece { get; set; }

        /// <summary>
        /// translate text
        /// </summary>
        KaraokeText TranslateText { get; set; }

        /// <summary>
        /// add translate
        /// </summary>
        /// <param name="code"></param>
        /// <param name="translateResult"></param>
        void AddTranslate(TranslateCode code, string translateResult);

       

        
    }
}
