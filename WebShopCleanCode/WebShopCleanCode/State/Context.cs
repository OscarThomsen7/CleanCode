using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State;

public class Context
{
    //This class is what runs the whole application.
    //different methods will be executed depending oon what state/menu the application is set to.
    public Database Db { get;}
    private MenuTemplate _currentMenuState;
    private readonly WebShop _currentWebShopState;
    private Dictionary<string, CommandExecutor> _inputDictionary = new();

    public Context()
    {
        _currentWebShopState = new WebShop();
        _currentMenuState = new MainState(this);
        Db = _currentWebShopState.Database;
        SetDictionary();
    }
    
    //Create a dictionary of commands to get rid of the need for nested ifs or switch cases when taking user input.
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
    
    //Logs customer out and resets login properties
    public void LogOut()
    {
        _currentWebShopState.CurrentCustomer = null;
        _currentWebShopState.IsLoggedIn = false;
        _currentWebShopState.Password = "";
        _currentWebShopState.Username = "";
        ChangeState(new MainState(this));
    }

    //Gets logged in customers username
    public string GetUserName()
    {
        return _currentWebShopState.Username!;
    }

    //Gets logged in customers password
    public string GetPassWord()
    {
        return _currentWebShopState.Password!;
    }

    //Sets username property to be used for login attempt
    public void SetUserName()
    {
        Message("A keyboard appears.\nPlease input your username.");
        _currentWebShopState.Username = Console.ReadLine();
        Console.WriteLine();
    }
    
    //Sets password property to be used for login attempt 
    public void SetPassWord()
    {
        Message("A keyboard appears.\nPlease input your password.");
        _currentWebShopState.Password = Console.ReadLine();
        Console.WriteLine();
    }
    
    //Gets product list length
    public int GetProductCount()
    {
        return _currentWebShopState.Products.Count;
    }

    //Gets products list
    public List<Product> GetProducts()
    {
        return _currentWebShopState.Products;
    }

    //sets the current customer to a customer object
    public void SetCurrentCustomer(Customer? customer)
    {
        _currentWebShopState.CurrentCustomer = customer;
        _currentWebShopState.IsLoggedIn = true;
    }
    
    //Gets customer list
    public List<Customer?> GetCustomers()
    {
        return _currentWebShopState.Customers;
    }

    //Prints all products for the purchase menu
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
    
    //Prints all products for wares menu
    public void PrintProducts()
    {
        Console.WriteLine();
        foreach (Product product in GetProducts())
        {
            product.PrintProductInfo();
        }
        Console.WriteLine();
    }
    
    //Prints all orders of logged in customer
    public void PrintOrders()
    {
        _currentWebShopState.CurrentCustomer!.PrintOrders();
    }
    
    //Prints all properties/info of logged in customer
    public void PrintInfo()
    {
        _currentWebShopState.CurrentCustomer!.PrintCustomerInfo();
    }
    
    //Adds funds to logged in customer if input is a number.If so, it checks if the number is positive.
    //If it is then it adds funds to logged in customers account and updates the database
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
            Db.UpdateIntegerColumn("Customers", "Funds", GetCurrentCustomer()!.Funds, GetCurrentCustomer().Id);
            Message($"{amount} added to your profile.");
            return;
        }
        Message("Please write a number next time.");
    }
    
    //Gets logged in customer
    public Customer? GetCurrentCustomer()
    {
        return _currentWebShopState.CurrentCustomer;
    }
    
    //Sets the current choice
    public void SetCurrentChoice(int position)
    {
        _currentWebShopState.CurrentChoice = position;
    }
    
    //Sets the current choice
    public int GetCurrentChoice()
    {
        return _currentWebShopState.CurrentChoice;
    }
    
    //adds/registers new customer 
    public void RegisterCustomer()
    { 
        _currentWebShopState.RegisterCustomer();
        ChangeState(new MainState(this));
    }
    
    //Change current context/state
    public void ChangeState(MenuTemplate menu)
    {
        _currentMenuState = menu;
    }

    public void Message(string message)
    {
        Console.WriteLine($"\n{message}\n");
    }

    //get logged in property
    public bool GetLoggedInStatus()
    {
        return _currentWebShopState.IsLoggedIn;
    }

    //executes current command
    private void OnOk()
    {
        _currentMenuState.Ok();
        SetCurrentChoice(1);
    }

    //Executes current back method
    private void OnBack()
    {
        _currentMenuState.Back();
    }
    
    //Executes current showmenu method
    private void ShowMenu()
    {
        _currentMenuState.ShowMenu();
        CustomerCheck();
    }
    
    //Checks if a customer is logged in to print what customer is logged in at the moment
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
    
    //Checks if parameter equals a key in the commands dictionary. If so, it executes the method connected to that key
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
    
    //The method that runs entire application
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
    
    //Executes current quit method
    private void Quit()
    {
        _currentMenuState.Quit();
    }
    
    //Executes Moveleft quit method
    private void MoveLeft()
    {
        _currentMenuState.MoveLeft();
    }

    //Executes current Moveright method
    private void MoveRight()
    {
        _currentMenuState.MoveRight();
    }
}