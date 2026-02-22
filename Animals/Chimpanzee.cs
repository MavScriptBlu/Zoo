using MoneyCollectors;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a chimpanzee.
    /// </summary>
    public class Chimpanzee : Mammal, IMoneyCollector
    {
        /// <summary>
        /// Initializes a new instance of the Chimpanzee class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Chimpanzee(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10.0;
        }

        /// <summary>
        /// Gets the money balance (chimpanzees don't hold money).
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Adds money (chimpanzees spend it on bananas).
        /// </summary>
        /// <param name="amount">The amount of money to add.</param>
        public void AddMoney(decimal amount)
        {
            // Chimpanzees buy bananas with any money given to them.
        }

        /// <summary>
        /// Removes money (chimpanzees have no money to give).
        /// </summary>
        /// <param name="amount">The amount of money to remove.</param>
        /// <returns>Zero, because chimpanzees have no money.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return 0;
        }

        /// <summary>
        /// Moves by climbing.
        /// </summary>
        public override void Move()
        {
            // Climb.
        }
    }
}
