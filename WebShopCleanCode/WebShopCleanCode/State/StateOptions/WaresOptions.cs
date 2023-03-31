using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class WaresOptions : IOption
{
    private readonly Context _context;
    public WaresOptions(Context context) 
    {
        _context = context;
    }
    
    public void Option1()
    {
        _context.PrintProducts();
    }

    public void Option2()
    {
        if (_context.GetLoggedInStatus())
        {
            _context.ChangeState(new PurchaseState(_context));
            return;
        }
        _context.Message("You must be logged in to purchase wares.");
    }

    public void Option3()
    {
        _context.ChangeState(new SortState(_context));
    }

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