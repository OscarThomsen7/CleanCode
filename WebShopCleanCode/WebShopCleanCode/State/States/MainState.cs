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
        IOption option = new MainOptions(context);
        var options = new List<CommandExecutor>()
        {
            new(() => option.Option1()),
            new(() => option.Option2()),
            new(() => option.Option3()),
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildMainMenu(context.IsLoggedIn));
    }

    
    //Overrides the back method from MenuTemplate Class because it has a different implementation
    public override void Back()
    {
        Console.WriteLine("You're already on the main menu.");
        _context.ChangeState(new MainState(_context));
    }
    
    
    //Main menu that is used when it is set to the current state/context
    //Contains a list of Commands to be executed. The commands are the option methods from the MainOptions Class
}