using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class WaresOptions : IOption
{
    
    //This class is the wares menus options
    private readonly Context _context;
    public WaresOptions(Context context) 
    {
        _context = context;
    }
    
    
    //Prints all the products
    public void Option1()
    {
        _context.PrintProducts();
    }

    
    //Checks if a customer is logged in. If so, changes state/context to the purchase menu
    public void Option2()
    {
        if (_context.GetLoggedInStatus())
        {
            _context.ChangeState(new PurchaseState(_context));
            return;
        }
        _context.Message("You must be logged in to purchase wares.");
    }

    
    //changes state/context to the sort menu
    public void Option3()
    {
        _context.ChangeState(new SortState(_context));
    }

    
    //Checks if customer is logged in or not. If not it changes state/context to the sort menu. Otherwise it logs the customer out
    public void Option4()
    {
        if (_context.GetLoggedInStatus() == false)
        {
            _context.ChangeState(new LoginState(_context));
            return;
        }
        _context.Message($"{_context.GetCurrentCustomer().Username} logged out.");
        _context.LogOut();
    }
}