using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class MainStateOptions : IOption
{
    private readonly Context _context;
    public MainStateOptions(Context context) 
    {
        _context = context;
    }
    
    public void Option1()
    {
        _context.ChangeState(new WaresState(_context));
    }

    public void Option2()
    {
        if (_context.GetLoggedInStatus())
        {
            _context.ChangeState(new CustomerState(_context));
            return;
        }
        _context.Message("Nobody is logged in.");
    }

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

    public void Option4()
    {
        throw new NotImplementedException();
    }
}