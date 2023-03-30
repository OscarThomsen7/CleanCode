using WebShopCleanCode;
using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.State;

Database database = new Database();
CustomerBuilder customerBuilder = new CustomerBuilder();

customerBuilder.SetId(0);
customerBuilder.SetUsername("Ogge99");
customerBuilder.SetPassword("Thisismypass");
customerBuilder.SetFirstName("Oscar");
customerBuilder.SetLastName("Thomsen");
customerBuilder.SetEmail("E@gmail.com");
customerBuilder.SetAge("32");
customerBuilder.SetAddress("Revingegatan 5b");
customerBuilder.SetPhoneNumber("0703642048");
customerBuilder.SetFunds(36798);
database.InsertCustomer(customerBuilder.Build());
database.GetCustomersFromDb();
//var context = new Context();
//context.Run();