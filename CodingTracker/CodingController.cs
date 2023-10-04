using System.Configuration;

namespace CodingTracker;

internal class CodingController
{
    private static readonly string? ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
    DatabaseManager dbManager = new();


    public static void AddRecord(string date, string duration)
    {
        var coding = new Coding { Date = date, Duration = duration };
        DatabaseManager.AddRecord(connectionString: ConnectionString, coding);
    }
}