// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Game.Rulesets.Karaoke.Configuration;

namespace osu.Game.Rulesets.Karaoke.Tools.Translator
{
    /// <summary>
    /// translator base
    /// </summary>
    public abstract class TranslateorBase
    {
        /// <summary>
        /// notified translate multi string 
        /// </summary>
        public EventHandler<List<string>> OnTranslateMultiStringSuccess { get; set; }

        /// <summary>
        /// notified translate single string 
        /// </summary>
        public EventHandler<string> OnTranslateSuccess { get; set; }

        /// <summary>
        /// if fail ,get error message
        /// </summary>
        public EventHandler<string> OnTranslateFail { get; set; }

        /// <summary>
        /// translate multi string at the same thme
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="targetLangCode"></param>
        /// <param name="translateString"></param>
        public abstract void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, List<string> translateString);

        /// <summary>
        /// translate multi string at the same thme
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="targetLangCode"></param>
        /// <param name="translateString"></param>
        public abstract void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, string translateString);
    }

    /// <summary>
    /// Translator Type
    /// </summary>
    public enum TranslatorType
    {
        google,
    }
}
