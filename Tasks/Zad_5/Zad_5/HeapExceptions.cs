namespace Zad_5;

public class HeapIsEmptyException : Exception
{
    public HeapIsEmptyException()
        : base("Куча пуста!") { }
}
public class TooBigHeapException : OutOfMemoryException
{
    public TooBigHeapException()
        : base("Слишком большая куча!") { }
}
public class WrongOperationException : InvalidOperationException
{
    public WrongOperationException()
        : base("Над данным типом невозможно!") { }
}
public class IndexOutOfHeapException : Exception
{
    public IndexOutOfHeapException()
        : base("Такого индекса нет!!") { }
}