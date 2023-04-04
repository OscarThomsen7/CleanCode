using WebShopCleanCode.Builder.BuildCustomer;

namespace WebShopCleanCode.Proxy;

public class ProxyDatabase
{
    //A proxy design pattern class.
    //Used to access the database without being able to change anything in the database class.

    private Database _database;
    public ProxyDatabase()
    {
        _database = new Database();
    }

    public void InsertCustomer(Customer customer)
    {
        _database.InsertCustomer(customer);
    }

    public void InsertOrder(Order order, Customer customer, Product product)
    {
        _database.InsertOrder(order, customer, product);
    }

    public void UpdateIntegerColumn(string table, string column, int value, int id)
    {
        _database.UpdateIntegerColumn(table, column, value, id);
    }
    
    public List<Product> GetProducts()
    {
        return _database.GetProducts();
    }
        
    //Returns customer list
    public List<Customer?> GetCustomers()
    {
        return _database.GetCustomers();
    }
}