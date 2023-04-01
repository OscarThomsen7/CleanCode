using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.SortingAlgorithm;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class SortOptions :IOption
{
    
    //This class is the sort menus options
    private readonly Context _context;
    private readonly int _length;
    private readonly List<Product> _products;
    private QuickSort _quickSort = new();
    private MenuDirector _menuDirector = new();

    public SortOptions(Context context)
    {
        _context = context;
        _length = _context.Products.Count;
        _products = _context.Products;
    }

    
    //Sorts product list by name, descending
    public void Option1()
    {
        _quickSort.SortByName(_products, 0, _length - 1, false);
        _context.Message("Wares sorted.");
        _context.ChangeState(new MenuState(_context, new WaresOptions(_context), _menuDirector.BuildWaresMenu(_context.IsLoggedIn)));            
    }

    //Sorts product list by name, ascending
    public void Option2()
    {
        _quickSort.SortByName(_products, 0, _length - 1, true);
        _context.Message("Wares sorted.");
        _context.ChangeState(new MenuState(_context, new WaresOptions(_context), _menuDirector.BuildWaresMenu(_context.IsLoggedIn)));            
    }

    //Sorts product list by price, descending
    public void Option3()
    {
        _quickSort.SortByPrice(_products, 0, _length - 1, false);
        _context.Message("Wares sorted.");
        _context.ChangeState(new MenuState(_context, new WaresOptions(_context), _menuDirector.BuildWaresMenu(_context.IsLoggedIn)));            
    }

    //Sorts product list by price,ascending
    public void Option4()
    {
        _quickSort.SortByPrice(_products, 0, _length - 1, true);
        _context.Message("Wares sorted.");
        _context.ChangeState(new MenuState(_context, new WaresOptions(_context), _menuDirector.BuildWaresMenu(_context.IsLoggedIn)));            
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