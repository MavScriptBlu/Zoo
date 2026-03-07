using System;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a mammal.
    /// </summary>
    public abstract class Mammal : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Mammal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Mammal(string name, double weight, Gender gender)
            : base(name, weight, gender)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Mammal class.
        /// </summary>
        /// <param name="age">The age of the animal.</param>
        /// <param name="name">The name of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Mammal(int age, string name, double weight, Gender gender)
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
                return 15.0;
            }
        }

        /// <summary>
        /// Moves by pacing.
        /// </summary>
        public override void Move()
        {
            // Pace left or right.
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
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public override IReproducer Reproduce()
        {
            // Create a baby reproducer.
            IReproducer baby = base.Reproduce();

            // If the animal is not a platypus and baby is an eater...
            if (this.GetType() != typeof(Platypus) && baby is IEater)
            {
                // Feed the baby.
                this.FeedNewborn(baby as IEater);
            }

            return baby;
        }

        /// <summary>
        /// Feeds a baby eater.
        /// </summary>
        /// <param name="newborn">The eater to feed.</param>
        private void FeedNewborn(IEater newborn)
        {
            // Determine milk weight.
            double milkWeight = this.Weight * 0.005;

            // Generate milk.
            Food milk = new Food(milkWeight);

            // Feed baby.
            newborn.Eat(milk);

            // Reduce parent's weight.
            this.Weight -= milkWeight;
        }
    }
}
