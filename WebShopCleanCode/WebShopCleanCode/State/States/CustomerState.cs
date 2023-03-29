using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class CustomerState : MenuTemplate
{
    private readonly MenuDirector _menuDirector = new();
    public CustomerState(Context context) : base(context)
    {
        IOption option = new CustomerStateOptions(context);
        var options = new List<ExecuteMethod>()
        {
            () => option.Option1(),
            () => option.Option2(),
            () => option.Option3(),
            () => option.Option4()
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildCustomerMenu());
    }
}