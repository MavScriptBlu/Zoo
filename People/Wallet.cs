using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    public class Wallet : MoneyCollector
    {
        /// <summary>
        /// The color of the wallet.
        /// </summary>
        private string color;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(string color)
        {
            this.color = color;
        }
    }
}
