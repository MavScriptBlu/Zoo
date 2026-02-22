using System;
using Animals;
using People;
using Zoos;

namespace ZooConsole
{
    /// <summary>
    /// The class which provides helper methods for console commands.
    /// </summary>
    internal static class ConsoleHelper
    {
        /// <summary>
        /// Sets the birthing room temperature.
        /// </summary>
        /// <param name="zoo">The zoo whose temperature is to be set.</param>
        /// <param name="commandWords">The words entered as a command.</param>
        public static void SetTemperature(Zoo zoo, string[] commandWords)
        {
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
        }

        /// <summary>
        /// Displays information for the specified animal.
        /// </summary>
        /// <param name="zoo">The zoo to search.</param>
        /// <param name="name">The name of the animal to show.</param>
        public static void ShowAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(name);

            if (animal != null)
            {
                Console.WriteLine($"The following animal was found: {animal.ToString()}.");
            }
            else
            {
                Console.WriteLine("The animal could not be found.");
            }
        }

        /// <summary>
        /// Displays information for the specified guest.
        /// </summary>
        /// <param name="zoo">The zoo to search.</param>
        /// <param name="name">The name of the guest to show.</param>
        public static void ShowGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(name);

            if (guest != null)
            {
                Console.WriteLine($"The following guest was found: {guest.ToString()}.");
            }
            else
            {
                Console.WriteLine("The guest could not be found.");
            }
        }

        /// <summary>
        /// Processes the show command.
        /// </summary>
        /// <param name="zoo">The zoo to search.</param>
        /// <param name="commandWords">The words entered as a command.</param>
        public static void ProcessShowCommand(Zoo zoo, string[] commandWords)
        {
            try
            {
                string type = ConsoleUtil.InitialUpper(commandWords[1]);

                switch (type)
                {
                    case "Animal":
                        string animalName = ConsoleUtil.InitialUpper(commandWords[2]);
                        ShowAnimal(zoo, animalName);
                        break;

                    case "Guest":
                        string guestName = ConsoleUtil.InitialUpper(commandWords[2]);
                        ShowGuest(zoo, guestName);
                        break;

                    default:
                        Console.WriteLine($"Only animals and guests can be shown.");
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered for the show command.");
            }
        }
    }
}
