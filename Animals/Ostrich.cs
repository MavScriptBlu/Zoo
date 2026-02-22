using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an ostrich.
    /// </summary>
    public sealed class Ostrich : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Ostrich class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Ostrich(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 30.0;
        }

        /// <summary>
        /// Moves by walking.
        /// </summary>
        public override void Move()
        {
            // Walk.
        }
    }
}
