using System;
using System.Collections.Generic;
using BoothItems;

namespace ZooScenario
{
    /// <summary>
    /// The class which is used to represent a booth.
    /// </summary>
    public class Booth
    {
        /// <summary>
        /// The employee currently assigned to be the attendant of the booth.
        /// </summary>
        private Employee attendant;

        /// <summary>
        /// The list of coupon books in the booth.
        /// </summary>
        private List<CouponBook> couponBooks;

        /// <summary>
        /// The list of maps in the booth.
        /// </summary>
        private List<Map> maps;

        /// <summary>
        /// The amount of money currently in the booth.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// The list of tickets in the booth.
        /// </summary>
        private List<Ticket> tickets;

        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The list of water bottles in the booth.
        /// </summary>
        private List<WaterBottle> waterBottles;

        /// <summary>
        /// Initializes a new instance of the Booth class.
        /// </summary>
        /// <param name="attendant">The employee to be the booth's attendant.</param>
        /// <param name="ticketPrice">The price of a ticket.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        public Booth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice)
        {
            this.attendant = attendant;
            this.ticketPrice = ticketPrice;

            // Initialize lists
            this.couponBooks = new List<CouponBook>();
            this.maps = new List<Map>();
            this.tickets = new List<Ticket>();
            this.waterBottles = new List<WaterBottle>();

            // Create booth items using a single for loop
            for (int i = 0; i < 10; i++)
            {
                // Create maps (10 total)
                this.maps.Add(new Map(0.5, DateTime.Now));

                // Create items for the first 5 iterations
                if (i < 5)
                {
                    // Create coupon books (5 total)
                    // All issued today, expire one year from today, weigh 0.8
                    this.couponBooks.Add(new CouponBook(DateTime.Now, DateTime.Now.AddYears(1), 0.8));

                    // Create tickets (5 total)
                    // All are $15, weigh 0.01, have unique sequential serial numbers
                    int serialNumber = i + 1;
                    this.tickets.Add(new Ticket(15m, serialNumber, 0.01));

                    // Create water bottles (5 total)
                    // Use the passed in price, weigh 1, have unique serial numbers
                    this.waterBottles.Add(new WaterBottle(waterBottlePrice, serialNumber, 1));
                }
            }
        }

        /// <summary>
        /// Adds a specified amount of money to the booth.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
        }
    }
}
