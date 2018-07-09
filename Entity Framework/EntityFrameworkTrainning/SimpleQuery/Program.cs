using System;
using System.Linq;
using DatabaseFirst;

namespace SimpleQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var from = DateTime.Parse("2012-01-01");
            var to = from.AddYears(1);
            using (var context = new BluewindsEntities())
            {
                
                var lastWeekOrders = context
                    .Orders
                    .Where(x => x.OrderDate >= from && x.OrderDate < to)
                    .ToList();

                foreach (var order in lastWeekOrders)
                {
                    Console.WriteLine(order.Id);
                }
            }


            using (var context = new BluewindsEntities())
            {

                var lastWeekOrders = context
                    .Orders
                    .Where(x => x.OrderDate >= from && x.OrderDate < to)
                    .GroupBy(x => x.OrderDate.Month)
                    .ToList();

                foreach (var monthlyOrders in lastWeekOrders)
                {
                    Console.WriteLine($"Mes: {monthlyOrders.Key}: Total; {monthlyOrders.Count()}");
                    foreach (var order in monthlyOrders)
                    {
                        Console.WriteLine(order.Id);
                    }
                }
            }


            using (var context = new BluewindsEntities())
            {

                var lastWeekOrders = context
                    .Orders
                    .Where(x => x.CustomerId == 2)
                    .OrderBy(x=>x.TotalAmount)
                    .ToList();

                foreach (var order in lastWeekOrders)
                {
                    Console.WriteLine($"Date: {order.OrderDate} + Amount: {order.TotalAmount}");
                }
            }
        }
    }
}
