namespace WebShopCleanCode.State.StateOptions;

public class CustomerStateOptions : IOption
{
    
    //This class is the customer menus options 
    private readonly Context _context;
    public CustomerStateOptions(Context context) 
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
}