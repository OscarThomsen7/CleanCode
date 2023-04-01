using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class MainOptions : IOption
{
    //This class is the main menus options
    private readonly Context _context;
    private MenuDirector _menuDirector = new();
    public MainOptions(Context context) 
    {
        _context = context;
    }
    
    //Changes state/context to the wares menu
    public void Option1()
    {
        //_context.ChangeState(new WaresState(_context));
        _context.ChangeState(new ContextMenu(_context, new WaresOptions(_context), _menuDirector.BuildWaresMenu(_context.IsLoggedIn)));
    }
    
    //If a customer is logged in it changes state/context to the customer menu
    public void Option2()
    {
        if (_context.IsLoggedIn)
        {
            _context.ChangeState(new ContextMenu(_context, new CustomerOptions(_context), _menuDirector.BuildCustomerMenu()));
            return;
        }
        _context.Message("Nobody is logged in.");
    }
    
    //If a customer is not logged in it changes state/context to the login menu. Otherwise it logs the customer out.
    public void Option3()
    {
        if (_context.IsLoggedIn == false)
        {
            _context.ChangeState(new ContextMenu(_context, new LoginOptions(_context), _menuDirector.BuildLoginMenu()));            
            return;
        }
        _context.Message($"{_context.CurrentCustomer.Username} logged out.");
        _context.LogOut();
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