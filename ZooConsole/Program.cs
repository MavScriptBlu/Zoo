using System;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// The class which is used to represent the console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The zoo to be managed.
        /// </summary>
        private static Zoo zoo;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Como Zoo!");

            Console.Title = "Object-Oriented Programming 2: Zoo";

            zoo = Zoo.NewZoo();

            Console.WriteLine("A new Como Zoo has been created.");

            bool exit = false;

            string command;

            while (exit != true)
            {
                Console.Write("] ");
                command = Console.ReadLine();
                command = command.ToLower().Trim();

                string[] commandWords = command.Split();

                if (commandWords.Length == 0)
                {
                    continue;
                }

                switch (commandWords[0])
                {
                    case "exit":
                        exit = true;
                        break;

                    case "restart":
                        zoo = Zoo.NewZoo();
                        Console.WriteLine("A new Como Zoo has been created.");
                        break;

                    case "help":
                        Console.WriteLine("Known commands:");
                        Console.WriteLine("HELP: Shows a list of known commands.");
                        Console.WriteLine("EXIT: Exits the application.");
                        Console.WriteLine("RESTART: Creates a new zoo.");
                        Console.WriteLine("TEMP: Sets the birthing room temperature.");
                        Console.WriteLine("SHOW ANIMAL [animal name]: Displays information for specified animal.");
                        Console.WriteLine("SHOW GUEST [guest name]: Displays information for specified guest.");
                        Console.WriteLine("ADD ANIMAL: Adds a new animal to the zoo.");
                        Console.WriteLine("ADD GUEST: Adds a new guest to the zoo.");
                        Console.WriteLine("REMOVE ANIMAL [animal name]: Removes the specified animal from the zoo.");
                        Console.WriteLine("REMOVE GUEST [guest name]: Removes the specified guest from the zoo.");
                        break;

                    case "temp":
                        ConsoleHelper.SetTemperature(zoo, commandWords);
                        break;

                    case "show":
                        ConsoleHelper.ProcessShowCommand(zoo, commandWords);
                        break;

                    case "add":
                        try
                        {
                            ConsoleHelper.ProcessAddCommand(zoo, commandWords[1]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("A type (animal or guest) must be entered for the add command.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case "remove":
                        try
                        {
                            ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("A type and name must be entered for the remove command.");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid command entered: " + command);
                        break;
                }
            }
        }
    }
}
