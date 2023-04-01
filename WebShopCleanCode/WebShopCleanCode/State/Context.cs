using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.Builder.BuildMenu;
using WebShopCleanCode.Command;
using WebShopCleanCode.State.StateOptions;
using WebShopCleanCode.State.States;

namespace WebShopCleanCode.State;

public class Context
{
    //This class is what runs the whole application.
    //different methods will be executed depending oon what state/menu the application is set to.
    private MenuTemplate _currentMenuState;
    private readonly Dictionary<string, CommandExecutor> _commandDictionary = new();
    private MenuDirector _menuDirector = new();
    public Database Database { get;} = new();
    private readonly CustomerBuilder _customerBuilder = new();
    public List<Product> Products { get;}
    public List<Customer?> Customers { get;}
    public Customer? CurrentCustomer { get; set; }
    public bool IsLoggedIn { get; set; }
    public int CurrentChoice { get; set; } = 1;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    private delegate string ExecuteMethod();
    private ExecuteMethod _executeMethod;
    private readonly Dictionary<string, ExecuteMethod> _inputDictionary = new();

    public Context()
    {
        _currentMenuState = new MenuState(this, new MainOptions(this),_menuDirector.BuildMainMenu(IsLoggedIn));
        Products = Database.GetProducts();
        Customers = Database.GetCustomers();
        SetCommands();
    }
    
    //Create a dictionary of commands to get rid of the need for nested ifs or switch cases when taking user input.
    private void SetCommands()
    {
        _commandDictionary.Add("left", new CommandExecutor(MoveLeft));
        _commandDictionary.Add("l", new CommandExecutor(MoveLeft));
        _commandDictionary.Add("right", new CommandExecutor(MoveRight));
        _commandDictionary.Add("r", new CommandExecutor(MoveRight));
        _commandDictionary.Add("ok", new CommandExecutor(OnOk));
        _commandDictionary.Add("o", new CommandExecutor(OnOk));
        _commandDictionary.Add("k", new CommandExecutor(OnOk));
        _commandDictionary.Add("back", new CommandExecutor(OnBack));
        _commandDictionary.Add("b", new CommandExecutor(OnBack));
        _commandDictionary.Add("q", new CommandExecutor(Quit));
        _commandDictionary.Add("quit", new CommandExecutor(Quit));
    }
    
    //Logs customer out and resets login properties
    public void LogOut()
    {
        CurrentCustomer = null;
        IsLoggedIn = false;
        Password = "";
        Username = "";
        ChangeState(new MenuState(this, new MainOptions(this), _menuDirector.BuildMainMenu(IsLoggedIn)));    }
    
    //Sets username property to be used for login attempt
    public void SetUserName()
    {
        Console.WriteLine("\nA keyboard appears.\nPlease input your username.");
        Username = Console.ReadLine();
    }
    
    //Sets password property to be used for login attempt 
    public void SetPassWord()
    {
        Console.WriteLine("\nA keyboard appears.\nPlease input your password.");
        Password = Console.ReadLine();
    }
    
    //sets the current customer to a customer object
    public void SetCurrentCustomer(Customer? customer)
    {
        CurrentCustomer = customer;
        IsLoggedIn = true;
    }
    
    //Prints all products for the purchase menu
    public void OutputProducts()
    {
        int spotInList = 1;
        foreach (var product in Products)
        {
            Console.WriteLine($"{spotInList}: {product.Name} , {product.Price} kr");
            spotInList++;
        }
        Console.WriteLine("Your funds: " + CurrentCustomer.Funds);
    }
    
    //Prints all products for wares menu
    public void PrintProducts()
    {
        Console.WriteLine();
        foreach (Product product in Products)
        {
            product.PrintProductInfo();
        }
        Console.WriteLine();
    }
    
    //Prints all orders of logged in customer
    public void PrintOrders()
    {
        CurrentCustomer.PrintOrders();
    }
    
