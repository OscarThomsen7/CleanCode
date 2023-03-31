using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;

namespace WebShopCleanCode.State.States;

public class MenuTemplate
{
    private readonly Context _context;
    private Menu _menu;

    protected delegate void ExecuteMethod();
    private ExecuteMethod _executeMethod;
    private List<CommandExecutor> _options;
    private CommandExecutor _commandExecutor;

    protected MenuTemplate(Context context)
    {
        _context = context;
    }

    protected void SetMethodListAndMenuType(List<CommandExecutor> methods, Menu menu)
    {
        _menu = menu;
        _options = methods;
        _commandExecutor = _options[0];
    }

    public virtual void ShowMenu()
    {
        _menu.DisplayMenu(_context.GetCurrentChoice());
    }

    public void Ok()
    {
        _commandExecutor.ExecuteMethod();
    }

    public virtual void Back()
    {
        _context.ChangeState(new MainState(_context));
    }

    public void Quit()
    {
        Console.WriteLine("The console powers down. You are free to leave.");
        Environment.Exit(0);
    }

    public virtual void MoveLeft()
    {
        if (_context.GetCurrentChoice() > 1)
        {
            _context.SetCurrentChoice(_context.GetCurrentChoice() - 1);
            _commandExecutor = _options[_context.GetCurrentChoice() - 1];
            return;
        }
        _context.Message("That is not an applicable option.");
    }
    
    public virtual void MoveRight()
    {
        if (_context.GetCurrentChoice() < _menu.AmountOfOptions)
        {
            _context.SetCurrentChoice(_context.GetCurrentChoice() + 1);
            _commandExecutor = _options[_context.GetCurrentChoice() - 1];
            return;
        }
        _context.Message("That is not an applicable option.");
    }
}