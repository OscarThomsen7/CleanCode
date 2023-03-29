namespace WebShopCleanCode.Builder.BuildMenu;

public class MenuDirector
{
    private MenuBuilder _menuBuilder = new MenuBuilder();

    public Menu BuildMainMenu(bool isLoggedIn)//Customer currentCustomer
    {
        _menuBuilder.SetName("Main menu")
            .SetInfo("What would you like to do?")
            .SetAmountofOptions(3)
            .SetOption1("See Wares")
            .SetOption2("Customer Info")
            .SetOption4("");
        if (isLoggedIn == false)
            return _menuBuilder.SetOption3("Login").Build();
        return _menuBuilder.SetOption3("Logout").Build();
    }
    
    
    public Menu BuildWaresMenu(bool isLoggedIn)//Customer currentCustomer
    {
        _menuBuilder.SetName("Wares menu")
            .SetInfo("What would you like to do?")
            .SetAmountofOptions(4)
            .SetOption1("See all wares")
            .SetOption2("Purchase a ware")
            .SetOption3("Sort wares");
        if (isLoggedIn == false)
            return _menuBuilder.SetOption4("Login").Build();
        return _menuBuilder.SetOption4("Logout").Build();
             
    }
    
    
    public Menu BuildCustomerMenu()
    {
        return _menuBuilder.SetName("Customer menu")
            .SetInfo("What would you like to do?")
            .SetAmountofOptions(3)
            .SetOption1("See your orders")
            .SetOption2("Set your info")
            .SetOption3("Add funds")
            .SetOption4("")
            .Build();
    }
    
    public Menu BuildLoginMenu()
    {
        return _menuBuilder.SetName("Login menu")
            .SetInfo("Please submit username and password.")
            .SetAmountofOptions(4)
            .SetOption1("Set Username")
            .SetOption2("Set Password")
            .SetOption3("Login")
            .SetOption4("Register")
            .Build();
    }
    
    public Menu BuildPurchaseMenu(int options)
    {
        return _menuBuilder.SetName("Purchase menu")
            .SetInfo("What would you like to purchase?")
            .SetAmountofOptions(options)
            .Build();
    }
    
    public Menu BuildSortMenu()
    {
        return _menuBuilder.SetName("Sort menu")
            .SetInfo("How would you like to sort them?")
            .SetAmountofOptions(4)
            .SetOption1("Sort by name, descending")
            .SetOption2("Sort by name, ascending")
            .SetOption3("Sort by price, descending")
            .SetOption4("Sort by price, ascending")
            .Build();
    }
}