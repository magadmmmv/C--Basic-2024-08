using LinqToDB.Mapping;

namespace DZ17.Models
{
    [Table(Name = "customers")]
    public class Customer
    {
        [PrimaryKey]
        [Column(Name = "id")]
        public int ID { get; set; }

        [Column(Name = "firstname")]
        public string? FirstName { get; set; }
        
        [Column(Name = "lastname")]
        public string? LastName { get; set; }

        [Column(Name = "age")]
        public int Age { get; set; }
    }
}
