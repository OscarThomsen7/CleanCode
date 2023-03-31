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

    public SortOptions(Context context)
    {
        _context = context;
        _length = _context.GetProductCount();
        _products = _context.GetProducts();
    }

    
    //Sorts product list by name, descending
    public void Option1()
    {
        _quickSort.SortByName(_products, 0, _length - 1, false);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }

    //Sorts product list by name, ascending
    public void Option2()
    {
        _quickSort.SortByName(_products, 0, _length - 1, true);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }

    //Sorts product list by price, descending
    public void Option3()
    {
        _quickSort.SortByPrice(_products, 0, _length - 1, false);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }

    //Sorts product list by price,ascending
    public void Option4()
    {
        _quickSort.SortByPrice(_products, 0, _length - 1, true);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }
}