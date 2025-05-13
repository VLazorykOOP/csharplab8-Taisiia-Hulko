using System;
using System.IO;

public class Lab8T5
{
    public void Run()
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
