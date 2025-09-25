using System;

namespace PolymorphismExample
{
    interface IGradable
    {
        void Grade();
    }

    interface ISchedulable
    {
        void Schedule(DateTime date);
    }

    interface IPrintable
    {
        void PrintResult();
    }
    abstract class Assessment
    {
        public string Name { get; set; }
        public int Duration { get; set; }

        public Assessment(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }

        public abstract void Conduct(); 
    }
}