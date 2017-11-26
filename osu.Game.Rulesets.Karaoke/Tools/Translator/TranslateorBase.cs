using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Rulesets.Karaoke.Tools.Translator
{
    /// <summary>
    /// translator base
    /// </summary>
    public abstract class TranslateorBase
    {
        protected abstract int MaxThanslateSentenceAtTime { get; }

        protected abstract Dictionary<TranslateCode, string> LangToCodeDictionary { get; }

        //notified translate single string 
        public EventHandler<string> OnTranslateSuccess { get; set; }

        //notified translate multi string 
        public EventHandler<List<string>> OnTranslateMultiStringSuccess { get; set; }

        //if fail ,get error message
        public EventHandler<string> OnTranslateFail { get; set; }

        //translate single streing 
        public abstract void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, string translateString);

        //translate multi string at the same thme
        public abstract void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, List<string> translateString);
    }

    /// <summary>
    /// Translator Type
    /// </summary>
    public enum TranslatorType
    {
        google,
    }
}
