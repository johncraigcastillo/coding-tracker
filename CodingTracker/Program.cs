using CodingTracker.Data;

namespace CodingTracker;

internal static class Program
{
    private static void Main(string[] args)
    {
        UserInputController userInputController = new();

        DatabaseManager.CreateTable();
        userInputController.MainMenu();
    }
}