using DZ17.Models;
using DZ17.Repositories;

namespace DZ17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Print customers with pattern Nick ordered by Age desc:");
            var customers = LinqRepository.GetCustomers("Nick");
            customers.ForEach(PrintCustomers);
            Console.WriteLine($"Count Nick: {LinqRepository.CountCustomers("Nick")}");

            Console.WriteLine();

            Console.WriteLine("Print products ordered by price:");
            var products = LinqRepository.GetProducts();
            products.ForEach(PrintProducts);
            Console.WriteLine($"Count products: {LinqRepository.CountProducts()}");

            Console.WriteLine();

            Console.WriteLine("Print first five orders");
            var orders = LinqRepository.GetOrders(5);
            orders.ForEach(PrintOrders);
            Console.WriteLine($"Count all orders: {LinqRepository.CountOrders()}");
        }

        private static void PrintCustomers(Customer customer)
        {
            Console.WriteLine($"{customer.FirstName} {customer.LastName}; Age: {customer.Age}");
        }
        private static void PrintProducts(Product product)
        {
            Console.WriteLine($"Price: {product.Price} dollars;\tName: {product.Name};\t\tDescription: {product.Description};");
        }
        private static void PrintOrders(Order order)
        {
            Console.WriteLine($"ID: {order.ID}; ProductID: {order.ProductID}; CustomerID: {order.CustomerID}");
        }
    }

    public static class LinqExt
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}
