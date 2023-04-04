using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class LoginOptions : IOption
{
    //This class is the login menus options 
    private Context _context;
    private MenuDirector _menuDirector = new();

    public LoginOptions(Context context)
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
        if (_context.Username.Equals("") || _context.Password.Equals(""))
        {
            _context.Message("Incomplete data.");
            return;
        }
        foreach (Customer? customer in _context.Customers)
        {
            if (_context.Username.Equals(customer.Username) && _context.Password.Equals(customer.Password))
            {
                _context.Message($"{ customer.Username} logged in.");
                _context.Login(customer);
                _context.ChangeState(new MenuState(_context, new MainOptions(_context), _menuDirector.BuildMainMenu(_context.IsLoggedIn)));                return;
            }
        }
        _context.Message("Invalid credentials.");
    }
    
    //Creates a new customer, logs in to it and saves it to the database.
    public void Option4()
    {
        _context.RegisterCustomer();
    }

    public List<CommandExecutor> GetOptions()
    {
        return new List<CommandExecutor>
        {
            new(() => Option1()),
            new(() => Option2()),
            new(() => Option3()),
            new(() => Option4())
        };
    }
}