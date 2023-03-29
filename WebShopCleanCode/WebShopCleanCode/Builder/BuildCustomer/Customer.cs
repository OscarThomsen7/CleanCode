namespace WebShopCleanCode.Builder.BuildCustomer;

public class Customer
{
    public string Username { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public int Age { get; }
    public string Address { get; }
    public string PhoneNumber { get; }
    public int Funds { get; set; }
    public List<Order> Orders { get; }
    public Customer(string username, string password, string firstName, string lastName, string email, int age, string address, string phoneNumber)
    {
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Age = age;
        Address = address;
        PhoneNumber = phoneNumber;
        Orders = new List<Order>();
        Funds = 0;
    }

    public bool CanAfford(int price)
    {
        return Funds >= price;
    }
        
    private void LogProperty<T>(string text, T property)
    {
        if (!property!.Equals("") && !property.Equals(-1))
        {
            Console.Write($"{text} {property}");
        }
    }
        
    public void PrintInfo()
    {
        Console.WriteLine();
        LogProperty("Username:", Username);
        LogProperty(", Password:", Password);
        LogProperty(", First Name:", FirstName);
        LogProperty(", Last Name:", LastName);
        LogProperty(", Email:", Email);
        LogProperty(", Age:", Age);
        LogProperty(", Address:", Address);
        LogProperty(", Phone Number:", PhoneNumber);
        Console.WriteLine(", Funds: " + Funds);
        Console.WriteLine();
    }

    public void PrintOrders()
    {
        Console.WriteLine();
        foreach (Order order in Orders)
        {
            order.PrintInfo();
        }
        Console.WriteLine();
    }
}