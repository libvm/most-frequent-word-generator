using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            List<string> sentences = text.Split('.', '!', '?', ';', ':', '(', ')').ToList();
            foreach (var sentence in sentences)
            {
                List<string> modifiedSentence = new List<string>();
                string modifiedText = GetNewSentence(sentence).ToLower();
                if (modifiedText == "")
                    continue;
                modifiedSentence.AddRange(modifiedText.Split(' '));
                sentencesList.Add(modifiedSentence);
            }
            return sentencesList;
        }

        static string GetNewSentence(string sentence)
        {
            string newSentence = "";
            foreach (var symbol in sentence)
                if (char.IsLetter(symbol) || symbol == '\'') newSentence += symbol;
                else newSentence += ' ';
            newSentence = newSentence.Trim();
            while (newSentence.Contains("  "))
                newSentence = newSentence.Replace("  ", " ");
            return newSentence;
        }
    }
}