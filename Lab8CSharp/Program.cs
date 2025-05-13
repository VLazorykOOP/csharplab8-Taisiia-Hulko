using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Оберіть завдання:");
        Console.WriteLine("1 - Пошук та заміна .edu.ua");
        Console.WriteLine("2 - Видалення слів на голосну");
        Console.WriteLine("3 - Видалення повторів останньої літери");
        Console.WriteLine("4 - Двійкові файли з числами");
        Console.WriteLine("5 - Операції з директоріями");

        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var lab8task1 = new Lab8T1();
                lab8task1.Run();
                break;
            case "2":
                var lab8task2 = new Lab8T2();
                lab8task2.Run();
                break;
            case "3":
                var lab8task3 = new Lab8T3();
                lab8task3.Run();
                break;
            case "4":
                var lab8task4 = new Lab8T4();
                lab8task4.Run();
                break;
            case "5":
                var lab8task5 = new Lab8T5();
                lab8task5.Run();
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}
