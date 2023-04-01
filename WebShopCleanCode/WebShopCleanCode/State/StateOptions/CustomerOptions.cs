using WebShopCleanCode.Command;

namespace WebShopCleanCode.State.StateOptions;

public class CustomerOptions : IOption
{
    
    //This class is the customer menus options 
    private readonly Context _context;
    public CustomerOptions(Context context) 
    {
        _context = context;
    }
    
    //Prints current customers orders.
    public void Option1()
    {
        _context.PrintOrders();
    }

    
    //Prints current customers properties/info
    public void Option2()
    {
        _context.PrintInfo();
    }

    
    //Adds funds to current customer
    public void Option3()
    {
        _context.AddFunds();
    }

    //Not used
    public void Option4()
    {
    }

    public List<CommandExecutor> GetOptions()
    {
        return new List<CommandExecutor>
        {
            new(() => Option1()),
            new(() => Option2()),
            new(() => Option3())
        };
    }
}