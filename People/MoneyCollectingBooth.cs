using BoothItems;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a booth that collects money for sold items.
    /// </summary>
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The price of a water bottle.
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// The booth's internal money box.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// Initializes a new instance of the MoneyCollectingBooth class.
        /// </summary>
        /// <param name="attendant">The employee to be the booth's attendant.</param>
        /// <param name="ticketPrice">The price of a ticket.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        /// <param name="moneyBox">The money box to use for collecting money.</param>
        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.moneyBox = moneyBox;

            // Create tickets (5 total) and water bottles (5 total).
            for (int i = 0; i < 5; i++)
            {
                int serialNumber = i + 1;
                this.Items.Add(new Ticket(15m, serialNumber, 0.01));
                this.Items.Add(new WaterBottle(this.waterBottlePrice, serialNumber, 1));
            }
        }

        /// <summary>
        /// Gets the money balance of the booth.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
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
        /// Adds a specified amount of money to the booth.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// Removes a specified amount of money from the booth.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyBox.RemoveMoney(amount);
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
                ticket = this.Attendant.FindItem(this.Items, typeof(Ticket)) as Ticket;

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
                waterBottle = this.Attendant.FindItem(this.Items, typeof(WaterBottle)) as WaterBottle;

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
