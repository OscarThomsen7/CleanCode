using WebShopCleanCode.SortingAlgorithm;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State.StateOptions;

public class SortStateOptions :IOption
{
    private readonly Context _context;
    private readonly int _length;
    private readonly List<Product> _products;
    private QuickSort _quickSort = new();

    public SortStateOptions(Context context)
    {
        _context = context;
        _length = _context.GetProductCount();
        _products = _context.GetProducts();
    }

    public void Option1()
    {
        _quickSort.SortName(_products, 0, _length - 1, false);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }

    public void Option2()
    {
        _quickSort.SortName(_products, 0, _length - 1, true);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }

    public void Option3()
    {
        _quickSort.SortPrice(_products, 0, _length - 1, false);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }

    public void Option4()
    {
        _quickSort.SortPrice(_products, 0, _length - 1, true);
        _context.Message("Wares sorted.");
        _context.ChangeState(new WaresState(_context));
    }
}