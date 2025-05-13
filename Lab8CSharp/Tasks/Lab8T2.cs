using System;
using System.IO;
using System.Text.RegularExpressions;

public class Lab8T2
{
    public void Run()
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
}
