using System;
using System.IO;

public class Lab8T3
{
    public void Run()
    {
        string inputPath = "input3.txt";
        string outputPath = "output3.txt";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Файл input3.txt не знайдено.");
            return;
        }

        string text = File.ReadAllText(inputPath);
        string[] words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.None);

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            string trimmed = word.TrimEnd('.', ',', ';', ':', '!', '?');

            if (trimmed.Length > 1)
            {
                char lastChar = trimmed[^1];
                string modified = "";

                for (int j = 0; j < trimmed.Length - 1; j++)
                {
                    if (trimmed[j] != lastChar)
                        modified += trimmed[j];
                }

                modified += lastChar;

                string punctuation = word.Length > trimmed.Length ? word[^1].ToString() : "";
                words[i] = modified + punctuation;
            }
        }

        string result = string.Join(" ", words);
        File.WriteAllText(outputPath, result);
        Console.WriteLine("Готово! Файл збережено у output3.txt");
    }
}
