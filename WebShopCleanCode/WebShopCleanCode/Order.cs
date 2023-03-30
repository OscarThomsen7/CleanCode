namespace WebShopCleanCode
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BoughtFor { get; set; }
        public DateTime PurchaseTime { get; set; }
        public Order(string name, int boughtFor, DateTime purchaseTime)//int id, 
        {
            //Id = id;
            Name = name;
            BoughtFor = boughtFor;
            PurchaseTime = purchaseTime;
        }
        public void PrintOrderInfo()
        {
            Console.WriteLine(Name + ", bought for " + BoughtFor + "kr, time: " + PurchaseTime + ".");
        }
    }
}
