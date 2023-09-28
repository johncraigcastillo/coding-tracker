using System.Configuration;

namespace CodingTracker;

class Program
{
    static string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];

    static void Main(string[] args)
    {
        DatabaseManager dbManager = new();
        // dbManager.CreateTable(connectionString);
    }
}