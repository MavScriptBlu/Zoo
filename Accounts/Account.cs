using MoneyCollectors;

namespace Accounts
{
    /// <summary>
    /// The class which is used to represent a financial account.
    /// </summary>
    public class Account : IMoneyCollector
    {
        /// <summary>
        /// The current money balance of the account.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets the money balance of the account.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }
        }

        /// <summary>
        /// Adds a specified amount of money to the account.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBalance += amount;
        }

        /// <summary>
        /// Removes a specified amount of money from the account.
        /// Unlike a wallet, the account balance can go negative.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The amount that was removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            this.moneyBalance -= amount;
            return amount;
        }
    }
}
