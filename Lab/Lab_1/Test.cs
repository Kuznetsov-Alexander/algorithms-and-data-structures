using System;

namespace PolymorphismExample
{
    class Test : Assessment, IGradable, ISchedulable, IPrintable
    {
        public Test(string name, int duration) : base(name, duration)
        {
        }

        public override void Conduct()
        {
            Console.WriteLine($"Проводится тест: {Name}, время {Duration} мин.");
        }

        public void Grade()
        {
            Console.WriteLine("Тест оценивается по количеству правильных ответов.");
        }

        public void Schedule(DateTime date)
        {
            Console.WriteLine($"Тест назначен на {date.ToShortDateString()}.");
        }

        public void PrintResult()
        {
            Console.WriteLine("Результат теста распечатан.");
        }
    }
}