using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;

namespace WebShopCleanCode.State.States;

public class MenuTemplate
{
    //This class is the parent class to all menus with already implemented methods that can be overriden if needed. Easily extendable.
    
    private readonly Context _context;
    private Menu _menu;
    private List<CommandExecutor> _options;
    private CommandExecutor _commandExecutor;

    protected MenuTemplate(Context context)
    {
        _context = context;
    }

    
    //Takes parameters from the menu that is the current state/context and sets commands and menu properties to the current menu passed.
    protected void SetMethodListAndMenuType(List<CommandExecutor> methods, Menu menu)
    {
        _menu = menu;
        _options = methods;
        _commandExecutor = _options[0];
    }

    
    //Prints the current menu
    public virtual void ShowMenu()
    {
        _menu.DisplayMenu(_context.GetCurrentChoice());
    }

    
    //Execute the current chosen method
    public void Ok()
    {
        _commandExecutor.ExecuteMethod();
    }

    
    //Changes state/context back to main menu
    public virtual void Back()
    {
        _context.ChangeState(new MainState(_context));
    }

    
    //Quit the application
    public void Quit()
    {
        Console.WriteLine("The console powers down. You are free to leave.");
        Environment.Exit(0);
    }

    
    //Moves left in the option choices if the cursor is not already in the leftmost position.
    //Also sets the commandexecutor to the new option.  
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
    
    
    //Moves right in the option choices if the cursor is not already in the rightmost position.
    //Also sets the commandexecutor to the new option. 
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