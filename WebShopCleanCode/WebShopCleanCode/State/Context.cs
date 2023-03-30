using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State;

public class Context
{
    private MenuTemplate _currentMenuState;
    private readonly WebShop _currentWebShopState;
    private Dictionary<string, CommandExecutor> _inputDictionary = new();

    public Context()
    {
        _currentWebShopState = new WebShop();
        _currentMenuState = new MainState(this);
        SetDictionary();
    }
    private void SetDictionary()
    {
        _inputDictionary.Add("left", new CommandExecutor(MoveLeft));
        _inputDictionary.Add("l", new CommandExecutor(MoveLeft));
        _inputDictionary.Add("right", new CommandExecutor(MoveRight));
        _inputDictionary.Add("r", new CommandExecutor(MoveRight));
        _inputDictionary.Add("ok", new CommandExecutor(OnOk));
        _inputDictionary.Add("o", new CommandExecutor(OnOk));
        _inputDictionary.Add("k", new CommandExecutor(OnOk));
        _inputDictionary.Add("back", new CommandExecutor(OnBack));
        _inputDictionary.Add("b", new CommandExecutor(OnBack));
        _inputDictionary.Add("q", new CommandExecutor(Quit));
        _inputDictionary.Add("quit", new CommandExecutor(Quit));
    }
    public void LogOut()
    {
        _currentWebShopState.CurrentCustomer = null;
        _currentWebShopState.IsLoggedIn = false;
        _currentWebShopState.Password = "";
        _currentWebShopState.Username = "";
        ChangeState(new MainState(this));
    }

    public string GetUserName()
    {
        return _currentWebShopState.Username!;
    }

    public string GetPassWord()
    {
        return _currentWebShopState.Password!;
    }

    public void SetUserName()
    {
        Message("A keyboard appears.\nPlease input your username.");
        _currentWebShopState.Username = Console.ReadLine();
        Console.WriteLine();
    }
    
    public void SetPassWord()
    {
        Message("A keyboard appears.\nPlease input your password.");
        _currentWebShopState.Password = Console.ReadLine();
        Console.WriteLine();
    }
    
    public int GetProductCount()
    {
        return _currentWebShopState.Products.Count;
    }
    public List<Product> GetProducts()
    {
        return _currentWebShopState.Products;
    }
 
    public void SetCurrentCustomer(Customer? customer)
    {
        _currentWebShopState.CurrentCustomer = customer;
        _currentWebShopState.IsLoggedIn = true;
    }
    
    public List<Customer?> GetCustomers()
    {
        return _currentWebShopState.Customers;
    }
    
    public void OutputProducts()
    {
        int spotInList = 1;
        foreach (var product in GetProducts())
        {
            Console.WriteLine($"{spotInList}: {product.Name} , {product.Price} kr");
            spotInList++;
        }
        Console.WriteLine("Your funds: " + GetCurrentCustomer()!.Funds);
    }
    public void PrintProducts()
    {
        Console.WriteLine();
        foreach (Product product in GetProducts())
        {
            product.PrintInfo();
        }
        Console.WriteLine();
    }

    public void PrintOrders()
    {
        _currentWebShopState.CurrentCustomer!.PrintOrders();
    }
    public void PrintInfo()
    {
        _currentWebShopState.CurrentCustomer!.PrintInfo();
    }
    public void AddFunds()
    {
        Console.WriteLine("How many funds would you like to add?");
        if (int.TryParse(Console.ReadLine(), out var amount))
        {
            if (amount < 0)
            {
                Message("Don't add negative amounts.");
                return;
            }
            GetCurrentCustomer()!.Funds += amount;
            Message($"{amount} added to your profile.");
            return;
        }
        Message("Please write a number next time.");
    }

    public Customer? GetCurrentCustomer()
    {
        return _currentWebShopState.CurrentCustomer;
    }

    public void SetCurrentChoice(int position)
    {
        _currentWebShopState.CurrentChoice = position;
    }
    public int GetCurrentChoice()
    {
        return _currentWebShopState.CurrentChoice;
    }

    public void RegisterCustomer()
    {
        _currentWebShopState.RegisterCustomer();
        ChangeState(new MainState(this));
    }

    public void ChangeState(MenuTemplate menu)
    {
        _currentMenuState = menu;
    }

    public void Message(string message)
    {
        Console.WriteLine($"\n{message}\n");
    }

    public bool GetLoggedInStatus()
    {
        return _currentWebShopState.IsLoggedIn;
    }


    private void OnOk()
    {
        _currentMenuState.Ok();
        SetCurrentChoice(1);
    }

    private void OnBack()
    {
        _currentMenuState.Back();
    }

    public void ShowMenu()
    {
        _currentMenuState.ShowMenu();
        CustomerCheck();
    }

    private void CustomerCheck()
    {
        Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
        if (GetLoggedInStatus())
        {
            Console.WriteLine($"Current user: {_currentWebShopState.CurrentCustomer!.Username}");
            return;
        }
        Console.WriteLine("Nobody logged in.");
        
    }
    
    private void MoveInMenu(string choice)
    {
        foreach (var item in _inputDictionary)
        {
            if (String.Equals(choice, item.Key, StringComparison.CurrentCultureIgnoreCase))
            {
                item.Value.ExecuteMethod();
                return;
            }
        }
        Console.WriteLine("That is not an applicable option.");
    }

    public void Run()
    {
        Console.WriteLine("Welcome to the WebShop!");
        while (true)
        {
            ShowMenu();
            var choice = Console.ReadLine()!;
            MoveInMenu(choice);
        }
    }

    private void Quit()
    {
        _currentMenuState.Quit();
    }

    private void MoveLeft()
    {
        _currentMenuState.MoveLeft();
    }

    private void MoveRight()
    {
        _currentMenuState.MoveRight();
    }
}