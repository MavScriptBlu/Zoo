namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a money collector.
    /// </summary>
    public class MoneyCollector
    {
        /// <summary>
        /// The amount of money currently in the money collector.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets the money balance of the money collector.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }
        }

        /// <summary>
        /// Adds a specified amount of money to the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the money collector...
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

            // Subtract the amount removed from the money collector's money balance.
            this.moneyBalance -= amountRemoved;

            return amountRemoved;
        }
    }
}
