using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    public class Wallet
    {
        /// <summary>
        /// The color of the wallet.
        /// </summary>
        private string color;

        /// <summary>
        /// The wallet's internal money pocket.
        /// </summary>
        private MoneyCollector moneyPocket;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(string color)
        {
            this.color = color;
            this.moneyPocket = new MoneyCollector();
        }

        /// <summary>
        /// Gets the money balance of the wallet.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyPocket.MoneyBalance;
            }
        }

        /// <summary>
        /// Adds a specified amount of money to the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyPocket.AddMoney(amount);
        }

        /// <summary>
        /// Removes a specified amount of money from the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyPocket.RemoveMoney(amount);
        }
    }
}
