using System;
using System.Collections.Generic;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// The zoo's information booth.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        /// <param name="boothMoneyBalance">The initial money balance of the zoo's ticket booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animals = new List<Animal>();
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.b168 = new BirthingRoom(vet);
            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, waterBottlePrice, new MoneyBox());
            this.ticketBooth.AddMoney(boothMoneyBalance);
            this.informationBooth = new GivingBooth(attendant);
        }

        /// <summary>
        /// Gets all animals currently in the zoo.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets all guests currently visiting the zoo.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        /// <summary>
        /// Gets the zoo's animal snack machine.
        /// </summary>
        public VendingMachine AnimalSnackMachine
        {
            get
            {
                return this.animalSnackMachine;
            }
        }

        /// <summary>
        /// Gets the average weight of all animals in the zoo.
        /// </summary>
        public double AverageAnimalWeight
        {
            get
            {
                return this.TotalAnimalWeight / this.animals.Count;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                // Define accumulator variable.
                double totalWeight = 0;

                // Loop through the list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            this.animals.Add(animal);
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The guest's ticket.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket == null)
            {
                throw new NullReferenceException("Guest could not be admitted because they do not have a ticket.");
            }

            if (!ticket.IsRedeemed)
            {
                ticket.Redeem();
                this.guests.Add(guest);
            }
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        public void BirthAnimal(IReproducer reproducer)
        {
            // Birth animal.
            IReproducer baby = this.b168.BirthAnimal(reproducer);

            // If the baby is an animal...
            if (baby is Animal)
            {
                // Add the baby to the zoo's list of animals.
                this.AddAnimal(baby as Animal);
            }
        }

        /// <summary>
        /// Finds an animal based on type.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public Animal FindAnimal(Type type)
        {
            // Define variable to hold matching animal.
            Animal animal = null;

            // Loop through the list of animals.
            foreach (Animal a in this.animals)
            {
                // If the current animal matches...
                if (a.GetType() == type)
                {
                    // Set the current animal to the variable.
                    animal = a;

                    // Break out of the loop.
                    break;
                }
            }

            // Return the matching animal.
            return animal;
        }

        /// <summary>
        /// Finds an animal based on type and pregnancy status.
        /// </summary>
        /// <param name="type">The type of the animal to find.</param>
        /// <param name="isPregnant">The pregnancy status of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public Animal FindAnimal(Type type, bool isPregnant)
        {
            // Define variable to hold matching animal.
            Animal animal = null;

            // Loop through the list of animals.
            foreach (Animal a in this.animals)
            {
                // If the current animal matches...
                if (a.GetType() == type && a.IsPregnant == isPregnant)
                {
                    // Store the current animal in the variable.
                    animal = a;

                    // Break out of the loop.
                    break;
                }
            }

            // Return the matching animal.
            return animal;
        }

        /// <summary>
        /// Finds an animal based on name.
        /// </summary>
        /// <param name="name">The name of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public Animal FindAnimal(string name)
        {
            Animal animal = null;

            foreach (Animal a in this.animals)
            {
                if (a.Name == name)
                {
                    animal = a;
                    break;
                }
            }

            return animal;
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="name">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public Guest FindGuest(string name)
        {
            // Define variable to hold matching guest.
            Guest guest = null;

            // Loop through the list of guests.
            foreach (Guest g in this.guests)
            {
                // If the current guest matches...
                if (g.Name == name)
                {
                    // Store the current guest in the variable.
                    guest = g;

                    // Break out of the loop.
                    break;
                }
            }

            // Return the matching guest.
            return guest;
        }

        /// <summary>
        /// Sells a ticket to the specified guest.
        /// </summary>
        /// <param name="guest">The guest to sell a ticket to.</param>
        /// <returns>The sold ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            // Have the guest visit the ticket booth.
            Ticket ticket = guest.VisitTicketBooth(this.ticketBooth);

            // Have the guest visit the information booth.
            guest.VisitInformationBooth(this.informationBooth);

            return ticket;
        }

        /// <summary>
        /// Creates a new Como Zoo with default animals and guests.
        /// </summary>
        /// <returns>The created zoo.</returns>
        public static Zoo NewZoo()
        {
            // Create employees.
            Employee sam = new Employee("Sam", 42);
            Employee flora = new Employee("Flora", 98);

            // Create the zoo.
            Zoo zoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, sam, flora);

            // Set the birthing room temperature to its starting value.
            zoo.BirthingRoomTemperature = 77;

            // Create and add animals.
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Dingo, "Pierre", 3, 25.2, Gender.Male));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Dingo, "Jackie", 4, 35.3, Gender.Female));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Platypus, "Patty", 2, 15.5, Gender.Female));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Hummingbird, "Harold", 1, 0.5, Gender.Male));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Chimpanzee, "Charlie", 5, 90.0, Gender.Male));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Eagle, "Emily", 3, 12.5, Gender.Female));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Kangaroo, "Kevin", 4, 110.0, Gender.Male));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Ostrich, "Oliver", 2, 250.0, Gender.Male));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Shark, "Steve", 6, 390.0, Gender.Male));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Squirrel, "Sammy", 1, 1.5, Gender.Female));
            zoo.AddAnimal(AnimalFactory.CreateAnimal(AnimalType.Dingo, "Dolly", 3, 22.0, Gender.Female));

            // Create guests.
            IMoneyCollector gregAccount = new Account();
            gregAccount.AddMoney(2500m);
            Guest greg = new Guest("Greg", 44, 25.00m, WalletColor.Brown, Gender.Male, gregAccount);
            Guest darla = new Guest("Darla", 11, 10.00m, WalletColor.Salmon, Gender.Female, greg.Wallet);

            // Sell tickets and add guests.
            zoo.AddGuest(greg, zoo.SellTicket(greg));
            zoo.AddGuest(darla, zoo.SellTicket(darla));

            return zoo;
        }
    }
}
