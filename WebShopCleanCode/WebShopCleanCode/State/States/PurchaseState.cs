using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class PurchaseState : MenuTemplate
{
    private readonly Menu _menu;
    private Context _context;
    private readonly MenuDirector _menuDirector = new();
    public PurchaseState(Context context) : base(context)
    {
        _context = context;
        _menu = _menuDirector.BuildPurchaseMenu(_context.GetProductCount());
        IOption option = new PurchaseStateOptions(context);
        var options = new List<CommandExecutor>()
        {
            new( () => option.Option1())
        };
        SetMethodListAndMenuType(options, _menu);
    }


    public override void Back()
    {
        _context.ChangeState(new WaresState(_context));
    }

    public override void MoveLeft()
    {
        if (_context.GetCurrentChoice() > 1)
        {
            _context.SetCurrentChoice(_context.GetCurrentChoice() - 1);
            return;
        }
        _context.Message("That is not an applicable option.");
    }
    
    public override void MoveRight()
    {
        if (_context.GetCurrentChoice() < _menu.AmountOfOptions)
        {
            _context.SetCurrentChoice(_context.GetCurrentChoice() + 1);
            return;
        }
        _context.Message("That is not an applicable option.");
    }

    public override void ShowMenu()
    {
        _context.OutputProducts();
        _menu.DisplayMenu(_context.GetCurrentChoice());
    }
}