using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class CustomerState : MenuTemplate
{
    private readonly MenuDirector _menuDirector = new();
    public CustomerState(Context context) : base(context)
    {
        IOption option = new CustomerOptions(context);
        var options = new List<CommandExecutor>()
        {
            new( () => option.Option1()),
            new( () => option.Option2()),
            new( () => option.Option3()),
            new( () => option.Option4())
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildCustomerMenu());
    }
    
    //Customer menu that is used when it is set to the current state/context
    //Contains a list of Commands to be executed. The commands are the option methods from the CustomerOptions Class
}