using System.Configuration;

namespace CodingTracker;

internal static class Program
{
    private static readonly string? ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

    private static void Main(string[] args)
    {
        DatabaseManager dbManager = new();
        GetUserInput getUserInput = new();
        
        dbManager.CreateTable(ConnectionString);
        getUserInput.MainMenu();
    }
}