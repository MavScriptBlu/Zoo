using System;
using Accounts;
using Animals;
using MoneyCollectors;
using People;
using Reproducers;
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
                        Console.WriteLine("Only animals and guests can be shown.");
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered for the show command.");
            }
        }

        /// <summary>
        /// Processes the add command.
        /// </summary>
        /// <param name="zoo">The zoo to add to.</param>
        /// <param name="type">The type of object to add.</param>
        public static void ProcessAddCommand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    AddAnimal(zoo);
                    break;

                case "guest":
                    AddGuest(zoo);
                    break;

                default:
                    Console.WriteLine("The command only supports adding animals and guests.");
                    break;
            }
        }

        /// <summary>
        /// Processes the remove command.
        /// </summary>
        /// <param name="zoo">The zoo to remove from.</param>
        /// <param name="type">The type of object to remove.</param>
        /// <param name="name">The name of the object to remove.</param>
        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            switch (type)
            {
                case "animal":
                    RemoveAnimal(zoo, ConsoleUtil.InitialUpper(name));
                    break;

                case "guest":
                    RemoveGuest(zoo, ConsoleUtil.InitialUpper(name));
                    break;

                default:
                    Console.WriteLine("The command only supports removing animals and guests.");
                    break;
            }
        }

        /// <summary>
        /// Adds an animal to the zoo based on user input.
        /// </summary>
        /// <param name="zoo">The zoo to add the animal to.</param>
        private static void AddAnimal(Zoo zoo)
        {
            AnimalType animalType = ConsoleUtil.ReadAnimalType();
            Animal animal = AnimalFactory.CreateAnimal(animalType, "Default", 0, 0, Gender.Female);

            bool success = false;

            while (!success)
            {
                try
                {
                    animal.Name = ConsoleUtil.InitialUpper(ConsoleUtil.ReadAlphabeticValue("Name"));
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            animal.Gender = ConsoleUtil.ReadGender();

            success = false;

            while (!success)
            {
                try
                {
                    animal.Age = ConsoleUtil.ReadIntValue("Age");
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            success = false;

            while (!success)
            {
                try
                {
                    animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            zoo.AddAnimal(animal);
            ShowAnimal(zoo, animal.Name);
        }

        /// <summary>
        /// Adds a guest to the zoo based on user input.
        /// </summary>
        /// <param name="zoo">The zoo to add the guest to.</param>
        private static void AddGuest(Zoo zoo)
        {
            string name = ConsoleUtil.InitialUpper(ConsoleUtil.ReadAlphabeticValue("Name"));
            int age = ConsoleUtil.ReadIntValue("Age");
            Gender gender = ConsoleUtil.ReadGender();
            decimal walletBalance = (decimal)ConsoleUtil.ReadDoubleValue("Wallet balance");
            WalletColor walletColor = ConsoleUtil.ReadWalletColor();
            decimal checkingBalance = (decimal)ConsoleUtil.ReadDoubleValue("Checking account balance");

            IMoneyCollector account = new Account();
            account.AddMoney(checkingBalance);

            Guest guest = new Guest(name, age, gender, account);
            guest.Wallet.AddMoney(walletBalance);

            Ticket ticket = zoo.SellTicket(guest);
            zoo.AddGuest(guest, ticket);

            ShowGuest(zoo, guest.Name);
        }

        /// <summary>
        /// Removes an animal from the zoo by name.
        /// </summary>
        /// <param name="zoo">The zoo to remove the animal from.</param>
        /// <param name="name">The name of the animal to remove.</param>
        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(name);

            if (animal != null)
            {
                zoo.RemoveAnimal(animal);
                Console.WriteLine($"Animal {name} was successfully removed from the zoo.");
            }
            else
            {
                Console.WriteLine($"Animal {name} could not be found.");
            }
        }

        /// <summary>
        /// Removes a guest from the zoo by name.
        /// </summary>
        /// <param name="zoo">The zoo to remove the guest from.</param>
        /// <param name="name">The name of the guest to remove.</param>
        private static void RemoveGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(name);

            if (guest != null)
            {
                zoo.RemoveGuest(guest);
                Console.WriteLine($"Guest {name} was successfully removed from the zoo.");
            }
            else
            {
                Console.WriteLine($"Guest {name} could not be found.");
            }
        }
    }
}
