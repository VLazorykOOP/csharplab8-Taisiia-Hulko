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
        Console.WriteLine("5 - Операції з директоріями та файлами (Гулько)");
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
            case "5":
                Task5_FileOperations();
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

    static void Task5_FileOperations()
    {
        string basePath = @"D:\temp";
        string path1 = Path.Combine(basePath, "Гулько1");
        string path2 = Path.Combine(basePath, "Гулько2");

        Directory.CreateDirectory(path1);
        Directory.CreateDirectory(path2);

        string t1Path = Path.Combine(path1, "t1.txt");
        string t2Path = Path.Combine(path1, "t2.txt");
        File.WriteAllText(t1Path, "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми");
        File.WriteAllText(t2Path, "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ");

        string t3Path = Path.Combine(path2, "t3.txt");
        File.WriteAllText(t3Path, File.ReadAllText(t1Path) + Environment.NewLine + File.ReadAllText(t2Path));

        Console.WriteLine("Інформація про створені файли:");
        foreach (var file in new[] { t1Path, t2Path, t3Path })
        {
            FileInfo fi = new FileInfo(file);
            Console.WriteLine($"{fi.FullName} | Розмір: {fi.Length} байт | Створено: {fi.CreationTime}");
        }

        string movedT2 = Path.Combine(path2, "t2.txt");
        File.Move(t2Path, movedT2);

        string copiedT1 = Path.Combine(path2, "t1.txt");
        File.Copy(t1Path, copiedT1);

        string allPath = Path.Combine(basePath, "ALL");
        if (Directory.Exists(allPath)) Directory.Delete(allPath, true);
        Directory.Move(path2, allPath);
        Directory.Delete(path1, true);

        Console.WriteLine("\nФайли в папці ALL:");
        string[] finalFiles = Directory.GetFiles(allPath);
        foreach (string file in finalFiles)
        {
            FileInfo fi = new FileInfo(file);
            Console.WriteLine($"{fi.Name} | {fi.Length} байт | {fi.CreationTime}");
        }
    }
}
