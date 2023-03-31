using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class LoginStateOptions : IOption
{
    //This class is the login menus options 
    private Context _context;

    public LoginStateOptions(Context context)
    {
        _context = context;
    }
    
    //input a username to log in
    public void Option1()
    {
        _context.SetUserName();
    }
    
    //input a password to log in
    public void Option2()
    {
        _context.SetPassWord();
    }
    
    //Tries to log in. If username or password is empty or does not match a customer from the customer list it does not log in.
    //otherwise it logs in to the customer with matched password and username.
    public void Option3()
    {
        if (_context.GetUserName().Equals("") || _context.GetPassWord().Equals(""))
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
    
    //Creates a new customer, logs in to it and saves it to the database.
    public void Option4()
    {
        _context.RegisterCustomer();
    }
}