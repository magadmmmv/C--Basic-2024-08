using LinqToDB.Mapping;

namespace SummaryProjectBotPlanner.Models
{
    public enum Category
    {
        [MapValue("Today")]
        Today,

        [MapValue("Somewhen")]
        Somewhen,

        [MapValue("Done")]
        Done
    }
}
