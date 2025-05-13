using System;
using System.IO;

public class Lab8T4
{
    public void Run()
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

        Console.WriteLine($"\nЧисла в інтервалі [{a}, {b}] з файлу {filePath}:");
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
}
