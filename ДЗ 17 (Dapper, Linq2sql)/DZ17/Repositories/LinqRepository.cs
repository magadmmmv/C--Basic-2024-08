using LinqToDB;
using LinqToDB.Data;
using DZ17.Models;

namespace DZ17.Repositories
{
    internal class LinqRepository
    {
        static DataConnection db = new DataConnection(ProviderName.PostgreSQL, Config.SqlConnectionString);

        public static List<Customer> GetCustomers(string pattern)
        {
            return db.GetTable<Customer>()
                .Where(x => x.FirstName!.Contains(pattern))
                .OrderByDescending(x => x.Age)
                .ToList();
        }

        public static int CountCustomers(string pattern)
        {
            return db.GetTable<Customer>()
                .Where(x => x.FirstName!.Contains(pattern))
                .Count();
        }

        public static List<Product> GetProducts()
        {
            return db.GetTable<Product>()
                .OrderBy(x => x.Price)
                .ToList();
        }

        public static int CountProducts()
        {
            return db.GetTable<Product>()
                .Count();
        }

        public static List<Order> GetOrders(int count)
        {
            return db.GetTable<Order>()
                .Take(count)
                .ToList();
        }

        public static int CountOrders()
        {
            return db.GetTable<Order>()
                .Count();
        }
    }
}
