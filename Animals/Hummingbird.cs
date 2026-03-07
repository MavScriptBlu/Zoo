using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a hummingbird.
    /// </summary>
    public class Hummingbird : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Hummingbird class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Hummingbird(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 17.5;
        }

        /// <summary>
        /// Gets the display size of the hummingbird.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return 0.4;
            }
        }

        /// <summary>
        /// Moves by hovering.
        /// </summary>
        public override void Move()
        {
            // Hover (use base flying movement).
            base.Move();
        }
    }
}
