namespace WebShopCleanCode.Command;

public class CommandExecutor
{
    public delegate void DelegateMethod();

    private DelegateMethod _delegateMethod;

    public CommandExecutor(DelegateMethod method)
    {
        _delegateMethod = method;
    }

    public void ExecuteMethod()
    {
        _delegateMethod();
    }
}