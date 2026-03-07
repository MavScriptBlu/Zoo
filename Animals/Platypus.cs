using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a platypus.
    /// </summary>
    public sealed class Platypus : Mammal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Platypus class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        public Platypus(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 12.0;
        }

        /// <summary>
        /// Gets the display size of the platypus.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return 0.7;
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public override void Eat(Food food)
        {
            this.StashInPouch(food);

            base.Eat(food);
        }

        /// <summary>
        /// Hatches the animal.
        /// </summary>
        public void Hatch()
        {
            // The animal hatches from an egg.
        }

        /// <summary>
        /// Moves by swimming.
        /// </summary>
        public override void Move()
        {
            // Swim. Note that there is a base method that paces, which we are intentionally avoiding.

            // Swim horizontally.
            if (this.XDirection == HorizontalDirection.Right)
            {
                this.XPosition += this.MoveDistance;

                if (this.XPosition >= this.XPositionMax)
                {
                    this.XPosition = this.XPositionMax;
                    this.XDirection = HorizontalDirection.Left;
                }
            }
            else
            {
                this.XPosition -= this.MoveDistance;

                if (this.XPosition <= 0)
                {
                    this.XPosition = 0;
                    this.XDirection = HorizontalDirection.Right;
                }
            }

            // Swim vertically.
            if (this.YDirection == VerticalDirection.Down)
            {
                this.YPosition += this.MoveDistance;

                if (this.YPosition >= this.YPositionMax)
                {
                    this.YPosition = this.YPositionMax;
                    this.YDirection = VerticalDirection.Up;
                }
            }
            else
            {
                this.YPosition -= this.MoveDistance;

                if (this.YPosition <= 0)
                {
                    this.YPosition = 0;
                    this.YDirection = VerticalDirection.Down;
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
            IReproducer result = this.LayEgg();

            // If the baby is hatchable...
            if (result is IHatchable)
            {
                // Hatch the baby (egg).
                this.HatchEgg(result as IHatchable);
            }

            // Return the (hatched) baby.
            return result;
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
            // Return the baby (egg) from the base Reproduce method.
            return base.Reproduce();
        }

        /// <summary>
        /// Stashes food in its cheek pouches.
        /// </summary>
        /// <param name="food">The food to be stashed.</param>
        private void StashInPouch(Food food)
        {
            // Stash food to eat later.
        }
    }
}
