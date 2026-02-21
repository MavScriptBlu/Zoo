namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent a ticket.
    /// </summary>
    public class Ticket : Item
    {
        /// <summary>
        /// A value indicating whether the ticket has been redeemed.
        /// </summary>
        private bool isRedeemed;

        /// <summary>
        /// The serial number of the ticket.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Initializes a new instance of the Ticket class.
        /// </summary>
        /// <param name="price">The price of the ticket.</param>
        /// <param name="serialNumber">The serial number of the ticket.</param>
        /// <param name="weight">The weight of the ticket.</param>
        public Ticket(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
            this.isRedeemed = false;
        }

        /// <summary>
        /// Gets a value indicating whether the ticket has been redeemed.
        /// </summary>
        public bool IsRedeemed
        {
            get
            {
                return this.isRedeemed;
            }
        }

        /// <summary>
        /// Gets the serial number of the ticket.
        /// </summary>
        public int SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }

        /// <summary>
        /// Redeems the ticket.
        /// </summary>
        public void Redeem()
        {
            this.isRedeemed = true;
        }
    }
}
