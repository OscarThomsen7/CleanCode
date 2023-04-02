using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;

namespace WebShopCleanCode.State.States;

public class MenuTemplate
{
    //This class is the parent class to all menus with already implemented methods that can be overriden if needed. Easily extendable.

    private readonly Context _context;
    private Menu _menu;
    private MenuDirector _menuDirector = new();
    private List<CommandExecutor> _options;
    private CommandExecutor _commandExecutor, _moveMethodExecutor, _leftExecutor, _rightExecutor;
    private Dictionary<string, CommandExecutor> dictionary = new();

    protected MenuTemplate(Context context)
    {
        _context = context;
        SetDictionary();
    }
    
    //Takes parameters from the menu that is the state/context passed and sets commands and menu properties to the menu passed.
    protected void SetMethodListAndMenuType(List<CommandExecutor> methods, Menu menu)
    {
        _menu = menu;
        _options = methods;
        _commandExecutor = _options[0];
    }
    
    //Prints the current menu
    public void ShowMenu()
    {
        if (_menu.Name.Equals("Purchase menu"))
        {
            _context.OutputProducts();
        }
        _menu.DisplayMenu(_context.CurrentChoice);
    }
    
    //Execute the current chosen method
    public void Ok()
    {
        _commandExecutor.ExecuteMethod();
    }
    
    //Changes state/context back to main menu
    public void Back()
    {
        CheckBack();
        _moveMethodExecutor.ExecuteMethod();
    }
    
    //Quit the application
    public void Quit()
    {
        Console.WriteLine("The console powers down. You are free to leave.");
        Environment.Exit(0);
    }
    
    //Moves left in the option choices if the cursor is not already in the leftmost position.
    public void MoveLeft()
    {
        CheckLeft();
        _moveMethodExecutor.ExecuteMethod();
    }
    
    //Moves right in the option choices if the cursor is not already in the rightmost position.
    public void MoveRight()
    {
        CheckRight();
        _moveMethodExecutor.ExecuteMethod();
    }
    
    //Checks if the current menu is a main menu or a purchase menu and sets the back method to its proper implementation
    //from the dictionary of methods.
    private void CheckBack()
    {
        foreach (var item in dictionary)
        {
            if (_menu.Name.Equals(item.Key))
            {
                _moveMethodExecutor = item.Value;
                return;
            }
            _moveMethodExecutor = new(() => _context.ChangeState(new MenuState(_context, new MainOptions(_context),
                _menuDirector.BuildMainMenu(_context.IsLoggedIn))));
        }
    }
    
    //Checks if the current menu is a purchase menu to set the moveleft method to its proper implementation.
    //The purchase menu is the only menu with the need for different implementation in move left
    private void CheckLeft()
    {
        if (_menu.Name.Equals("Purchase menu"))
        {
            _moveMethodExecutor = dictionary["Purchase menu left"];
            return;
        }
        _moveMethodExecutor = dictionary["left"];
    }
    
    //Checks if the current menu is a purchase menu to set the moveright method to its proper implementation.
    //The purchase menu is the only menu with the need for different implementation in moveright
    private void CheckRight()
    {
        if (_menu.Name.Equals("Purchase menu"))
        {
            _moveMethodExecutor = dictionary["Purchase menu right"];
            return;
        }
        _moveMethodExecutor = dictionary["right"];
    }
    
    //Adds all keys and values/commandExecutors to the dictionary, these are used in Back(), MoveLeft() and MoveRight(). 
    private void SetDictionary()
    {
        dictionary.Add("Main menu", new(() => { Console.WriteLine("You're already on the main menu."); }));
        dictionary.Add("Purchase menu", new(() => { 
            _context.ChangeState(new MenuState(_context, new WaresOptions(_context),
                _menuDirector.BuildWaresMenu(_context.IsLoggedIn))); }));
        dictionary.Add("Purchase menu left", new(() =>
        {
            {
                if (_context.CurrentChoice > 1)
                {
                    _context.SetCurrentChoice(_context.CurrentChoice - 1);
                    return;
                }
                _context.Message("That is not an applicable option.");
            }
        }));
        dictionary.Add("left", new(() =>
        {
            if (_context.CurrentChoice > 1)
            {
                _context.SetCurrentChoice(_context.CurrentChoice - 1);
                _commandExecutor = _options[_context.CurrentChoice - 1];
                return;
            }
            _context.Message("That is not an applicable option.");
        }));
        dictionary.Add("Purchase menu right", new(() =>
        {
            {
                if (_context.CurrentChoice < _menu.AmountOfOptions)
                {
                    _context.SetCurrentChoice(_context.CurrentChoice + 1);
                    return;
                }
                _context.Message("That is not an applicable option.");
            }
        }));
        dictionary.Add("right", new(() =>
        {
            if (_context.CurrentChoice < _menu.AmountOfOptions)
            {
                _context.SetCurrentChoice(_context.CurrentChoice + 1);
                _commandExecutor = _options[_context.CurrentChoice - 1];
                return;
            }
            _context.Message("That is not an applicable option.");
        }));
    }
}