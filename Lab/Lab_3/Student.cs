using System;

namespace LabWork3
{
    class Student
    {
        public string LastName { get; set; }
        public List<int> Marks { get; set; }

        public double AverageMark => Marks.Count > 0 ? Marks.Average() : 0;
    }
}