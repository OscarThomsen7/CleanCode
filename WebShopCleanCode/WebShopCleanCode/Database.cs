using WebShopCleanCode.Builder.BuildCustomer;
using System.IO;
using System.Data.SQLite;

namespace WebShopCleanCode
{
    public class Database
    {
        private string _dbFilePath = "Database.sqlite";
        private string _customers = "Customers";
        private string _orders = "Orders";
        private string _products = "Products";
        private List<Product> _productsInDatabase = new();
        private List<Customer?> _customersInDatabase = new();
        private List<Order> _ordersInDatabase = new();
        public Database()
        {
            CreateDatabase();
            if (_productsInDatabase.Count < 1 && _customersInDatabase.Count < 1)
            {
                InsertProduct(new Product("Mirror", 300, 2));
                InsertProduct(new Product("Car", 2000000, 2));
                InsertProduct(new Product("Candle", 50, 2));
                InsertProduct(new Product("Computer", 100000, 2));
                InsertProduct(new Product("Game", 599, 2));
                InsertProduct(new Product("Painting", 399, 2));
                InsertProduct(new Product("Chair", 500, 2));
                InsertProduct(new Product("Table", 1000, 2));
                InsertProduct(new Product("Bed", 20000, 2));
            
                InsertCustomer(new Customer("jimmy", "jimisthebest", "Jimmy", "Jamesson", "jj@mail.com", 22, "Big Street 5", "123456789"));
                InsertCustomer(new Customer("jake", "jake123", "Jake", "", "", 0, "", ""));
                GetProductsFromDb();
                GetCustomersFromDb();
            }
        }

        
        //If a database file does not exist it creates one and creates all the tables. Also gets all data from the database
        private void CreateDatabase()
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
            GetCustomersFromDb();
            GetProductsFromDb();
            GetOrdersFromDb();
            SetOrdersToCustomers();
        }
        
        //Creates products table
        private void CreateProductsTable(SQLiteConnection connection)
        {
            string createTableSql = "CREATE TABLE Products (Id INTEGER PRIMARY KEY, Name TEXT, Price INTEGER, NumberInStock INTEGER," +
                                    "FOREIGN KEY (Id) REFERENCES " + _orders + "(ProductId))";
            SQLiteCommand createTableCmd = new SQLiteCommand(createTableSql, connection);
            createTableCmd.ExecuteNonQuery();
        }
        
        //Creates customers table
        private void CreateCustomersTable(SQLiteConnection connection)
        {
            string createTableSql = "CREATE TABLE Customers (Id INTEGER PRIMARY KEY, Username TEXT, Password TEXT, Firstname TEXT" +
                                    ", Lastname TEXT, Email TEXT, Age INTEGER, Address TEXT, Phonenumber TEXT, Funds INTEGER" +
                                    ", FOREIGN KEY (Id) REFERENCES " + _orders +"(CustomerId))";
            SQLiteCommand createTableCmd = new SQLiteCommand(createTableSql, connection);
            createTableCmd.ExecuteNonQuery();
        }
        
        //Creates orders table
        private void CreateOrdersTable(SQLiteConnection connection)
        {
            string createTableSql = "CREATE TABLE Orders (Id INTEGER PRIMARY KEY, CustomerId INTEGER, ProductId INTEGER, ProductName TEXT, Price INTEGER, PurchaseTime DATE)";
            SQLiteCommand createTableCmd = new SQLiteCommand(createTableSql, connection);
            createTableCmd.ExecuteNonQuery();
        }
        
