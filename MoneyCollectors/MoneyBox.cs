namespace MoneyCollectors
{
    /// <summary>
    /// The class which is used to represent a money box (used in vending machines and booths).
    /// </summary>
    public class MoneyBox : MoneyCollector
    {
        /// <summary>
        /// Removes a specified amount of money from the money box.
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>The money that was removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            return base.RemoveMoney(amount);
        }
    }
}
