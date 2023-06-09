﻿using WebShopCleanCode.Builder.BuildCustomer;
using WebShopCleanCode.Command;

namespace WebShopCleanCode.State.StateOptions;

public class PurchaseOptions : IOption
{
    //This class is the purchase menus options
    private Context _context;

    public PurchaseOptions(Context context)
    {
        _context = context;
    }
    
    //Checks if the chosen product is in stock and if current customer can afford to buy it. If not, it does not let the customer buy it.
    //Otherwise it lets the customer buy, decrements customer funds and stock of the product and updates the database.
    //Then creates a new order with all the info collected and sends it to the db and adds it to current customers orders list
    public void Option1()
    {
        Customer currentCustomer = _context.CurrentCustomer;
        Product product = _context.Products[_context.CurrentChoice - 1];
        if (product.InStock())
        {
            if (currentCustomer.CanAfford(product.Price))
            {
                currentCustomer.Funds -= product.Price;
                _context.ProxyDatabase.UpdateIntegerColumn("Customers", "Funds", currentCustomer.Funds, currentCustomer.Id);
                product.NrInStock -= 1;
                
                _context.ProxyDatabase.UpdateIntegerColumn("Products", "NumberInStock", product.NrInStock, product.Id);
                Order order = new Order(currentCustomer.Id, product.Id, product.Name, product.Price, DateTime.Now);
                currentCustomer.Orders.Add(order);
                
                _context.ProxyDatabase.InsertOrder(order, currentCustomer, product);
                _context.Message($"Successfully bought {product.Name}");
                return;
            }
            _context.Message("You cannot afford.");
            return;
        }
        _context.Message("Not in stock.");
    }
    
    //Not used
    public void Option2()
    {
    }

    //Not used
    public void Option3()
    {
    }

    //Not used
    public void Option4()
    {
    }

    public List<CommandExecutor> GetOptions()
    {
        return new List<CommandExecutor>
        {
            new(() => Option1())
        };
    }
}