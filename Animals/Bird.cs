using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a bird.
    /// </summary>
    public abstract class Bird : Animal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Bird class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Bird(string name, double weight, Gender gender)
            : base(name, weight, gender)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Bird class.
        /// </summary>
        /// <param name="age">The age of the animal.</param>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Bird(int age, string name, double weight, Gender gender)
            : base(age, name, weight, gender)
        {
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        protected override double WeightGainPercentage
        {
            get
            {
                return 5.0;
            }
        }

        /// <summary>
        /// Hatches from its egg.
        /// </summary>
        public void Hatch()
        {
            // Break out of egg.
        }

        /// <summary>
        /// Moves by flying.
        /// </summary>
        public override void Move()
        {
            // Fly horizontally.
            if (this.XDirection == HorizontalDirection.Right)
            {
                if (this.XPosition + this.MoveDistance > this.XPositionMax)
                {
                    this.XPosition = this.XPositionMax;
                    this.XDirection = HorizontalDirection.Left;
                }
                else
                {
                    this.XPosition += this.MoveDistance;
                }
            }
            else
            {
                if (this.XPosition - this.MoveDistance < 0)
                {
                    this.XPosition = 0;
                    this.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    this.XPosition -= this.MoveDistance;
                }
            }

            // Fly vertically.
            if (this.YDirection == VerticalDirection.Down)
            {
                if (this.YPosition + this.MoveDistance > this.YPositionMax)
                {
                    this.YPosition = this.YPositionMax;
                    this.YDirection = VerticalDirection.Up;
                }
                else
                {
                    this.YPosition += this.MoveDistance;
                }
            }
            else
            {
                if (this.YPosition - this.MoveDistance < 0)
                {
                    this.YPosition = 0;
                    this.YDirection = VerticalDirection.Down;
                }
                else
                {
                    this.YPosition -= this.MoveDistance;
                }
            }
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public override IReproducer Reproduce()
        {
            // Lay an egg.
            IReproducer baby = this.LayEgg();

            // If the baby is hatchable...
            if (baby is IHatchable)
            {
                // Hatch the baby out of its egg.
                this.HatchEgg(baby as IHatchable);
            }

            // Return the (hatched) baby.
            return baby;
        }

        /// <summary>
        /// Hatches an egg.
        /// </summary>
        /// <param name="egg">The egg to hatch.</param>
        private void HatchEgg(IHatchable egg)
        {
            // Hatch the egg.
            egg.Hatch();
        }

        /// <summary>
        /// Lays an egg.
        /// </summary>
        /// <returns>The resulting egg.</returns>
        private IReproducer LayEgg()
        {
            // Return a baby (in egg form).
            return base.Reproduce();
        }
    }
}
