using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Animals;
using BoothItems;
using CagedItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using VendingMachines;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    public class Guest : IEater, ICageable
    {
        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// The guest's checking account.
        /// </summary>
        private IMoneyCollector checkingAccount;

        /// <summary>
        /// The gender of the guest.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private IMoneyCollector wallet;

        /// <summary>
        /// The guest's bag of items.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="gender">The gender of the guest.</param>
        /// <param name="account">The guest's checking account.</param>
        public Guest(string name, int age, Gender gender, IMoneyCollector account)
        {
            this.age = age;
            this.gender = gender;
            this.name = name;
            this.wallet = new Wallet();
            this.checkingAccount = account;
            this.bag = new List<Item>();
            this.XPosition = 400;
            this.YPosition = 200;
        }

        /// <summary>
        /// Gets or sets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 120)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 120.");
                }

                this.age = value;
            }
        }

        /// <summary>
        /// Gets the guest's checking account.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets or sets the gender of the guest.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    throw new ArgumentException("Name must contain only letters or spaces.");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the animal the guest has adopted.
        /// </summary>
        public Animal AdoptedAnimal { get; set; }

        /// <summary>
        /// Gets or sets the horizontal position of the guest.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the vertical position of the guest.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets the display size of the guest.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets the resource key for the guest's image.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        /// <summary>
        /// Gets the guest's wallet.
        /// </summary>
        public IMoneyCollector Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets the weight of the guest.
        /// </summary>
        public double Weight
        {
            get
            {
                // Confidential.
                return 0.0;
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        /// <param name="animalSnackMachine">The animal snack machine from which to buy food.</param>
        public void FeedAnimal(IEater eater, VendingMachine animalSnackMachine)
        {
            // Find food price.
            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            // If wallet doesn't have enough, withdraw from checking account.
            if (this.wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(10 * price);
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(price);

            // Buy food.
            Food food = animalSnackMachine.BuyFood(payment);

            // Feed animal.
            eater.Eat(food);
        }

        /// <summary>
        /// Visits the ticket booth to buy a ticket and a water bottle.
        /// </summary>
        /// <param name="ticketBooth">The booth to visit.</param>
        /// <returns>The purchased ticket.</returns>
        public Ticket VisitTicketBooth(MoneyCollectingBooth ticketBooth)
        {
            // Get the ticket price.
            decimal ticketPrice = ticketBooth.TicketPrice;

            // Get the water bottle price.
            decimal waterBottlePrice = ticketBooth.WaterBottlePrice;

            // If wallet doesn't have enough, withdraw from checking account.
            if (this.wallet.MoneyBalance < ticketPrice + waterBottlePrice)
            {
                this.WithdrawMoney(2 * (ticketPrice + waterBottlePrice));
            }

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(ticketPrice);

            // Buy the ticket.
            Ticket ticket = ticketBooth.SellTicket(payment);

            // Get money from wallet for water bottle.
            decimal waterBottlePayment = this.wallet.RemoveMoney(waterBottlePrice);

            // Buy a water bottle.
            WaterBottle waterBottle = ticketBooth.SellWaterBottle(waterBottlePayment);

            // Add water bottle to bag.
            this.bag.Add(waterBottle);

            return ticket;
        }

        /// <summary>
        /// Visits the information booth to receive free items.
        /// </summary>
        /// <param name="informationBooth">The booth to visit.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            // Get a free map.
            Map map = informationBooth.GiveFreeMap();

            // Get a free coupon book.
            CouponBook couponBook = informationBooth.GiveFreeCouponBook();

            // Add items to bag.
            this.bag.Add(map);
            this.bag.Add(couponBook);
        }

        /// <summary>
        /// Withdraws money from the checking account into the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to withdraw.</param>
        public void WithdrawMoney(decimal amount)
        {
            this.wallet.AddMoney(this.checkingAccount.RemoveMoney(amount));
        }

        /// <summary>
        /// Generates a string representation of the guest.
        /// </summary>
        /// <returns>A string representation of the guest.</returns>
        public override string ToString()
        {
            string adoptedAnimalName = this.AdoptedAnimal == null ? string.Empty : string.Format(" [{0}]", this.AdoptedAnimal.Name);
            return string.Format("{0}: {1} [${2:0.00} / ${3:0.00}]{4}", this.name, this.age, this.wallet.MoneyBalance, this.checkingAccount.MoneyBalance, adoptedAnimalName);
        }
    }
}
