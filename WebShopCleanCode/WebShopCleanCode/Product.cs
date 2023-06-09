﻿namespace WebShopCleanCode
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int NrInStock { get; set; }
        public Product(string name, int price, int nrInStock)
        {
            Name = name;
            Price = price;
            NrInStock = nrInStock;
        }
        
        //Check if a product is in stock
        public bool InStock()
        {
            return NrInStock > 0;
        }
        
        //prints all properties of a product object
        public void PrintProductInfo()
        {
            Console.WriteLine(Name + ": " + Price + "kr, " + NrInStock + " in stock.");
        }
    }
}
