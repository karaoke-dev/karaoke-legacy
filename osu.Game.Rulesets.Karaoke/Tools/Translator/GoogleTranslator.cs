// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace osu.Game.Rulesets.Karaoke.Tools.Translator
{
    public class GoogleTranslator : TranslateorBase
    {
        //我勸你不要亂幹人家的API Key喔
        protected string ApiKey = "AIzaSyB9tomdvp8WmySkEWIhjhVYO3rkhzKOPMc";

        protected override int MaxThanslateSentenceAtTime => 70; //max is 73

        private Dictionary<TranslateCode, string> _langCode;

        public override Dictionary<TranslateCode, string> LangToCodeDictionary
        {
            get
            {
                if (_langCode == null)
                    _langCode = LangTagConvertor.ListTranslateCode;
                return _langCode;
            }
        }

        /// <summary>
        /// Translate : 
        /// https://www.aspsnippets.com/Articles/Using-Google-Translation-Translate-API-in-ASPNet-using-C-and-VBNet.aspx
        /// converto to unicode : 
        /// https://msdn.microsoft.com/zh-tw/library/kdcak6ye(v=vs.110).aspx
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="targetLangCode"></param>
        /// <param name="translateString"></param>
        public override void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, string translateString)
        {
            try
            {
                string url = "https://translation.googleapis.com/language/translate/";
                url += "v2?key=" + ApiKey;
                url += "&source=" + LangToCodeDictionary[sourceLangeCode];
                url += "&target=" + LangToCodeDictionary[targetLangCode];
                url += "&q=" + translateString;
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string json = client.DownloadString(url);
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
                string returnTranslateResult = rootObject?.Data?.Translations?.FirstOrDefault().TranslatedText;
                OnTranslateSuccess?.Invoke(this, returnTranslateResult);
            }
            catch (Exception e)
            {
                OnTranslateFail?.Invoke(this, e.Message);
            }
        }

        /// <summary>
        /// translate multi string
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="targetLangCode"></param>
        /// <param name="translateListString"></param>
        public override void Translate(TranslateCode sourceLangeCode, TranslateCode targetLangCode, List<string> translateListString)
        {
            try
            {
                List<string> returnString = new List<string>();

                int translateTime = translateListString.Count / MaxThanslateSentenceAtTime + 1;
                for (int i = 0; i < translateTime; i++)
                {
                    int startIndex = i * MaxThanslateSentenceAtTime;
                    int count = (i + 1) * MaxThanslateSentenceAtTime >= translateListString.Count ? translateListString.Count - startIndex : MaxThanslateSentenceAtTime;
                    var partialTranslateString = translateListString.GetRange(startIndex, count);

                    string url = "https://translation.googleapis.com/language/translate/";
                    url += "v2?key=" + ApiKey;
                    url += "&source=" + LangToCodeDictionary[sourceLangeCode];
                    url += "&target=" + LangToCodeDictionary[targetLangCode];

                    foreach (var singleString in partialTranslateString)
                    {
                        url += "&q=" + singleString;
                    }

                    WebClient client = new WebClient();
                    client.Encoding = Encoding.UTF8;
                    string json = client.DownloadString(url);
                    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
                    List<Data.Translation> listTranslate = rootObject?.Data?.Translations;

                    foreach (var single in listTranslate)
                    {
                        returnString.Add(single.TranslatedText);
                    }
                }

                OnTranslateMultiStringSuccess?.Invoke(this, returnString);
            }
            catch (Exception e)
            {
                OnTranslateFail?.Invoke(this, e.Message);
            }
        }

        public class RootObject
        {
            public Data Data { get; set; }
        }

        public class Data
        {
            public List<Translation> Translations { get; set; }

            public class Translation
            {
                public string TranslatedText { get; set; }
                public string DetectedSourceLanguage { get; set; }
            }
        }
    }
}
