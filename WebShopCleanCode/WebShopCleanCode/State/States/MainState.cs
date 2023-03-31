using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class MainState : MenuTemplate
{
    private Context _context;
    private readonly MenuDirector _menuDirector = new();
    public MainState(Context context) : base(context)
    {
        _context = context;
        IOption option = new MainStateOptions(context);
        var options = new List<CommandExecutor>()
        {
            new(() => option.Option1()),
            new(() => option.Option2()),
            new(() => option.Option3()),
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildMainMenu(context.GetLoggedInStatus()));
    }

    public override void Back()
    {
        Console.WriteLine("You're already on the main menu.");
        _context.ChangeState(new MainState(_context));
    }
}