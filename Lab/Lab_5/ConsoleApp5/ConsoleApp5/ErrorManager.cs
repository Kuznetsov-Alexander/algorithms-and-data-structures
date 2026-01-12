namespace ConsoleApp5;

public class ErrorManager
{
    public event EventHandler<ErrorEventArgs> ErrorOccurred;

    public void RaiseError(string message)
    {
        ErrorOccurred?.Invoke(this, new ErrorEventArgs(message));
    }
}