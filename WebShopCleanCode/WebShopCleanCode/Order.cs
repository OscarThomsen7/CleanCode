namespace WebShopCleanCode
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int BoughtFor { get; set; }
        public DateTime PurchaseTime { get; set; }
        public Order(int customerId, int productId, string name, int boughtFor, DateTime purchaseTime)
        {
            CustomerId = customerId;
            ProductId = productId;
            Name = name;
            BoughtFor = boughtFor;
            PurchaseTime = purchaseTime;
        }
        
        //Prints all properties of an order object
        public void PrintOrderInfo()
        {
            Console.WriteLine(Name + ", bought for " + BoughtFor + "kr, time: " + PurchaseTime + ".");
        }
    }
}
