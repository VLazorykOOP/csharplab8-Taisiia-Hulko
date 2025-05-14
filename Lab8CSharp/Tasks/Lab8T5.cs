using System;
using System.IO;

public class Lab8T5
{
    public void Run()
    {
        string basePath = @"D:\temp";
        string folder1 = Path.Combine(basePath, "Гулько1");
        string folder2 = Path.Combine(basePath, "Гулько2");

        // 1. Створення папок
        Directory.CreateDirectory(folder1);
        Directory.CreateDirectory(folder2);

        // 2. Створення файлів t1.txt і t2.txt
        string t1Path = Path.Combine(folder1, "t1.txt");
        string t2Path = Path.Combine(folder1, "t2.txt");

        File.WriteAllText(t1Path, "<Шевченко Степан Іванович, 2001> року народження, місце проживання <м. Суми>");
        File.WriteAllText(t2Path, "<Комар Сергій Федорович, 2000 > року народження, місце проживання <м. Київ>");

        // 3. Створення t3.txt в Гулько2 з вмістом t1 і t2
        string t3Path = Path.Combine(folder2, "t3.txt");
        File.WriteAllText(t3Path, File.ReadAllText(t1Path) + Environment.NewLine + File.ReadAllText(t2Path));

        // 4. Інформація про створені файли
        Console.WriteLine("Інформація про створені файли:");
        PrintFileInfo(t1Path);
        PrintFileInfo(t2Path);
        PrintFileInfo(t3Path);

        // 5. Переміщення t2.txt до Гулько2
        string movedT2 = Path.Combine(folder2, "t2.txt");
        File.Move(t2Path, movedT2);

        // 6. Копіювання t1.txt до Гулько2
        string copiedT1 = Path.Combine(folder2, "t1.txt");
        File.Copy(t1Path, copiedT1, true);

        // 7. Перейменування Гулько2 в ALL, видалення Гулько1
        string allPath = Path.Combine(basePath, "ALL");
        if (Directory.Exists(allPath)) Directory.Delete(allPath, true);
        Directory.Move(folder2, allPath);
        Directory.Delete(folder1, true);

        // 8. Інформація про файли в папці ALL
        Console.WriteLine("\nФайли в папці ALL:");
        foreach (string file in Directory.GetFiles(allPath))
        {
            PrintFileInfo(file);
        }

        Console.WriteLine("\nГотово!");
    }

    private void PrintFileInfo(string path)
    {
        FileInfo file = new FileInfo(path);
        Console.WriteLine($"{file.Name} | {file.FullName}");
        Console.WriteLine($"  Розмір: {file.Length} байт");
        Console.WriteLine($"  Створено: {file.CreationTime}");
        Console.WriteLine($"  Змінено: {file.LastWriteTime}");
        Console.WriteLine();
    }
}
