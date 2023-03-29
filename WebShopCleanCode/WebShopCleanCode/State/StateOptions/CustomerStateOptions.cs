namespace WebShopCleanCode.State.StateOptions;

public class CustomerStateOptions : IOption
{
    private readonly Context _context;
    public CustomerStateOptions(Context context) 
    {
        _context = context;
    }
    public void Option1()
    {
        _context.PrintOrders();
    }

    public void Option2()
    {
        _context.PrintInfo();
    }

    public void Option3()
    {
        _context.AddFunds();
    }

    public void Option4()
    {
        throw new NotImplementedException();
    }
}