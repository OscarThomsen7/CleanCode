﻿using WebShopCleanCode.Builder.BuildCustomer;
using System.IO;
using System.Data.SQLite;

namespace WebShopCleanCode
{
    public class Database
    {

        string _dbFilePath = "Database.sqlite";
        string _customers = "Customers";
        string _orders = "Orders";
        // We just pretend this accesses a real database.
        private List<Product> productsInDatabase;
        private List<Customer?> customersInDatabase;
        public Database()
        {
            productsInDatabase = new List<Product>();
            productsInDatabase.Add(new Product("Mirror", 300, 2));
            productsInDatabase.Add(new Product("Car", 2000000, 2));
            productsInDatabase.Add(new Product("Candle", 50, 2));
            productsInDatabase.Add(new Product("Computer", 100000, 2));
            productsInDatabase.Add(new Product("Game", 599, 2));
            productsInDatabase.Add(new Product("Painting", 399, 2));
            productsInDatabase.Add(new Product("Chair", 500, 2));
            productsInDatabase.Add(new Product("Table", 1000, 2));
            productsInDatabase.Add(new Product("Bed", 20000, 2));

            customersInDatabase = new List<Customer?>();
            customersInDatabase.Add(new Customer("jimmy", "jimisthebest", "Jimmy", "Jamesson", "jj@mail.com", 22, "Big Street 5", "123456789"));
            customersInDatabase.Add(new Customer("jake", "jake123", "Jake", "", "", 0, "", ""));
        }

        public void CreateDatabase()
        {
            if (!File.Exists(_dbFilePath))
            {
                SQLiteConnection.CreateFile(_dbFilePath);
                SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
                connection.Open();
                CreateOrdersTable(connection);
                CreateCustomersTable(connection);
                CreateProductsTable(connection);
                connection.Close();
            }
        }
        
        private void CreateProductsTable(SQLiteConnection connection)
        {
            string createTableSql = "CREATE TABLE Products (Id INTEGER PRIMARY KEY, Name TEXT, Price INTEGER, NumberInStock INTEGER," +
                                    "FOREIGN KEY (Id) REFERENCES " + _orders + "(ProductId))";
            SQLiteCommand createTableCmd = new SQLiteCommand(createTableSql, connection);
            createTableCmd.ExecuteNonQuery();
        }

        private void CreateCustomersTable(SQLiteConnection connection)
        {
            string createTableSql = "CREATE TABLE Customers (Id INTEGER PRIMARY KEY, Username TEXT, Password TEXT, Firstname TEXT" +
                                    ", Lastname TEXT, Email TEXT, Age INTEGER, Address TEXT, Phonenumber TEXT, Funds INTEGER" +
                                    ", FOREIGN KEY (Id) REFERENCES " + _orders +"(CustomerId))";
            SQLiteCommand createTableCmd = new SQLiteCommand(createTableSql, connection);
            createTableCmd.ExecuteNonQuery();
        }

        private void CreateOrdersTable(SQLiteConnection connection)
        {
            string createTableSql = "CREATE TABLE Orders (Id INTEGER PRIMARY KEY, CustomerId INTEGER, ProductId INTEGER, ProductName TEXT, Price INTEGER, PurchaseTime DATE)";
            SQLiteCommand createTableCmd = new SQLiteCommand(createTableSql, connection);
            createTableCmd.ExecuteNonQuery();
        }

        public void InsertProduct(Product product)
        {
            string products = "Products";
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            string insertOrdersDataSql = "INSERT INTO " + products +"(Name, Price, NumberInStock) " +
                                         "VALUES (@Name, @Price, @NumberInStock)";
            SQLiteCommand insertOrdersDataCmd = new SQLiteCommand(insertOrdersDataSql, connection);
            insertOrdersDataCmd.Parameters.AddWithValue("@Name", product.Name);
            insertOrdersDataCmd.Parameters.AddWithValue("@Price", product.Price);
            insertOrdersDataCmd.Parameters.AddWithValue("@NumberInStock", product.NrInStock);
            insertOrdersDataCmd.ExecuteNonQuery();
            connection.Close();
        }
        
        public void InsertCustomer(Customer customer)
        {
            string customers = "Customers";
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            string insertOrdersDataSql = "INSERT INTO " + customers +"(Username, Password, Firstname, Lastname, Email, Age, Address, Phonenumber, Funds)" +
                                         " VALUES (@Username, @Password, @Firstname, @Lastname, @Email, @Age, @Address, @Phonenumber, @Funds)";
            SQLiteCommand insertOrdersDataCmd = new SQLiteCommand(insertOrdersDataSql, connection);
            insertOrdersDataCmd.Parameters.AddWithValue("@Username", customer.Username);
            insertOrdersDataCmd.Parameters.AddWithValue("@Password", customer.Password);
            insertOrdersDataCmd.Parameters.AddWithValue("@Firstname", customer.FirstName);
            insertOrdersDataCmd.Parameters.AddWithValue("@Lastname", customer.LastName);
            insertOrdersDataCmd.Parameters.AddWithValue("@Email", customer.Email);
            insertOrdersDataCmd.Parameters.AddWithValue("@Age", customer.Age);
            insertOrdersDataCmd.Parameters.AddWithValue("@Address", customer.Address);
            insertOrdersDataCmd.Parameters.AddWithValue("@Phonenumber", customer.PhoneNumber);
            insertOrdersDataCmd.Parameters.AddWithValue("@Funds", customer.Funds);
            insertOrdersDataCmd.ExecuteNonQuery();
            customer.Id = (int)connection.LastInsertRowId;
            connection.Close();
        }
        
        
        public void InsertOrder(Order order, Customer customer, Product product)
        {
            string orders = "Orders";
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            string insertOrdersDataSql = "INSERT INTO " + orders +"(CustomerId, ProductId, ProductName, Price, PurchaseTime)" +
                                         " VALUES (@CustomerId, @ProductId, @Price, @PurchaseTime)";
            SQLiteCommand insertOrdersDataCmd = new SQLiteCommand(insertOrdersDataSql, connection);
            insertOrdersDataCmd.Parameters.AddWithValue("@CustomerId", customer.Id);
            insertOrdersDataCmd.Parameters.AddWithValue("@ProductId", product.Id);
            insertOrdersDataCmd.Parameters.AddWithValue("@Price", order.BoughtFor);
            insertOrdersDataCmd.Parameters.AddWithValue("@PurchaseTime", order.PurchaseTime);
            insertOrdersDataCmd.ExecuteNonQuery();
            connection.Close();
        }


        public void GetCustomersFromDb()
        {
            CustomerBuilder customerBuilder = new CustomerBuilder();
            Customer customer = new Customer();
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM " + _customers;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                customerBuilder.SetId(reader.GetInt32(0));
                customerBuilder.SetUsername(reader.GetString(1));
                customerBuilder.SetPassword(reader.GetString(2));
                customerBuilder.SetFirstName(reader.GetString(3));
                customerBuilder.SetLastName(reader.GetString(4));
                customerBuilder.SetEmail(reader.GetString(5));
                customerBuilder.SetAge(reader.GetInt32(6).ToString());
                customerBuilder.SetAddress(reader.GetString(7));
                customerBuilder.SetPhoneNumber(reader.GetString(8));
                customerBuilder.SetFunds(reader.GetInt32(9));
                customer = customerBuilder.Build();
                customersInDatabase.Add(customer);
            }
        }

        public List<Product> GetProducts()
        {
            return productsInDatabase;
        }

        public List<Customer?> GetCustomers()
        {
            return customersInDatabase;
        }
    }
}
