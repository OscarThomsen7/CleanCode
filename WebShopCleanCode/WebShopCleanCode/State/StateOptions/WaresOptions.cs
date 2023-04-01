using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class WaresOptions : IOption
{
    
    //This class is the wares menus options
    private readonly Context _context;
    private MenuDirector _menuDirector = new();

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
        if (_context.IsLoggedIn)
        {
            _context.ChangeState(new ContextMenu(_context, new PurchaseOptions(_context), _menuDirector.BuildPurchaseMenu(_context.Products.Count)));
            return;
        }
        _context.Message("You must be logged in to purchase wares.");
    }

    
    //changes state/context to the sort menu
    public void Option3()
    {
        _context.ChangeState(new ContextMenu(_context, new SortOptions(_context), _menuDirector.BuildSortMenu()));            
    }

    
    //Checks if customer is logged in or not. If not it changes state/context to the sort menu. Otherwise it logs the customer out
    public void Option4()
    {
        if (_context.IsLoggedIn == false)
        {
            _context.ChangeState(new ContextMenu(_context, new LoginOptions(_context), _menuDirector.BuildLoginMenu()));            
            return;
        }
        _context.Message($"{_context.CurrentCustomer.Username} logged out.");
        _context.LogOut();
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