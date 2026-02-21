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

            zoo = null;

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

                    case "new":
                        zoo = NewZoo();
                        zoo.BirthingRoomTemperature = 77;
                        break;

                    case "help":
                        Console.WriteLine("Known commands:");
                        Console.WriteLine("HELP: Shows a list of known commands.");
                        Console.WriteLine("EXIT: Exits the application.");
                        Console.WriteLine("NEW: Creates the ComoZoo.");
                        Console.WriteLine("TEMP: Sets the birthing room temperature.");
                        break;

                    case "temp":
                        try
                        {
                            double previousTemp = zoo.BirthingRoomTemperature;
                            zoo.BirthingRoomTemperature = double.Parse(commandWords[1]);
                            Console.WriteLine($"Previous temperature: {previousTemp:0.0} °F.");
                            Console.WriteLine($"New temperature: {zoo.BirthingRoomTemperature:0.0} °F.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid command entered: " + command);
                        break;
                }
            }
        }

        /// <summary>
        /// Creates a new zoo.
        /// </summary>
        /// <returns>The created zoo.</returns>
        private static Zoo NewZoo()
        {
            // Create employees.
            Employee sam = new Employee("Sam", 42);
            Employee flora = new Employee("Flora", 98);

            // Create the zoo.
            Zoo comoZoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, sam, flora);

            // Create and add animals.
            Dingo dPierre = new Dingo("Pierre", 3, 25.2);
            comoZoo.AddAnimal(dPierre);

            Dingo dJackie = new Dingo("Jackie", 4, 35.3);
            comoZoo.AddAnimal(dJackie);

            Platypus pPatty = new Platypus("Patty", 2, 15.5);
            comoZoo.AddAnimal(pPatty);

            Hummingbird hHarold = new Hummingbird("Harold", 1, 0.5);
            comoZoo.AddAnimal(hHarold);

            Chimpanzee cCharlie = new Chimpanzee("Charlie", 5, 90.0);
            comoZoo.AddAnimal(cCharlie);

            Eagle eEmily = new Eagle("Emily", 3, 12.5);
            comoZoo.AddAnimal(eEmily);

            Kangaroo kKevin = new Kangaroo("Kevin", 4, 110.0);
            comoZoo.AddAnimal(kKevin);

            Ostrich oOliver = new Ostrich("Oliver", 2, 250.0);
            comoZoo.AddAnimal(oOliver);

            Shark sSteve = new Shark("Steve", 6, 500.0);
            comoZoo.AddAnimal(sSteve);

            Squirrel sSammy = new Squirrel("Sammy", 1, 1.5);
            comoZoo.AddAnimal(sSammy);

            Console.WriteLine("A new Como Zoo has been created.");

            return comoZoo;
        }
    }
}
