using CodingTracker.Data;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Controllers;

internal static class CodingController
{

    public static void ViewRecords()
    {
        var codingSessions = DatabaseManager.ViewAllRecords();
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Date");
        table.AddColumn("Duration");
        foreach (var coding in codingSessions)
        {
            table.AddRow(coding.Id.ToString(), coding.Date, coding.Duration);
        }

        AnsiConsole.Write(table);
    }

    public static void AddRecord(UserInput userInput)
    {
        var coding = new Coding { Date = userInput.date, Duration = userInput.duration };
        var rowsAffected = DatabaseManager.AddRecord(coding);
        AnsiConsole.MarkupLine(rowsAffected != 1
            ? "[bold red]Error adding record![/]"
            : "[bold green]Record added successfully![/]");
    }

    public static void DeleteRecord(UserInput userInput)
    {
        var coding = new Coding { Id = userInput.id };
        var rowsAffected = DatabaseManager.DeleteRecord(coding);
        AnsiConsole.MarkupLine(rowsAffected != 1
            ? "[bold red]Record not deleted![/]"
            : "[bold green]Record deleted successfully![/]");
    }

    public static void UpdateRecord(UserInput userInput)
    {
        var rowsAffected = DatabaseManager.UpdateRecord(userInput);
        AnsiConsole.MarkupLine(rowsAffected != 1
            ? "[bold red]Record not updated![/]"
            : "[bold green]Record updated successfully![/]");
    }
}