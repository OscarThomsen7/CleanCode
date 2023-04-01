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
    private CommandExecutor _commandExecutor;

    private delegate void BackOverride();
    private BackOverride _backOverride;
    private delegate void LeftOverride();
    private delegate void RightOverride();
    private LeftOverride _leftOverride;
    private RightOverride _rightOverride;
    
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
        _backOverride();
    }


    //Quit the application
    public void Quit()
    {
        Console.WriteLine("The console powers down. You are free to leave.");
        Environment.Exit(0);
    }


    //Moves left in the option choices if the cursor is not already in the leftmost position.
    //Also sets the commandexecutor to the new option.  
    public void MoveLeft()
    {
        CheckLeft();
        _leftOverride();
    }


    //Moves right in the option choices if the cursor is not already in the rightmost position.
    //Also sets the commandexecutor to the new option. 
    public void MoveRight()
    {
        CheckRight();
        _rightOverride();
    }

    
    //Checks if the current menu is a main menu or a purchase menu and sets the back method to its proper implementation.
    private void CheckBack()
    {
        Dictionary<string, BackOverride> dictionary = new Dictionary<string, BackOverride>();
        dictionary.Add("Main menu", () => { Console.WriteLine("You're already on the main menu."); });
        dictionary.Add("Purchase menu",
            () =>
            {
                _context.ChangeState(new MenuState(_context, new WaresOptions(_context),
                    _menuDirector.BuildWaresMenu(_context.IsLoggedIn)));
            });
        foreach (var item in dictionary)
        {
            if (_menu.Name.Equals(item.Key))
            {
                _backOverride = item.Value;
                return;
            }
            _backOverride = () => _context.ChangeState(new MenuState(_context, new MainOptions(_context),
                _menuDirector.BuildMainMenu(_context.IsLoggedIn)));
        }
    }

    
    //Checks if the current menu is a purchase menu to set the moveleft method to its proper implementation.
    //The purchase menu is the only menu with the need for different implementation in move left
    private void CheckLeft()
    {
        if (_menu.Name.Equals("Purchase menu"))
        {
            _leftOverride = () =>
            {
                if (_context.CurrentChoice > 1)
                {
                    _context.SetCurrentChoice(_context.CurrentChoice - 1);
                    return;
                }
                _context.Message("That is not an applicable option.");
            };
            return;
        }
        _leftOverride = () =>
        {
            if (_context.CurrentChoice > 1)
            {
                _context.SetCurrentChoice(_context.CurrentChoice - 1);
                _commandExecutor = _options[_context.CurrentChoice - 1];
                return;
            }
            _context.Message("That is not an applicable option.");
        };
    }
    
    
    //Checks if the current menu is a purchase menu to set the moveright method to its proper implementation.
    //The purchase menu is the only menu with the need for different implementation in moveright
    private void CheckRight()
    {
        if (_menu.Name.Equals("Purchase menu"))
        {
            _rightOverride = () =>
            {
                if (_context.CurrentChoice < _menu.AmountOfOptions)
                {
                    _context.SetCurrentChoice(_context.CurrentChoice + 1);
                    return;
                }
                _context.Message("That is not an applicable option.");
            };
            return;
        }
        _rightOverride = () =>
        {
            if (_context.CurrentChoice < _menu.AmountOfOptions)
            {
                _context.SetCurrentChoice(_context.CurrentChoice + 1);
                _commandExecutor = _options[_context.CurrentChoice - 1];
                return;
            }
            _context.Message("That is not an applicable option.");
        };
    }
}