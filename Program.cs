using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnitLite;

namespace TextAnalysis
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //Console.WriteLine(FrequencyAnalysisTask.GetMostFrequentNextWords(SentencesParserTask.ParseSentences("a b c d. b c d. e b c a d.")));
            //// Запуск автоматических тестов. Ниже список тестовых наборов, который нужно запустить.
            //// Закомментируйте тесты на те задачи, к которым ещё не приступали, чтобы они не мешались в консоли.
            //// Все непрошедшие тесты 
            var testsToRun = new string[]
            {
                //"TextAnalysis.SentencesParser_Tests",
                //"TextAnalysis.FrequencyAnalysis_Tests",
                "TextAnalysis.TextGenerator_Tests",
            };
            new AutoRun().Execute(new[]
            {
                "--stoponerror", // Останавливать после первого же непрошедшего теста. Закомментируйте, чтобы увидеть все падающие тесты
                "--noresult",
                "--test=" + string.Join(",", testsToRun)
            });

            var text = File.ReadAllText("HarryPotterText.txt");
            var sentences = SentencesParserTask.ParseSentences(text);
            var frequency = FrequencyAnalysisTask.GetMostFrequentNextWords(sentences);
            timer.Stop();
            ////Расскомментируйте этот блок, если хотите выполнить последнюю задачу до первых двух.
            ///*
            frequency = new Dictionary<string, string>
            {
                {"harry", "potter"},
                {"potter", "boy" },
                {"boy", "who" },
                {"who", "likes" },
                {"boy who", "survived" },
                {"survived", "attack" },
                {"he", "likes" },
                {"likes", "harry" },
                {"ron", "likes" },
                {"wizard", "harry" },
            };
            while (true)
            {
                Console.Write("Введите первое слово (например, harry): ");
                var beginning = Console.ReadLine();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGeneratorTask.ContinuePhrase(frequency, beginning.ToLower(), 10);
                Console.WriteLine(phrase);
            }
        }
    }
}