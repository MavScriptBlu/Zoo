namespace ZooScenario
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
        /// The amount of money currently contained within the wallet.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(string color)
        {
            this.color = color;
        }

        /// <summary>
        /// Adds a specified amount of money to the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the wallet.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the wallet...
            if (this.moneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.moneyBalance;
            }

            // Subtract the amount removed from the wallet's money balance.
            this.moneyBalance -= amountRemoved;

            return amountRemoved;
        }
    }
}