        //inserts product
        private void InsertProduct(Product product)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            string insertData = "INSERT INTO " + _products +"(Name, Price, NumberInStock) " +
                                "VALUES (@Name, @Price, @NumberInStock)";
            SQLiteCommand insertProductDataCmd = new SQLiteCommand(insertData, connection);
            insertProductDataCmd.Parameters.AddWithValue("@Name", product.Name);
            insertProductDataCmd.Parameters.AddWithValue("@Price", product.Price);
            insertProductDataCmd.Parameters.AddWithValue("@NumberInStock", product.NrInStock);
            insertProductDataCmd.ExecuteNonQuery();
            product.Id = (int)connection.LastInsertRowId;
            connection.Close();
        }
        
        //inserts customer
        public void InsertCustomer(Customer customer)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            string insertData = "INSERT INTO " + _customers +"(Username, Password, Firstname, Lastname, Email, Age, Address, Phonenumber, Funds)" +
                                " VALUES (@Username, @Password, @Firstname, @Lastname, @Email, @Age, @Address, @Phonenumber, @Funds)";
            SQLiteCommand insertCustomerDataCmd = new SQLiteCommand(insertData, connection);
            insertCustomerDataCmd.Parameters.AddWithValue("@Username", customer.Username);
            insertCustomerDataCmd.Parameters.AddWithValue("@Password", customer.Password);
            insertCustomerDataCmd.Parameters.AddWithValue("@Firstname", customer.FirstName);
            insertCustomerDataCmd.Parameters.AddWithValue("@Lastname", customer.LastName);
            insertCustomerDataCmd.Parameters.AddWithValue("@Email", customer.Email);
            insertCustomerDataCmd.Parameters.AddWithValue("@Age", customer.Age);
            insertCustomerDataCmd.Parameters.AddWithValue("@Address", customer.Address);
            insertCustomerDataCmd.Parameters.AddWithValue("@Phonenumber", customer.PhoneNumber);
            insertCustomerDataCmd.Parameters.AddWithValue("@Funds", customer.Funds);
            insertCustomerDataCmd.ExecuteNonQuery();
            customer.Id = (int)connection.LastInsertRowId;
            connection.Close();
        }
        
        //inserts order
        public void InsertOrder(Order order, Customer customer, Product product)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            string insertData = "INSERT INTO " + _orders +"(CustomerId, ProductId, ProductName, Price, PurchaseTime)" +
                                         " VALUES (@CustomerId, @ProductId, @ProductName, @Price, @PurchaseTime)";
            SQLiteCommand insertOrderDataCmd = new SQLiteCommand(insertData, connection);
            insertOrderDataCmd.Parameters.AddWithValue("@CustomerId", customer.Id);
            insertOrderDataCmd.Parameters.AddWithValue("@ProductId", product.Id);
            insertOrderDataCmd.Parameters.AddWithValue("@ProductName", product.Name);
            insertOrderDataCmd.Parameters.AddWithValue("@Price", order.BoughtFor);
            insertOrderDataCmd.Parameters.AddWithValue("@PurchaseTime", order.PurchaseTime);
            insertOrderDataCmd.ExecuteNonQuery();
            order.Id = (int)connection.LastInsertRowId;
            connection.Close();
        }
        
        //Gets each customer from database and adds it to the customer list 
        private void GetCustomersFromDb()
        {
            string query = $"SELECT COUNT(*) FROM {_customers}";
            using SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            
            connection.Open();
            
            using SQLiteCommand command1 = new SQLiteCommand(query, connection);
            int rowCount = Convert.ToInt32(command1.ExecuteScalar());
            if (rowCount <= 0) return;
            CustomerBuilder customerBuilder = new CustomerBuilder();
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
                _customersInDatabase.Add(customerBuilder.Build());
            }
        }
        
        //Gets each product from database and adds it to the product list 
        private void GetProductsFromDb()
        {
            string query = $"SELECT COUNT(*) FROM {_products}";
            using SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            using SQLiteCommand command1 = new SQLiteCommand(query, connection);
            int rowCount = Convert.ToInt32(command1.ExecuteScalar());
            if (rowCount <= 0) return;
            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM " + _products;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product product = new Product("", 0, 0)
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetInt32(2),
                    NrInStock = reader.GetInt32(3)
                };
                _productsInDatabase.Add(product);
            }
        }
        
        //Gets each order from database and adds it to the order list 
        private void GetOrdersFromDb()
        {
            string query = $"SELECT COUNT(*) FROM {_orders}";
            using SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();
            using SQLiteCommand command1 = new SQLiteCommand(query, connection);
            int rowCount = Convert.ToInt32(command1.ExecuteScalar());
            if (rowCount <= 0) return;
            using var command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM " + _orders;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order(0, 0, "", 0, DateTime.Now)
                {
                    Id = reader.GetInt32(0),
                    CustomerId = reader.GetInt32(1),
                    ProductId = reader.GetInt32(2),
                    Name = reader.GetString(3),
                    BoughtFor = reader.GetInt32(4)
                };
                var dateString = (DateTime)reader["PurchaseTime"];
                order.PurchaseTime = dateString;
                _ordersInDatabase.Add(order);
            }
        }

        //checks every customer id column in the orders table and id column of the customers table to see if they match.
        //If they do that order is added to that customers orders list.
        private void SetOrdersToCustomers()
        {
            foreach (var customer in _customersInDatabase)
            {
                foreach (var order in _ordersInDatabase.Where(order => order.CustomerId == customer.Id))
                {
                    customer.Orders.Add(order);
                }
            }
        }

        
        //Updates a column that has a integer value in any you want table. 
        public void UpdateIntegerColumn(string table, string column, int value, int id)
        {
            var query = "UPDATE " + table + " SET " + column + " = @value WHERE id = @id";

            using SQLiteConnection connection = new SQLiteConnection("Data Source=" + _dbFilePath);
            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@value", value);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        
        //Returns product list
        public List<Product> GetProducts()
        {
            return _productsInDatabase;
        }
        
        //Returns customer list
        public List<Customer?> GetCustomers()
        {
            return _customersInDatabase;
        }
    }
}
