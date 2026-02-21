namespace Animals
{
    /// <summary>
    /// The class which is used to represent a kangaroo.
    /// </summary>
    public class Kangaroo : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Kangaroo class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Kangaroo(string name, int age, double weight)
            : base(name, age, weight)
        {
            this.BabyWeightPercentage = 13.0;
        }

        /// <summary>
        /// Moves by hopping.
        /// </summary>
        public override void Move()
        {
            // Hop.
        }
    }
}
