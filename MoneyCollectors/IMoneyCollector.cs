namespace MoneyCollectors
{
    /// <summary>
    /// The interface which is used to represent a money collector.
    /// </summary>
    public interface IMoneyCollector
    {
        /// <summary>
        /// Gets the money balance of the money collector.
        /// </summary>
        decimal MoneyBalance { get; }

        /// <summary>
        /// Adds a specified amount of money to the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        void AddMoney(decimal amount);

        /// <summary>
        /// Removes a specified amount of money from the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        decimal RemoveMoney(decimal amount);
    }
}
