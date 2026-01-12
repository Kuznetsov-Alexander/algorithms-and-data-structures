namespace ConsoleApp4{

public class Student : IInfo, IEditable
    {
        public string LastName { get; set; }
        public List<int> Marks { get; set; } = new List<int>();

        public double AverageMark => Marks.Count > 0 ? Marks.Average() : 0;

        public void ShowInfo()
        {
            Console.WriteLine($"Студент: {LastName}, Средний балл: {AverageMark:F2}");
        }

        public void EditData()
        {
            Console.Write($"Введите новую фамилию для {LastName}: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                LastName = newName;

            Console.WriteLine($"Введите оценки через пробел: ");
            string[] parts = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts?.Length > 0)
            {
                Marks.Clear();
                foreach (var p in parts)
                    if (int.TryParse(p, out int mark)) Marks.Add(mark);
            }
        }
    }
}