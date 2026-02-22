namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a money pocket (used in a wallet).
    /// </summary>
    public class MoneyPocket : MoneyCollector
    {
        /// <summary>
        /// Removes a specified amount of money from the money pocket.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            return base.RemoveMoney(amount);
        }
    }
}
