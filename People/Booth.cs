using System;
using System.Collections.Generic;
using BoothItems;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a booth.
    /// </summary>
    public class Booth : MoneyCollector
    {
        /// <summary>
        /// The employee currently assigned to be the attendant of the booth.
        /// </summary>
        private Employee attendant;

        /// <summary>
        /// The list of items in the booth.
        /// </summary>
        private List<Item> items;

        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The price of a water bottle.
        /// </summary>
        private decimal waterBottlePrice;

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
            this.waterBottlePrice = waterBottlePrice;

            // Initialize list.
            this.items = new List<Item>();

            // Create booth items using a single for loop.
            for (int i = 0; i < 10; i++)
            {
                // Create maps (10 total).
                this.items.Add(new Map(0.5, DateTime.Now));

                // Create items for the first 5 iterations.
                if (i < 5)
                {
                    // Create coupon books (5 total).
                    this.items.Add(new CouponBook(DateTime.Now, DateTime.Now.AddYears(1), 0.8));

                    // Create tickets (5 total).
                    int serialNumber = i + 1;
                    this.items.Add(new Ticket(15m, serialNumber, 0.01));

                    // Create water bottles (5 total).
                    this.items.Add(new WaterBottle(this.waterBottlePrice, serialNumber, 1));
                }
            }
        }

        /// <summary>
        /// Gets the price of a ticket.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
        }

        /// <summary>
        /// Gets the price of a water bottle.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return this.waterBottlePrice;
            }
        }

        /// <summary>
        /// Gives away a free coupon book.
        /// </summary>
        /// <returns>The coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            CouponBook couponBook = this.attendant.FindItem(this.items, typeof(CouponBook)) as CouponBook;

            return couponBook;
        }

        /// <summary>
        /// Gives away a free map.
        /// </summary>
        /// <returns>The map.</returns>
        public Map GiveFreeMap()
        {
            Map map = this.attendant.FindItem(this.items, typeof(Map)) as Map;

            return map;
        }

        /// <summary>
        /// Sells a ticket.
        /// </summary>
        /// <param name="payment">The payment for the ticket.</param>
        /// <returns>The sold ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            Ticket ticket = null;

            // Only find a ticket if the payment is sufficient.
            if (payment >= this.ticketPrice)
            {
                ticket = this.attendant.FindItem(this.items, typeof(Ticket)) as Ticket;

                // If a ticket was found, add the payment to the money balance.
                if (ticket != null)
                {
                    this.AddMoney(payment);
                }
            }

            return ticket;
        }

        /// <summary>
        /// Sells a water bottle.
        /// </summary>
        /// <param name="payment">The payment for the water bottle.</param>
        /// <returns>The sold water bottle.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            WaterBottle waterBottle = null;

            // Only find a water bottle if the payment is sufficient.
            if (payment >= this.waterBottlePrice)
            {
                waterBottle = this.attendant.FindItem(this.items, typeof(WaterBottle)) as WaterBottle;

                // If a water bottle was found, add the payment to the money balance.
                if (waterBottle != null)
                {
                    this.AddMoney(payment);
                }
            }

            return waterBottle;
        }
    }
}
