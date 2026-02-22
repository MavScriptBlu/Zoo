using System.Collections.Generic;
using BoothItems;
using Foods;
using VendingMachines;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    public class Guest : IEater
    {
        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// The guest's bag of items.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        public Guest(string name, int age, decimal moneyBalance, string walletColor)
        {
            this.age = age;
            this.name = name;
            this.wallet = new Wallet(walletColor);
            this.wallet.AddMoney(moneyBalance);
            this.bag = new List<Item>();
        }

        /// <summary>
        /// Gets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }
        }

        /// <summary>
        /// Gets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
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

            // Get money from wallet.
            decimal payment = this.wallet.RemoveMoney(ticketPrice);

            // Buy the ticket.
            Ticket ticket = ticketBooth.SellTicket(payment);

            // Get the water bottle price.
            decimal waterBottlePrice = ticketBooth.WaterBottlePrice;

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
        /// Generates a string representation of the guest.
        /// </summary>
        /// <returns>A string representation of the guest.</returns>
        public override string ToString()
        {
            return string.Format("{0}: {1} [${2:0.00}]", this.name, this.age, this.wallet.MoneyBalance);
        }
    }
}
