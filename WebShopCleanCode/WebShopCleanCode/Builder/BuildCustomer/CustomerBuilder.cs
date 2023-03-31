namespace WebShopCleanCode.Builder.BuildCustomer;

public class CustomerBuilder
{
    private int _id;
    private string _username;
    private string _password;
    private string _firstName;
    private string _lastName;
    private string _email;
    private int _age = -1;
    private string _address;
    private string _phoneNumber;
    private int _funds;

    
    //This class is used to create a customer in a simpler way, used in WebShop class for example.
    public void SetId(int id)
    {
        _id = id;
    }

    public void SetFunds(int funds)
    {
        _funds = funds;
    }
    
    public void SetUsername(string input)
    {
        _username = input;
    }
    
    public void SetFirstName(string input)
    {
        _firstName = input;
    }
    public void SetLastName(string input)
    {
        _lastName = input;
    }
    public void SetEmail(string input)
    {
        _email = input;
    }
    public void SetAge(string input)
    {
        if (input.Equals("")) return;
        if (int.TryParse(input, out var number))
        {
            _age = number;
            return;
        }
        _age = -1;
    }
    
    public void SetAddress(string input)
    {
        _address = input;
    }
    public void SetPassword(string input)
    {
        _password = input;
    }
    public void SetPhoneNumber(string input)
    {
        _phoneNumber = input;
    }
    public Customer Build()
    {
        return new Customer(_username, _password, _firstName, _lastName, _email, _age, _address, _phoneNumber, _id, _funds);
    }
}