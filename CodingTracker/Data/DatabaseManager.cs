using System.Configuration;
using CodingTracker.Models;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Data;

internal static class DatabaseManager
{
    private static readonly string? ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

    internal static void CreateTable()
    {
        using var connection = new SqliteConnection(ConnectionString);
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


    internal static IEnumerable<Coding> ViewAllRecords()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var selectCommand = connection.CreateCommand();
        selectCommand.CommandText =
            "SELECT * FROM coding_tracker;";
        using var reader = selectCommand.ExecuteReader();

        List<Coding> codingSessions = new();
        while (reader.Read())
        {
            codingSessions.Add(new Coding
            {
                Id = reader.GetInt32(0),
                Date = reader.GetString(1),
                Duration = reader.GetString(2)
            });
        }

        return codingSessions;
    }

    internal static int AddRecord(Coding coding)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var insertCommand = connection.CreateCommand();
        insertCommand.CommandText =
            $"""
             INSERT INTO coding_tracker (date, duration)
             VALUES ('{coding.Date}', '{coding.Duration}');
             """;
        return insertCommand.ExecuteNonQuery();
    }

    public static int DeleteRecord(Coding coding)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var deleteCommand = connection.CreateCommand();
        deleteCommand.CommandText =
            $"""
             DELETE FROM coding_tracker
             WHERE id = {coding.Id};
             """;
        return deleteCommand.ExecuteNonQuery();
    }

    public static int UpdateRecord(UserInput userInput)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var updateCommand = connection.CreateCommand();
        updateCommand.CommandText =
            $"""
                UPDATE coding_tracker
                SET date = '{userInput.date}', duration = '{userInput.duration}'
                where id = {userInput.id}
             """;
        return updateCommand.ExecuteNonQuery();
    }
}