using WebShopCleanCode.State;
namespace WebShopCleanCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new Context();
            context.Run();
        }
    }
}