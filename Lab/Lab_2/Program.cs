using System;
namespace PolymorphismExample
{
class Program
{
    static void Main(string[] args)
    {
        Assessment[] assessments =
        {
            new Test("Тест по математике", 30),
            new Exam("Экзамен по истории", 90),
            new FinalExam("Выпускной экзамен по программированию", 120)
        };

        foreach (Assessment a in assessments)
        {
            a.Conduct();
        }

        Console.ReadKey();
    }
}
}