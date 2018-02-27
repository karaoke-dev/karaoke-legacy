using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Game.Rulesets.Karaoke.Configuration;
using osu.Game.Rulesets.Karaoke.Objects;

namespace osu.Game.Rulesets.Karaoke.Tools.Romaji.karaoke
{
    /// <summary>
    /// this translate server is provided by : 
    /// https://romaji-translator.herokuapp.com/
    /// </summary>
    public class KaraokeRomajiTranslator
    {
        /// <summary>
        /// translte list 
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<Lyric> Translate(Lyric translateListString)
        {
            
            return (await TranslatePart(new List<Lyric>() { translateListString })).FirstOrDefault();
        }


        /// <summary>
        /// translte list 
        /// </summary>
        /// <param name="sourceLangeCode"></param>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        public async Task<List<Lyric>> Translate(List<Lyric> translateListString)
        {
            foreach (var single in translateListString)
            {
                
            }
            return translateListString;
        }

        /// <summary>
        /// translate it part
        /// </summary>
        /// <param name="translateListString"></param>
        /// <returns></returns>
        protected async Task<List<Lyric>> TranslatePart(List<Lyric> translateListString)
        {
            return null;
        }
    }
    /// <summary>
    /// translate result
    /// </summary>
    public class TranslateResult
    {

    }
}
