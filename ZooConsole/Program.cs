using System;
using Animals;
using People;
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
                        break;

                    case "temp":
                        try
                        {
                            double previousTemp = zoo.BirthingRoomTemperature;
                            zoo.BirthingRoomTemperature = double.Parse(commandWords[1]);
                            Console.WriteLine($"Previous temperature: {previousTemp:0.0} °F.");
                            Console.WriteLine($"New temperature: {zoo.BirthingRoomTemperature:0.0} °F.");
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("A number must be entered as a parameter.");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("A parameter must be entered for the temperature command.");
                        }

                        break;

                    case "show":
                        try
                        {
                            switch (commandWords[1])
                            {
                                case "animal":
                                    string animalName = InitialUpper(commandWords[2]);
                                    Animal animal = zoo.FindAnimal(animalName);
                                    if (animal != null)
                                    {
                                        Console.WriteLine($"The following animal was found: {animal.ToString()}.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("The animal could not be found.");
                                    }

                                    break;

                                case "guest":
                                    string guestName = InitialUpper(commandWords[2]);
                                    Guest guest = zoo.FindGuest(guestName);
                                    if (guest != null)
                                    {
                                        Console.WriteLine($"The following guest was found: {guest.ToString()}.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("The guest could not be found.");
                                    }

                                    break;
                            }
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("A number must be entered as a parameter.");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("A parameter must be entered for the temperature command.");
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid command entered: " + command);
                        break;
                }
            }
        }

        /// <summary>
        /// Capitalizes the first letter of a string.
        /// </summary>
        /// <param name="str">The string to capitalize.</param>
        /// <returns>The string with its first letter capitalized.</returns>
        private static string InitialUpper(string str)
        {
            if (str == null || str.Length == 0)
            {
                return str;
            }

            string result = char.ToUpper(str[0]) + str.Substring(1);
            return result;
        }
    }
}
