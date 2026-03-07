using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a fish.
    /// </summary>
    public abstract class Fish : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Fish class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Fish(string name, double weight, Gender gender)
            : base(name, weight, gender)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Fish class.
        /// </summary>
        /// <param name="age">The age of the animal.</param>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Fish(int age, string name, double weight, Gender gender)
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
        /// Moves by swimming.
        /// </summary>
        public override void Move()
        {
            // Swim horizontally.
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

            // Swim vertically.
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
    }
}
