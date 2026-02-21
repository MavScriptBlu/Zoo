namespace BoothItems
{
    /// <summary>
    /// The class which is used to represent an item.
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// The price of the item.
        /// </summary>
        private decimal price;

        /// <summary>
        /// The weight of the item (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="price">The price of the item.</param>
        /// <param name="weight">The weight of the item.</param>
        public Item(decimal price, double weight)
        {
            this.price = price;
            this.weight = weight;
        }

        /// <summary>
        /// Gets the price of the item.
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.price;
            }
        }

        /// <summary>
        /// Gets the weight of the item.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}
