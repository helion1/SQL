using System;
using System.Data.Entity;
using System.Linq;
using DatabaseFirst;

namespace CRUD
{
    class Program
    {

        static void Main(string[] args)
        {

            int id = 0;

            //First INSERT
            id = FirstInsert();

            Console.WriteLine($"INSERT Customer id: {id}");

            //Second INSERT
            id = SecondInsert();

            Console.WriteLine($"INSERT Customer id: {id}");

            //DELETE
            DeleteCustomerWithId(id);

            //Third INSERT
            id = ThirdInsert();

            //UPDATE

            ShowOrdersForCustomerWithId(id);

            UpdateOrderForCustomerWithId(id);

            ShowOrdersForCustomerWithId(id);


            //UPDATE with AsNoTracking

            ShowOrdersForCustomerWithId(id);

            UpdateWithAsNoTrackingOrderForCustomerWithId(id);

            ShowOrdersForCustomerWithId(id);

        }

        private static int FirstInsert()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            int i;
            using (var context = new BluewindsEntities())
            {
                var customer = new Customer()
                {
                    City = "Barcelona",
                    Country = "Spain",
                    FirstName = "Dario",
                    LastName = "Griffo",
                    Phone = "123456789"
                };

                context.Customers.Add(customer);

                i = customer.Id;
            }

            return i;
        }

        private static int SecondInsert()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            int id;
            using (var context = new BluewindsEntities())
            {
                var customer = new Customer()
                {
                    City = "Barcelona",
                    Country = "Spain",
                    FirstName = "Dario",
                    LastName = "Griffo",
                    Phone = "123456789"
                };

                context.Customers.Add(customer);

                context.SaveChanges();

                id = customer.Id;
            }

            return id;
        }


        private static int ThirdInsert()
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            int id;
            using (var context = new BluewindsEntities())
            {
                var customer = new Customer()
                {
                    City = "Barcelona",
                    Country = "Spain",
                    FirstName = "Dario",
                    LastName = "Griffo",
                    Phone = "123456789"
                };

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    Customer = customer,
                    TotalAmount = 100,
                    OrderNumber = "123456"
                };

                var item = new OrderItem()
                {
                    Product = context.Products.First(x => x.Id == 1),
                    Order = order,
                    Quantity = 10,
                    UnitPrice = 50
                };

                order.OrderItems.Add(item);

                customer.Orders.Add(order);

                context.Customers.Add(customer);

                Console.WriteLine($"INSERT WITH ORDER AND ITEMS Customer id: {customer.Id} + Order id: {order.Id}");

                context.SaveChanges();

                id = customer.Id;
            }

            return id;
        }

        private static void DeleteCustomerWithId(int id)
        {
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            using (var context = new BluewindsEntities())
            {
                var customer = context.Customers.First(x => x.Id == id);
                Console.WriteLine($"DELETE Customer id: {customer.Id}");
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }




        private static void ShowOrdersForCustomerWithId(int id)
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
                    .FirstOrDefault(x => x.Id == id);

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

        private static void UpdateOrderForCustomerWithId(int id)
        {
            
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            using (var context = new BluewindsEntities())
            {
                var orders = context.Orders.Where(x => x.CustomerId == id).ToList();

                foreach (var order in orders)
                {
                    order.TotalAmount = order.TotalAmount * (decimal)0.75;
                }

                var savedOrdersCount = context.SaveChanges();
                Console.WriteLine($"UPDATED Orders count: {savedOrdersCount}");
            }
        }

        private static void UpdateWithAsNoTrackingOrderForCustomerWithId(int id)
        {
            
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("==================\n");

            using (var context = new BluewindsEntities())
            {
                var orders = context.Orders.AsNoTracking().Where(x => x.CustomerId == id).ToList();

                foreach (var order in orders)
                {
                    order.TotalAmount = order.TotalAmount * (decimal)0.75;
                }

                var savedOrdersCount = context.SaveChanges();
                Console.WriteLine($"UPDATED Orders count: {savedOrdersCount}");
            }
        }
    }
}
