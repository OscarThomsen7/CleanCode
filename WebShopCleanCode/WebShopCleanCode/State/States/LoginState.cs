using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class LoginState : MenuTemplate
{
    private readonly MenuDirector _menuDirector = new();
    public LoginState(Context context) : base(context)
    {
        IOption option = new LoginOptions(context);
        var options = new List<CommandExecutor>()
        {
            new( () => option.Option1()),
            new( () => option.Option2()),
            new( () => option.Option3()),
            new( () => option.Option4())
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildLoginMenu());
        
        //Login menu that is used when it is set to the current state/context
        //Contains a list of Commands to be executed. The commands are the option methods from the LoginOptions Class
    }
}