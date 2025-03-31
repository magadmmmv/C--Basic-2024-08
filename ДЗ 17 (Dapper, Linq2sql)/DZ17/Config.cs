namespace DZ17
{
    public static class Config
    {
        // replace Password below, or set your Password in Project Properties > Debug > Launch profiles UI > Environment variables
        private static string _password = Environment.GetEnvironmentVariable("Password") ?? "YOUR_PG_PASSWORD";
        public static string SqlConnectionString = "User ID=postgres;Password=" + _password + ";Host=localhost;Port=5432;Database=Shop";
    }
}
