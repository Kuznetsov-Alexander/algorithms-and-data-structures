using System;

namespace PolymorphismExample
{
    class Exam : Assessment, IGradable, ISchedulable, IPrintable
    {
        public Exam(string name, int duration) : base(name, duration)
        {
        }

        public override void Conduct()
        {
            Console.WriteLine($"Проводится экзамен: {Name}, длительность {Duration} мин.");
        }

        public void Grade()
        {
            Console.WriteLine("Экзамен оценивается по 5-балльной шкале.");
        }

        public void Schedule(DateTime date)
        {
            Console.WriteLine($"Экзамен назначен на {date.ToShortDateString()}.");
        }

        public void PrintResult()
        {
            Console.WriteLine("Результат экзамена занесён в ведомость.");
        }
    }
}