using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a shark.
    /// </summary>
    public class Shark : Fish
    {
        /// <summary>
        /// Initializes a new instance of the Shark class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Shark(string name, double weight, Gender gender)
            : this(0, name, weight, gender)
        {
        }

        public Shark(int age, string name, double weight, Gender gender)
            : base(age, name, weight, gender)
        {
            this.BabyWeightPercentage = 18.0;
        }

        /// <summary>
        /// Gets the display size of the shark.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 1.0 : 1.5;
            }
        }
    }
}
