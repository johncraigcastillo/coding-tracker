using System.Globalization;
using CodingTracker.Controllers;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker;

internal class UserInputController
{
    private readonly UserInput _userInput = new();

    public void MainMenu()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("\n[bold blue]MAIN MENU[/]");
        _userInput.menuSelection = GetUserMenuSelection();

        switch (_userInput.menuSelection)
        {
            case "View Records":
                Console.Clear();
                CodingController.ViewRecords();
                ReturnToMainMenuPrompt();
                break;
            case "Add Record":
                Console.Clear();
                ProcessAdd();
                break;
            case "Delete Record":
                CodingController.ViewRecords();
                ProcessDelete();
                break;
            case "Update Record":
                CodingController.ViewRecords();
                ProcessUpdate();
                break;
            case "Exit":
                Environment.Exit(0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ProcessUpdate()
    {
        _userInput.id =
            AnsiConsole.Ask<int>(
                "[bold blue]Enter the ID of the record you would like to update[/] [bold red]or 0 to cancel[/]:");
        if (_userInput.id == 0)
        {
            MainMenu();
        }
        _userInput.date = GetUserInput(type: InputType.Date);
        _userInput.duration = GetUserInput(type: InputType.Duration);
        CodingController.UpdateRecord(_userInput);
        ReturnToMainMenuPrompt();
    }


    private void ProcessAdd()
    {
        _userInput.date = GetUserInput(type: InputType.Date);
        _userInput.duration = GetUserInput(type: InputType.Duration);
        CodingController.AddRecord(_userInput);
        ReturnToMainMenuPrompt();
    }

    private void ProcessDelete()
    {
        _userInput.id =
            AnsiConsole.Ask<int>(
                "[bold blue]Enter the ID of the record you would like to delete[/] [bold red]or 0 to cancel[/]:");
        if (_userInput.id == 0)
        {
            MainMenu();
        }

        CodingController.DeleteRecord(_userInput);
        ReturnToMainMenuPrompt();
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


    private void ReturnToMainMenuPrompt()
    {
        AnsiConsole.MarkupLine("\n[bold blue]Press any key to return to the main menu.[/]");
        Console.ReadKey();
        MainMenu();
    }

    private string GetUserInput(InputType type)
    {
        string userInput;
        do
        {
            userInput = PromptForUserInput(type: type);
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


    private static string PromptForUserInput(InputType type)
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