namespace WebShopCleanCode.Command;

public class CommandExecutor
{
    
    //Command design pattern that takes a delegated method to be executed wherever this is used.
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