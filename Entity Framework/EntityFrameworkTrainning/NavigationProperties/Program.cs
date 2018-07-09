using System;
using System.Linq;
using DatabaseFirst;

namespace NavigationProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BluewindsEntities())
            {

                var lastWeekOrders = context
                    .Orders
                    .Where(x => x.Customer.FirstName == "Ana" && x.Customer.LastName == "Trujillo")
                    .OrderBy(x => x.TotalAmount)
                    .ToList();

                foreach (var order in lastWeekOrders)
                {
                    Console.WriteLine($"Date: {order.OrderDate} + Amount: {order.TotalAmount}");
                }
            }
        }
    }
}
