using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker;

internal class DatabaseManager
{
    internal static void CreateTable(string? connectionString)
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

    internal static void AddRecord(string? connectionString, Coding coding)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            $"""
             INSERT INTO coding_tracker (date, duration)
             VALUES ('{coding.Date}', '{coding.Duration}');
             """;
        var rowsAffected = insertCommand.ExecuteNonQuery();
        
        AnsiConsole.MarkupLine(rowsAffected != 1
            ? "[bold red]Error adding record![/]"
            : "[bold green]Record added successfully![/]");
    }
}