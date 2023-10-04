using System.Globalization;
using Spectre.Console;

namespace CodingTracker;

internal class GetUserInput
{
    public void MainMenu()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("\n[bold blue]MAIN MENU[/]");
        var userSelection = GetUserMenuSelection();

        switch (userSelection)
        {
            case "View Records":
                // TODO: Implement View Records
                break;
            case "Add Record":
                Console.Clear();
                ProcessAdd();
                break;
            case "Delete Record":
                // TODO: Implement Delete Record
                break;
            case "Update Record":
                // TODO: Implement Update Record
                break;
            case "Exit":
                Environment.Exit(0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private static string GetUserMenuSelection()
    {
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("[bold blue]What would you like to do?[/]")
            .AddChoices("View Records", "Add Record", "Delete Record", "Update Record", "Exit")
        );
    }

    private enum InputType
    {
        Date,
        Duration
    }


    private void ProcessAdd()
    {
        var date = GetValidUserInput(type: InputType.Date);
        var duration = GetValidUserInput(type: InputType.Duration);
        CodingController.AddRecord(date: date, duration: duration);
    }

    private string GetValidUserInput(InputType type)
    {
        string userInput;
        do
        {
            userInput = RawUserInput(type: type);
            if (userInput == "q")
            {
                MainMenu();
            }

            if (!IsValidInput(userInput: userInput, type: type))
            {
                DisplayInvalidInputMessage(type: type);
            }
        } while (!IsValidInput(userInput: userInput, type: type));

        return userInput;
    }


    /// <summary>
    /// Prompts the user for input based on the specified input type.
    /// </summary>
    /// <param name="type">The type of input to prompt for (either date or duration)</param>
    /// <returns>The user-provided input as a string.</returns>
    private static string RawUserInput(InputType type)
    {
        return AnsiConsole.Ask<string>(type == InputType.Date
            ? "[bold blue]Enter the date of the coding session (MM/DD/YYYY)[/] [bold red]or q to cancel[/]:"
            : "[bold blue]Enter the duration of the coding session (hh:mm)[/] [bold red]or q to cancel[/]:");
    }

    private static bool IsValidInput(string userInput, InputType type)
    {
        return type switch
        {
            InputType.Date => DateTime.TryParseExact(userInput, "MM/dd/yyyy", new CultureInfo("en-US"),
                DateTimeStyles.None, out _),
            InputType.Duration => TimeSpan.TryParseExact(userInput, "hh\\:mm", new CultureInfo("en-US"), out _),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static void DisplayInvalidInputMessage(InputType type)
    {
        AnsiConsole.MarkupLine(type == InputType.Date
            ? "[bold red]Date must be a valid date in the format MM/DD/YYYY[/]"
            : "[bold red]Duration must be a valid time in the format hh:mm[/]");
    }
}