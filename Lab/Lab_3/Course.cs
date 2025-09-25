using System;

namespace LabWork3
{
    class Course
    {
        public int Number { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();

        public double AverageMark
        {
            get
            {
                var allMarks = Groups.SelectMany(g => g.Students).SelectMany(s => s.Marks);
                return allMarks.Any() ? allMarks.Average() : 0;
            }
        }
    }
}