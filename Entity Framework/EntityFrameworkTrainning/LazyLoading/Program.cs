using System;
using System.Data.Entity;
using System.Linq;
using DatabaseFirst;

namespace LazyLoading
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lazy Loading
            QueryWithLazyLoadingEnabled();
            Console.WriteLine();

            //LazyLoading disabled
            QueryWithLazyLoadingDisabled();
            Console.WriteLine();

            //Lazy loading disabled with includes
            QueryWithLazyLoadingDisabledWithIncludes();
            Console.WriteLine();

            //SELECT inner entity
            SelectProperty();
        }

        private static void SelectProperty()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            using (var context = new BluewindsEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var orders = context
                    .Customers
                    .Where(x => x.FirstName == "Ana" && x.LastName == "Trujillo")
                    .SelectMany(x => x.Orders);

                foreach (var order in orders)
                {
                    Console.WriteLine($"Date: {order.OrderDate} + Amount: {order.TotalAmount}");
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"Item: {item.Product.ProductName} + Amount: {item.Quantity}");
                    }
                }
            }
        }

        private static void QueryWithLazyLoadingDisabledWithIncludes()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            using (var context = new BluewindsEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var customer = context
                    .Customers
                    .Include(c => c.Orders)
                    .Include(c => c.Orders.Select(o => o.OrderItems.Select(oi => oi.Product)))
                    .FirstOrDefault(x => x.FirstName == "Ana" && x.LastName == "Trujillo");

                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"Date: {order.OrderDate} + Amount: {order.TotalAmount}");
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"Item: {item.Product.ProductName} + Amount: {item.Quantity}");
                    }
                }
            }
        }

        private static void QueryWithLazyLoadingDisabled()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            using (var context = new BluewindsEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var customer = context
                    .Customers
                    .FirstOrDefault(x => x.FirstName == "Ana" && x.LastName == "Trujillo");

                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"Date: {order.OrderDate} + Amount: {order.TotalAmount}");
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"Item: {item.Product.ProductName} + Amount: {item.Quantity}");
                    }
                }
            }
        }

        private static void QueryWithLazyLoadingEnabled()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            using (var context = new BluewindsEntities())
            {
                var customer = context
                    .Customers
                    .FirstOrDefault(x => x.FirstName == "Ana" && x.LastName == "Trujillo");

                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"Date: {order.OrderDate} + Amount: {order.TotalAmount}");
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"Item: {item.Product.ProductName} + Amount: {item.Quantity}");
                    }
                }
            }
        }
    }
}
