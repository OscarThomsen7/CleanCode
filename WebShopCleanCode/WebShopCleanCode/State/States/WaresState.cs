using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class WaresState : MenuTemplate
{
    private readonly MenuDirector _menuDirector = new();
    public WaresState(Context context) : base(context)
    {
        IOption option = new WaresOptions(context);
        var options = new List<CommandExecutor>()
        {
            new( () => option.Option1()),
            new( () => option.Option2()),
            new( () => option.Option3()),
            new( () => option.Option4())
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildWaresMenu(context.IsLoggedIn));
        
        //Wares menu that is used when it is set to the current state/context
        //Contains a list of Commands to be executed. The commands are the option methods from the WaresOptions Class
    }
}