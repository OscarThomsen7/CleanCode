using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class LoginState : MenuTemplate
{
    private readonly MenuDirector _menuDirector = new();
    public LoginState(Context context) : base(context)
    {
        IOption option = new LoginStateOptions(context);
        var options = new List<ExecuteMethod>()
        {
            () => option.Option1(),
            () => option.Option2(),
            () => option.Option3(),
            () => option.Option4()
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildLoginMenu());
    }
}