    //Prints all properties/info of logged in customer
    public void PrintInfo()
    {
       CurrentCustomer.PrintCustomerInfo();
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
            CurrentCustomer.Funds += amount;
            Database.UpdateIntegerColumn("Customers", "Funds", CurrentCustomer.Funds, CurrentCustomer.Id);
            Message($"{amount} added to your profile.");
            return;
        }
        Message("Please write a number next time.");
    }
    
    //Sets the current choice
    public void SetCurrentChoice(int position)
    {
        CurrentChoice = position;
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

    //executes current command and resets the choice cursor to 1
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
        if (IsLoggedIn)
        {
            Console.WriteLine($"Current user: {CurrentCustomer!.Username}");
            return;
        }
        Console.WriteLine("Nobody logged in.");
        
    }
    
    //Checks if parameter equals a key in the commands dictionary. If so, it executes the method connected to that key
    private void MoveInMenu(string choice)
    {
        foreach (var item in _commandDictionary)
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
            var choice = Console.ReadLine();
            MoveInMenu(choice);
        }
    }
    
    //Executes current quit method
    private void Quit()
    {
        _currentMenuState.Quit();
    }
    
    //Executes current Moveleft  method
    private void MoveLeft()
    {
        _currentMenuState.MoveLeft();
    }

    //Executes current Moveright method
    private void MoveRight()
    {
        _currentMenuState.MoveRight();
    }
    
    //Adds/registers a new customer using the CustomerBuilder Class and logs that new customer in.
    //Also sends customer to database and resets the menu to main menu.
    public void RegisterCustomer()
    {
        string message = "Please actually write something.";
        
        Console.WriteLine("Please write your username.");
        var input = Console.ReadLine()!;
        if (Customers.Any(customer => customer!.Username.Equals(input)))
        {
            Console.WriteLine("\nUsername already exists.\n");
            return;
        }
        _customerBuilder.SetUsername(input);
        _customerBuilder.SetPassword(RegisterProperty("a password", "password.", message));
        _customerBuilder.SetFirstName(RegisterProperty("a first name", "first name.", message));
        _customerBuilder.SetLastName(RegisterProperty("a last name", "last name.", message));
        _customerBuilder.SetEmail(RegisterProperty("an email", "email.", message));
        _customerBuilder.SetAge(RegisterProperty("an age", "age.", "Please write a number."));
        _customerBuilder.SetAddress(RegisterProperty("an address", "address.", message));
        _customerBuilder.SetPhoneNumber(RegisterProperty("a phone number", "phone number.", message));
        Customer newCustomer = _customerBuilder.Build();
        
        Customers.Add(newCustomer);
        CurrentCustomer = newCustomer;
        IsLoggedIn = true;
        Database.InsertCustomer(newCustomer);
        Console.WriteLine($"\n{CurrentCustomer.Username} successfully added and is now logged in.\n");
        ChangeState(new MenuState(this, new MainOptions(this), _menuDirector.BuildMainMenu(IsLoggedIn)));    }
    
    //Dictionary to execute a delegate if the input matches the key.
    private void SetRegisterDictionary(string instruction, string message)
    {
        _inputDictionary.Add("y", () => OnYes(instruction, message));
        _inputDictionary.Add("n", () => "");
    }
    
    //Asks user if they want to add each property or not. Loops through inputDictionary to check if input equals a key.
    //Executes the delegate if it matches.
    private string RegisterProperty(string question, string instruction, string message)
    {
        if (_inputDictionary.Count == 0)
            SetRegisterDictionary(instruction, message);
        _inputDictionary["y"] = () => OnYes(instruction, message);
        
        while (true)
        {
            Console.WriteLine($"Do you want {question}? y/n.");
            var choice = Console.ReadLine()!;

            foreach (var item in _inputDictionary.Where(item => choice.ToLower().Equals(item.Key.ToLower())))
            {
                _executeMethod = item.Value;
                return _executeMethod();
            }
            Console.WriteLine("\ny or n, please.\n");
        }
    }
    
    //if user says yes in registerproperty it takes a new input that becomes the property value.
    private string OnYes(string instruction, string message)
    {
        while (true)
        {
            Console.WriteLine($"Please write your {instruction}");
            var input = Console.ReadLine()!;
            if (!input.Equals("")) return input;
            Console.WriteLine($"\n{message}\n");
        }   
    }
}