using System.Configuration;

namespace CodingTracker;

internal static class Program
{
    private static readonly string? ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

    private static void Main(string[] args)
    {
        GetUserInput getUserInput = new();

        DatabaseManager.CreateTable(ConnectionString);
        getUserInput.MainMenu();
    }
}