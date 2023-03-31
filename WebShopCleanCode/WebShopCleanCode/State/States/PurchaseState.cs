using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class PurchaseState : MenuTemplate
{
    //Purchase menu that is used when it is set to the current state/context
    //Contains a list of Commands to be executed. The commands are the option methods from the PurchaseOptions Class
    
    private readonly Menu _menu;
    private Context _context;
    private readonly MenuDirector _menuDirector = new();
    public PurchaseState(Context context) : base(context)
    {
        _context = context;
        _menu = _menuDirector.BuildPurchaseMenu(_context.GetProductCount());
        IOption option = new PurchaseOptions(context);
        var options = new List<CommandExecutor>()
        {
            new( () => option.Option1())
        };
        SetMethodListAndMenuType(options, _menu);
    }


    
    //Overrides the back method from MenuTemplate Class because it has a different implementation
    public override void Back()
    {
        _context.ChangeState(new WaresState(_context));
    }

    //Overrides the Moveleft method from MenuTemplate Class because it has a different implementation
    //Only has one command to be executed so it only changes the cursor position to choose different products
    public override void MoveLeft()
    {
        if (_context.GetCurrentChoice() > 1)
        {
            _context.SetCurrentChoice(_context.GetCurrentChoice() - 1);
            return;
        }
        _context.Message("That is not an applicable option.");
    }
    
    //Overrides the Moveright method from MenuTemplate Class because it has a different implementation
    //Only has one command to be executed so it only changes the cursor position to choose different products
    public override void MoveRight()
    {
        if (_context.GetCurrentChoice() < _menu.AmountOfOptions)
        {
            _context.SetCurrentChoice(_context.GetCurrentChoice() + 1);
            return;
        }
        _context.Message("That is not an applicable option.");
    }

    
    //Overrides the Moveright method from MenuTemplate Class because it has a different implementation
    //Outputs all the products and the choices menu.
    public override void ShowMenu()
    {
        _context.OutputProducts();
        _menu.DisplayMenu(_context.GetCurrentChoice());
    }
}