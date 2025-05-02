using LinqToDB.Data;
using LinqToDB;
using SummaryProjectBotPlanner.Models;

namespace SummaryProjectBotPlanner.Repositories
{
    public class LinqRepository
    {
        DataConnection db = new DataConnection(ProviderName.PostgreSQL, Config.SqlConnectionString);

        public List<TaskNote> GetTaskNotesByPattern(string pattern)
        {
            return db.GetTable<TaskNote>()
                .Where(x => x.Description!.Contains(pattern))
                .OrderByDescending(x => x.ID)
                .ToList();
        }
        public List<TaskNote> GetTaskNotesByCategory(Category taskCategory)
        {
            return db.GetTable<TaskNote>()
                .Where(x => x.Category == taskCategory)
                .OrderBy(x => x.ID)
                .ToList();
        }
        public List<string> GetTaskDescriptionsByCategory(Category? taskCategory)
        {
            return db.GetTable<TaskNote>()
                .Where(x => x.Category == taskCategory)
                .OrderBy(x => x.ID)
                .Select(x => x.Description)
                .ToList()!;
        }
        public async Task InsertTaskNote(string taskDescription, Category taskCategory = Category.Somewhen)
        {
            var res = db.GetTable<TaskNote>()
                .Value(x => x.Description, taskDescription)
                .Value(x => x.Category, taskCategory)
                .Insert();

            Console.WriteLine($"Добавлена задача '{taskDescription}' в раздел '{taskCategory}'");
            await Task.CompletedTask;
        }
        public int CountTaskNotesByCategory(Category taskCategory)
        {
            return db.GetTable<TaskNote>()
                .Where(x => x.Category == taskCategory)
                .Count();
        }
        public async Task UpdateTaskNote(int taskId, Category? toCategory)
        {
            var res = db.GetTable<TaskNote>()
                .Where(x => x.ID == taskId)
                .Set(x => x.Category, toCategory)
                .Update();

            Console.WriteLine($"Задача taskId:'{taskId}' перенесена в раздел '{toCategory}'");
            await Task.CompletedTask;
        }
    }
}
