using System;

class Ispytanie
{
    public string Title { get; set; }
    public int MaxScore { get; set; }

    public Ispytanie(string title, int maxScore)
    {
        Title = title;
        MaxScore = maxScore;
    }

    public virtual void Info()
    {
        Console.WriteLine($"Испытание: {Title}, макс. балл: {MaxScore}");
    }
}

class Test : Ispytanie
{
    public int Questions { get; set; }

    public Test(string title, int maxScore, int questions)
        : base(title, maxScore)
    {
        Questions = questions;
    }

    public override void Info()
    {
        Console.WriteLine($"Тест: {Title}, вопросов: {Questions}, макс. балл: {MaxScore}");
    }
}

class Exam : Test
{
    public string Subject { get; set; }

    public Exam(string title, int maxScore, int questions, string subject)
        : base(title, maxScore, questions)
    {
        Subject = subject;
    }

    public override void Info()
    {
        Console.WriteLine($"Экзамен по предмету {Subject}: {Title}, вопросов: {Questions}, макс. балл: {MaxScore}");
    }
}

class VypusknoiExam : Exam
{
    public string Commission { get; set; }

    public VypusknoiExam(string title, int maxScore, int questions, string subject, string commission)
        : base(title, maxScore, questions, subject)
    {
        Commission = commission;
    }

    public override void Info()
    {
        Console.WriteLine($"Выпускной экзамен ({Subject}) для комиссии {Commission}: {Title}, макс. балл: {MaxScore}");
    }
}

class Program
{
    static void Main()
    {
        Ispytanie isp = new Ispytanie("Общий зачет", 50);
        Test test = new Test("Тест по математике", 100, 20);
        Exam exam = new Exam("Экзамен по программированию", 120, 15, "C#");
        VypusknoiExam vypusk = new VypusknoiExam("Госэкзамен", 150, 10, "ООП", "ГЭК №3");

        isp.Info();
        test.Info();
        exam.Info();
        vypusk.Info();

        Console.ReadLine();
    }
}