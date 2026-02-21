namespace Animals
{
    /// <summary>
    /// The class which is used to represent a squirrel.
    /// </summary>
    public class Squirrel : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Squirrel class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Squirrel(string name, int age, double weight)
            : base(name, age, weight)
        {
            this.BabyWeightPercentage = 17.0;
        }

        /// <summary>
        /// Moves by climbing and scurrying.
        /// </summary>
        public override void Move()
        {
            // Climb and scurry.
        }
    }
}
