using Microsoft.Data.Sqlite;

namespace CodingTracker;

internal class DatabaseManager
{
    internal void CreateTable(string? connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var tableCommand = connection.CreateCommand();
        tableCommand.CommandText =
            """
            CREATE TABLE IF NOT EXISTS coding_tracker (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                date TEXT NOT NULL,
                duration TEXT NOT NULL
            );
            """;
        tableCommand.ExecuteNonQuery();
    }
}