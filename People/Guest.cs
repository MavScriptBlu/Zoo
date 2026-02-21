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
        /// Visits the ticket booth to buy a ticket and other items.
        /// </summary>
        /// <param name="ticketBooth">The booth to visit.</param>
        /// <returns>The purchased ticket.</returns>
        public Ticket VisitTicketBooth(Booth ticketBooth)
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

            // Get a free map.
            Map map = ticketBooth.GiveFreeMap();

            // Get a free coupon book.
            CouponBook couponBook = ticketBooth.GiveFreeCouponBook();

            return ticket;
        }
    }
}
