using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            for (int i = 0; i < wordsCount; i++) 
            { 
                var phrase = phraseBeginning.Split(' ');
                if (phrase.Length != 1) 
                {
                    string rangeOfWords = phrase[phrase.Length - 2] + " " + phrase[phrase.Length - 1];
                    if (nextWords.ContainsKey(rangeOfWords)) phraseBeginning += " " + nextWords[rangeOfWords];
                    else if (nextWords.ContainsKey(phrase[phrase.Length - 1])) phraseBeginning += " " + nextWords[phrase[phrase.Length - 1]];
                }
                else if (nextWords.ContainsKey(phrase[phrase.Length - 1])) phraseBeginning += " " + nextWords[phrase[phrase.Length - 1]];
            }
            return phraseBeginning;
        }
    }
}