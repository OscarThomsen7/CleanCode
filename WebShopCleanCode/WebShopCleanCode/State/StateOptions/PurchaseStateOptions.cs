using WebShopCleanCode.Builder.BuildCustomer;

namespace WebShopCleanCode.State.StateOptions;

public class PurchaseStateOptions : IOption
{
    private Context _context;

    public PurchaseStateOptions(Context context)
    {
        _context = context;
    }
    public void Option1()
    {
        Customer? currentCustomer = _context.GetCurrentCustomer();
        Product product = _context.GetProducts()[_context.GetCurrentChoice() - 1];
        if (product.InStock())
        {
            if (currentCustomer.CanAfford(product.Price))
            {
                currentCustomer.Funds -= product.Price;
                product.NrInStock--;
                currentCustomer.Orders.Add(new Order(product.Name, product.Price, DateTime.Now));
                _context.Message($"Successfully bought {product.Name}");
                return;
            }
            _context.Message("You cannot afford.");
            return;
        }
        _context.Message("Not in stock.");
    }

    public void Option2()
    {
        throw new NotImplementedException();
    }

    public void Option3()
    {
        throw new NotImplementedException();
    }

    public void Option4()
    {
        throw new NotImplementedException();
    }
}