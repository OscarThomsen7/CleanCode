﻿namespace WebShopCleanCode
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int NrInStock { get; set; }
        public Product(string name, int price, int nrInStock)// int id,
        {
            //Id = id;
            Name = name;
            Price = price;
            NrInStock = nrInStock;
        }
        public bool InStock()
        {
            return NrInStock > 0;
        }
        public void PrintInfo()
        {
            Console.WriteLine(Name + ": " + Price + "kr, " + NrInStock + " in stock.");
        }
    }
}
