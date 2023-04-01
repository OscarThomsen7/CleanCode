using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class ContextMenu : MenuTemplate
{
    public ContextMenu(Context context, IOption option, Menu menu) : base(context)
    {
        SetMethodListAndMenuType(option.GetOptions(), menu);
    }
}
