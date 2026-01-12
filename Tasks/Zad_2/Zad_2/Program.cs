namespace _2;
class Program
{
    public static void Main()
    {
        ComplexNum secondNum = new ComplexNum();
        ComplexNum firstNum = new ComplexNum();

        Console.WriteLine("""
        -----------------
        1.Задать число 1
        2.Задать число 2
        3.Сумма
        4.Разность
        5.Произведение
        6.Деление
        7.Модуль первого числа
        8.Аргумент первого числа
        9.Вывести оба числа
        q, Q - Выход
        -----------------
        """);

        while (true)
        {
            Console.WriteLine("\nv");

            string command = Console.ReadLine();
            switch (command) {
                case "q":
                case "Q":
                    Console.WriteLine("Exiting...");
                    return;
                case "1":
                    Console.WriteLine("Введите вещ. часть 1-го числа -> ");
                    firstNum.Real = double.Parse(Console.ReadLine());
                    Console.WriteLine("Введите мнимую часть 1-го числа -> ");
                    firstNum.Imaginary = double.Parse(Console.ReadLine());
                    break;
                case "2":
                    Console.WriteLine("Введите вещ. часть 2-го числа -> ");
                    secondNum.Real = double.Parse(Console.ReadLine());
                    Console.WriteLine("Введите мнимую часть 2-го числа -> ");
                    secondNum.Imaginary = double.Parse(Console.ReadLine());
                    break;
                case "3":
                    Console.WriteLine(firstNum + secondNum); break; 
                case "4":
                    Console.WriteLine(firstNum - secondNum); break;
                case "5":
                    Console.WriteLine(firstNum * secondNum); break;
                case "6":
                    Console.WriteLine(firstNum / secondNum); break;
                case "7":
                    Console.WriteLine(firstNum.Module()); break; 
                case "8":
                    Console.WriteLine(firstNum.Argument()); break;
                case "9":
                    Console.WriteLine(firstNum);
                    Console.WriteLine(secondNum); break;
                default:
                    Console.WriteLine("неизвестная команда"); break; 
            }   
        }
    }
}