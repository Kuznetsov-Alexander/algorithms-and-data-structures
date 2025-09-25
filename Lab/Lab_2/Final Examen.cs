using System;

namespace PolymorphismExample
{
    class FinalExam : Exam
    {
        public FinalExam(string name, int duration) : base(name, duration)
        {
        }

        public override void Conduct()
        {
            Console.WriteLine($"Проводится выпускной экзамен: {Name}, длительность {Duration} мин.");
        }
    }
}