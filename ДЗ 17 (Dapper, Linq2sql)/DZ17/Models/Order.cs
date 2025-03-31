using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace DZ17.Models
{
    [Table(Name = "orders")]
    class Order
    {
        [PrimaryKey]
        [Column(Name = "id")]
        public int ID { get; set; }

        [Column(Name = "customerid")]
        [ForeignKey("orders_customers_id_fkey")]
        public int CustomerID { get; set; }

        [Column(Name= "productid")]
        [ForeignKey("orders_products_id_fkey")]
        public int ProductID { get; set; }

        [Column(Name= "quantity")]
        public int Quantity { get; set; }
    }
}
