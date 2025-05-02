namespace SummaryProjectBotPlanner
{
    public static class Config
    {
        private static string _password = Environment.GetEnvironmentVariable("Password") ?? "YOUR_PG_PASSWORD";
        public static string SqlConnectionString = "User ID=postgres;Password=" + _password + ";Host=localhost;Port=5432;Database=tasks";
        public static string _botKey = Environment.GetEnvironmentVariable("BOT_TOKEN") ?? "YOUR_BOT_TOKEN";
    }
}
