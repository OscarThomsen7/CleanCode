using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class LoginStateOptions : IOption
{
    private Context _context;

    public LoginStateOptions(Context context)
    {
        _context = context;
    }
    public void Option1()
    {
        _context.SetUserName();
    }

    public void Option2()
    {
        _context.SetPassWord();
    }

    public void Option3()
    {
        if (_context.GetUserName().Equals(null) || _context.GetPassWord().Equals(null))
        {
            _context.Message("Incomplete data.");
            return;
        }
        foreach (Customer? customer in _context.GetCustomers())
        {
            if (_context.GetUserName().Equals(customer.Username) && _context.GetPassWord().Equals(customer.Password))
            {
                _context.Message($"{ customer.Username} logged in.");
                _context.SetCurrentCustomer(customer);
                _context.ChangeState(new MainState(_context));
                return;
            }
            _context.Message("Invalid credentials.");
        }
    }

    public void Option4()
    {
        _context.RegisterCustomer();
    }
}