using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class MenuState : MenuTemplate
{
    public MenuState(Context context, IOption option, Menu menu) : base(context)
    {
        SetMethodListAndMenuType(option.GetOptions(), menu);
    }
}
