using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class SortState : MenuTemplate
{
    private readonly MenuDirector _menuDirector = new();
    public SortState(Context context) : base(context)
    {
        IOption option = new SortStateOptions(context);
        var options = new List<CommandExecutor>()
        {
            new( () => option.Option1()),
            new( () => option.Option2()),
            new( () => option.Option3()),
            new( () => option.Option4())
        };
        SetMethodListAndMenuType(options, _menuDirector.BuildSortMenu());
    }
}