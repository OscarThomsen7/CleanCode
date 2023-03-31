using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class MainStateOptions : IOption
{
    //This class is the main menus options
    private readonly Context _context;
    public MainStateOptions(Context context) 
    {
        _context = context;
    }
    
    //Changes state/context to the wares menu
    public void Option1()
    {
        _context.ChangeState(new WaresState(_context));
    }
    
    //If a customer is logged in it changes state/context to the customer menu
    public void Option2()
    {
        if (_context.GetLoggedInStatus())
        {
            _context.ChangeState(new CustomerState(_context));
            return;
        }
        _context.Message("Nobody is logged in.");
    }
    
    //If a customer is not logged in it changes state/context to the login menu. Otherwise it logs the customer out.
    public void Option3()
    {
        if (_context.GetLoggedInStatus() == false)
        {
            _context.ChangeState(new LoginState(_context));
            return;
        }
        _context.Message($"{_context.GetCurrentCustomer().Username} logged out.");
        _context.LogOut();
    }

    
    //Not used
    public void Option4()
    {
    }
}