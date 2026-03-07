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
        public Ostrich(string name, double weight, Gender gender)
            : this(0, name, weight, gender)
        {
        }

        public Ostrich(int age, string name, double weight, Gender gender)
            : base(age, name, weight, gender)
        {
            this.BabyWeightPercentage = 30.0;
        }

        /// <summary>
        /// Gets the display size of the ostrich.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.4 : 0.8;
            }
        }

        /// <summary>
        /// Moves by walking.
        /// </summary>
        public override void Move()
        {
            // Walk (use base flying movement for position tracking).
            base.Move();
        }
    }
}
