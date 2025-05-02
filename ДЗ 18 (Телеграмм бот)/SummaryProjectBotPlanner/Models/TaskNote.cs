using LinqToDB.Mapping;

namespace SummaryProjectBotPlanner.Models
{
    [Table(Name = "task")]
    public class TaskNote
    {
        [PrimaryKey, Identity]
        [Column(Name = "id")]
        public int ID { get; set; }

        [Column(Name = "description")]
        public string? Description { get; set; }

        [Column(Name = "category")]
        public Category Category { get; set; }
    }
}
