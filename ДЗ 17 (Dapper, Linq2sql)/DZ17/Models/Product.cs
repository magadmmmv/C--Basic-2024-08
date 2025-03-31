using LinqToDB.Mapping;

namespace DZ17.Models
{
    [Table(Name = "products")]
    class Product
    {
        [PrimaryKey]
        [Column(Name = "id")]
        public int ID { get; set; }

        [Column(Name = "name")]
        public string? Name { get; set; }

        [Column(Name = "description")]
        public string? Description { get; set; }

        [Column(Name = "stockquantity")]
        public int StockQuantity { get; set; }

        [Column(Name = "price")]
        public int Price { get; set; }
    }
}
