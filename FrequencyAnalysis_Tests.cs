using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    [TestFixture]
    public class FrequencyAnalysis_Tests
    {


        [Test]
        [Order(03)]
        public void ReturnCorrectResult_OnTextWithOneSentenceWithMultipleWords()
        {
            var text = "x y z";
            var parsedText = ParseText(text);
            var expected = new Dictionary<string, string>
            {
                {"x", "y"},
                {"y", "z"},
                {"x y", "z"}
            };

            var actual = FrequencyAnalysisTask.GetMostFrequentNextWords(parsedText);

            AssertResult(expected, actual, text);
        }

        [Test]
        [Order(04)]
        public void ReturnCorrectResult_OnTextWithTwoSentencesWithOneWord()
        {
            var text = "x.y";
            var parsedText = ParseText(text);
            var expected = new Dictionary<string, string>();

            var actual = FrequencyAnalysisTask.GetMostFrequentNextWords(parsedText);

            AssertResult(expected, actual, text);
        }

        [Test]
        [Order(05)]
        public void ReturnResult_WithMostFrequentBigrams([Values("x y. x z. x y.", "x z. x y. x y", "x y. x y.", "x y")]
            string text)
        {
            var parsedText = ParseText(text);
            var expected = new Dictionary<string, string>
            {
                {"x", "y"}
            };

            var actual = FrequencyAnalysisTask.GetMostFrequentNextWords(parsedText);

            AssertResult(expected, actual, text);
        }

        [Test]
        [Order(06)]
        public void ReturnResult_WithLexicographicallyFirstBigram_IfBigramsHaveSameFrequency(
            [Values("x y. x z.", "x z. x y.", "x y. x yy.", "x yy. x y")]
            string text)
        {
            var parsedText = ParseText(text);
            var expected = new Dictionary<string, string>
            {
                {"x", "y"}
            };

            var actual = FrequencyAnalysisTask.GetMostFrequentNextWords(parsedText);

            AssertResult(expected, actual, text);
        }

        [Test]
        [Order(50)]
        public void IgnoreSentencesWithSingleWord([Values("x. ax. y. z")] string text)
        {
            var parsedText = ParseText(text);
            var expected = new Dictionary<string, string>();

            var actual = FrequencyAnalysisTask.GetMostFrequentNextWords(parsedText);

            AssertResult(expected, actual, text);
        }

        [Test]
        [Order(60)]
        public void ReturnPairForEveryBigram([Values("x y. y z.", "y z. x y.")] string text)
        {
            var parsedText = ParseText(text);
            var expected = new Dictionary<string, string>
            {
                {"x", "y"},
                {"y", "z"}
            };

            var actual = FrequencyAnalysisTask.GetMostFrequentNextWords(parsedText);

            AssertResult(expected, actual, text);
        }

        // Упрощённый парсинг текста
        public List<List<string>> ParseText(string text)
        {
            return text.Split('.')
                .Select(sentence => sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList())
                .ToList();
        }

        public static void AssertResult(
            Dictionary<string, string> expected,
            Dictionary<string, string> actual,
            string text)
        {
            foreach (var key in expected.Keys)
            {
                 if (!actual.ContainsKey(key))
                     Assert.Fail($"input text: [{text}]\nmissing expected key [{key}] in dictionary");
                 Assert.AreEqual(expected[key], actual[key], $"input text: [{text}]\nwrong value for key [{key}]");
            } 

            foreach (var key in actual.Keys)
                if (!expected.ContainsKey(key))
                    Assert.Fail($"input text: [{text}]\nkey [{key}] should not be in dictionary");
        }

    }
}