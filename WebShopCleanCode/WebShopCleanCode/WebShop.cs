using WebShopCleanCode.Builder.BuildCustomer;

namespace WebShopCleanCode;

public class WebShop
{
    private readonly Database _database = new();
    private readonly CustomerBuilder _customerBuilder = new();
    public List<Product> Products { get;}
    public List<Customer?> Customers { get;}
    public Customer? CurrentCustomer { get; set; }
    public bool IsLoggedIn { get; set; }
    public int CurrentChoice { get; set; } = 1;
    public string? Username { get; set; } = "";
    public string? Password { get; set; } = "";
    private delegate string ExecuteMethod();

    private ExecuteMethod _executeMethod;
    
    private readonly Dictionary<string, ExecuteMethod> _inputDictionary = new();

    public WebShop()
    {
        Products = _database.GetProducts();
        Customers = _database.GetCustomers();
        _database.CreateDatabase();
    }
    
    

    public void RegisterCustomer()
    {
        Console.WriteLine("Please write your username.");
        var input = Console.ReadLine()!;
        if (Customers.Any(customer => customer!.Username.Equals(input)))
        {
            Console.WriteLine("\nUsername already exists.\n");
            return;
        }
        _customerBuilder.SetUsername(input);
        _customerBuilder.SetPassword(RegisterProperty("a password", "password.", "Please actually write something."));
        _customerBuilder.SetFirstName(RegisterProperty("a first name", "first name.", "Please actually write something."));
        _customerBuilder.SetLastName(RegisterProperty("a last name", "last name.", "Please actually write something."));
        _customerBuilder.SetEmail(RegisterProperty("an email", "email.", "Please actually write something."));
        _customerBuilder.SetAge(RegisterProperty("an age", "age.", "Please write a number."));
        _customerBuilder.SetAddress(RegisterProperty("an address", "address.", "Please actually write something."));
        _customerBuilder.SetPhoneNumber(RegisterProperty("a phone number", "phone number.", "Please actually write something."));
        Customer newCustomer = _customerBuilder.Build();
        
        Customers.Add(newCustomer);
        CurrentCustomer = newCustomer;
        IsLoggedIn = true;
        Console.WriteLine($"\n{newCustomer!.Username} successfully added and is now logged in.\n");
    }
    private void SetDictionary(string instruction, string message)
    {
        _inputDictionary.Add("y", () => OnYes(instruction, message));
        _inputDictionary.Add("n", () => "");
    }

    private string RegisterProperty(string question, string instruction, string message)
    {
        if (_inputDictionary.Count == 0)
            SetDictionary(instruction, message);
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