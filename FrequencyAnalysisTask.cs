using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
   static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var statistics = new Dictionary<string, Dictionary<string, int>>(GetFrequencyStats(text));
            foreach (var firstWord in statistics.Keys)
                result.Add(firstWord,GetMostFrequentWord(statistics[firstWord]));
            return result;
        }

        static Dictionary<string, Dictionary<string, int>> GetFrequencyStats(List<List<string>> text)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
                if (sentence.Count > 1)
                    for (int i = 0; i < sentence.Count; i++)
                    for (int j = 1; j < (sentence.Count - i < 2 ? 0 : 3); j++)
                    {
                        string rangeOfWords = string.Join(" ", sentence.GetRange(i, j));
                        if (i != sentence.Count - j && !result.ContainsKey(rangeOfWords)) 
                            result[rangeOfWords] = new Dictionary<string, int> { { sentence[i + j], 1 } };
                        else if (i != sentence.Count - j)
                            if (!result[rangeOfWords].ContainsKey(sentence[i + j]))
                                result[rangeOfWords].Add(sentence[i + j], 1);
                            else result[rangeOfWords][sentence[i + j]] += 1;
                    } 
            return result;
        }

        static int GetMostFrequentCount(Dictionary<string, int> stat)
        {
            int mostFrequentCount = 0;
            foreach (var count in stat.Values)
                if (count > mostFrequentCount) mostFrequentCount = count;
            return mostFrequentCount;
        }

        static string GetLowestOrdinalWord(IEnumerable<string> stat)
        {
            string lowestOrdinalWord = "◌󠇯◌󠇯◌󠇯◌󠇯";
            foreach (var lastWord in stat)
                if (string.CompareOrdinal(lastWord,lowestOrdinalWord) < 0)
                    lowestOrdinalWord = lastWord;
            return lowestOrdinalWord;
        }

        static string GetMostFrequentWord(Dictionary<string, int> stat)
        {
            int i = 0;
            var lastWord = from w in stat where w.Value == GetMostFrequentCount(stat) select w.Key;
            foreach (var count in stat.Values)
                if (count == GetMostFrequentCount(stat)) i++;
            if (i != 1)
            {
                var lastOrdinalWord = from w in stat where w.Key == GetLowestOrdinalWord(lastWord) select w.Key;
                return string.Join("", lastOrdinalWord);
            }
            return string.Join("", lastWord);
        }
    }
}