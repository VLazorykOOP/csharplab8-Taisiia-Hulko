using System;
using System.IO;
using System.Text.RegularExpressions;

public class Lab8T1
{
    public void Run()
    {
        // Шлях до файлів input.txt і output.txt у поточній директорії (де зберігається exe)
        string inputPath = "input.txt";  // Відносний шлях до файлу в поточній директорії
        string outputPath = "output.txt";  // Відносний шлях до файлу в поточній директорії

        Console.WriteLine($"Шлях до input.txt: {Path.GetFullPath(inputPath)}");
        Console.WriteLine($"Шлях до output.txt: {Path.GetFullPath(outputPath)}");

        // Перевірка, чи існує файл input.txt
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Файл input.txt не знайдено.");
            return;
        }

        // Читаємо вміст файлу input.txt
        string text = File.ReadAllText(inputPath);

        // Патерн для знаходження адрес з доменом .edu.ua
        string pattern = @"\b(?:https?:\/\/)?(?:www\.)?\w+\.edu\.ua(?:\/\S*)?\b";
        MatchCollection matches = Regex.Matches(text, pattern);

        // Виведення знайдених адрес
        Console.WriteLine($"Знайдено {matches.Count} адрес(и) з доменом .edu.ua:");
        foreach (Match match in matches)
        {
            Console.WriteLine($"- {match.Value}");
        }

        // Запит на введення заміни для знайдених адрес
        Console.Write("\nВведіть значення для заміни знайдених адрес: ");
        string replacement = Console.ReadLine();

        // Замінюємо знайдені адреси на введене значення
        string replacedText = Regex.Replace(text, pattern, replacement);

        // Записуємо результат у output.txt
        File.WriteAllText(outputPath, replacedText);
        Console.WriteLine($"\nГотово! Збережено у файл: {outputPath}");
    }
}
