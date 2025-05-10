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
        Console.WriteLine("3 - Вилучити попередні входження останньої літери в кожному слові");
        Console.WriteLine("4 - Записати у двійковий файл числа з інтервалу");
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
                Task3_RemovePrevOccurencesOfLastLetter();
                break;
            case "4":
                Task4_BinaryWriteAndRead();
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

    static void Task3_RemovePrevOccurencesOfLastLetter()
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

            // Визначаємо, де закінчується слово (без пунктуації)
            string trimmed = word.TrimEnd(new[] { '.', ',', ';', ':', '!', '?' });

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

                // Додаємо назад розділовий знак, якщо був
                string punctuation = word.Length > trimmed.Length ? word[^1].ToString() : "";
                words[i] = modified + punctuation;
            }
        }

        string result = string.Join(" ", words);
        File.WriteAllText(outputPath, result);
        Console.WriteLine("Готово! Файл збережено у output3.txt");
    }

    static void Task4_BinaryWriteAndRead()
    {
        string filePath = "output4.bin";

        Console.Write("Скільки чисел у послідовності? ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Некоректне число.");
            return;
        }

        int[] numbers = new int[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Введіть число #{i + 1}: ");
            while (!int.TryParse(Console.ReadLine(), out numbers[i]))
            {
                Console.Write("Некоректне значення. Введіть ще раз: ");
            }
        }

        Console.Write("Введіть нижню межу інтервалу (a): ");
        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Некоректне число.");
            return;
        }

        Console.Write("Введіть верхню межу інтервалу (b): ");
        if (!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Некоректне число.");
            return;
        }

        // Записуємо числа в файл
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            foreach (int number in numbers)
            {
                if (number >= a && number <= b)
                {
                    writer.Write(number);
                }
            }
        }

        // Виводимо результат з файлу на консоль
        Console.WriteLine($"\nЧисла в інтервалі [{a}, {b}] з файлу {filePath}:");
        if (File.Exists(filePath))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                if (reader.BaseStream.Length == 0)
                {
                    Console.WriteLine("У файлі немає чисел.");
                }
                else
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        int num = reader.ReadInt32();
                        Console.Write($"{num} ");
                    }
                    Console.WriteLine();
                }
            }
        }
        else
        {
            Console.WriteLine("Файл не знайдений.");
        }
    }


}
