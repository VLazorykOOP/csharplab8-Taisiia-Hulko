using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання:");
        Console.WriteLine("1 - Знайти та замінити .edu.ua адреси");
        Console.WriteLine("2 - Видалити українські слова, що починаються на голосну");
        Console.WriteLine("3 - Видалити всі попередні входження останньої літери у словах");
        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Task1_EduUaReplacement();
                break;
            case "2":
                Task2_RemoveVowelWords();
                break;
            case "3":
                Task3_RemovePreviousOccurrences();
                break;
            default:
                Console.WriteLine("Невірний вибір. Спробуйте знову.");
                break;
        }
    }

    static void Task1_EduUaReplacement()
    {
        string inputPath = "input.txt";
        string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Файл input.txt не знайдено.");
            return;
        }

        string text = File.ReadAllText(inputPath);

        string pattern = @"\b(?:https?:\/\/)?(?:www\.)?\w+\.edu\.ua(?:\/\S*)?\b";
        MatchCollection matches = Regex.Matches(text, pattern);

        Console.WriteLine($"Знайдено {matches.Count} адрес(и) з доменом .edu.ua:");

        foreach (Match match in matches)
        {
            Console.WriteLine($"- {match.Value}");
        }

        Console.Write("\nВведіть значення для заміни знайдених адрес: ");
        string replacement = Console.ReadLine();

        string replacedText = Regex.Replace(text, pattern, replacement);

        File.WriteAllText(outputPath, replacedText);

        Console.WriteLine($"\nГотово! Збережено у файл: {outputPath}");
    }

    static void Task2_RemoveVowelWords()
    {
        string inputPath = "input2.txt";
        string outputPath = "output2.txt";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Файл input2.txt не знайдено.");
            return;
        }

        string text = File.ReadAllText(inputPath);

        string pattern = @"\s?\b[АЕЄИІЇОУЮЯаеєиіїоуюя][\w’-]*";
        string cleanedText = Regex.Replace(text, pattern, "");

        cleanedText = Regex.Replace(cleanedText, @"\s{2,}", " ").Trim();
        cleanedText = Regex.Replace(cleanedText, @"\s([.,!?;:])", "$1");

        File.WriteAllText(outputPath, cleanedText);

        Console.WriteLine("Готово! Файл збережено у output2.txt");
    }

    static void Task3_RemovePreviousOccurrences()
    {
        string inputPath = "input3.txt";
        string outputPath = "output3.txt";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Файл input3.txt не знайдено.");
            return;
        }

        string text = File.ReadAllText(inputPath);

        string pattern = @"\b\w+\b";
        string result = Regex.Replace(text, pattern, RemoveRepeats);

        File.WriteAllText(outputPath, result);

        Console.WriteLine("Готово! Результат збережено у output3.txt");
    }

    static string RemoveRepeats(Match match)
    {
        string word = match.Value;
        if (word.Length < 2) return word;

        char last = word[^1];
        string cleaned = "";

        for (int i = 0; i < word.Length - 1; i++)
        {
            if (word[i] != last)
                cleaned += word[i];
        }

        cleaned += last;
        return cleaned;
    }
}
