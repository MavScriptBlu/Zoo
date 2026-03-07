using System;
using System.Timers;
using CagedItems;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// The gender of the animal.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// The random number generator used for movement initialization.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The timer that controls animal movement.
        /// </summary>
        private Timer moveTimer;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.gender = gender;
            this.name = name;
            this.weight = weight;

            // Randomize movement properties.
            this.MoveDistance = random.Next(5, 16);
            this.XPosition = random.NextDouble() * this.XPositionMax;
            this.YPosition = random.NextDouble() * this.YPositionMax;
            this.XDirection = random.Next(2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = random.Next(2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;

            // Set up the movement timer.
            this.moveTimer = new Timer(1000);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();
        }

        /// <summary>
        /// Gets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }
        }

        /// <summary>
        /// Gets or sets the age of the animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("age", "Age must be between 0 and 100.");
                }

                this.age = value;
            }
        }

        /// <summary>
        /// Gets or sets the gender of the animal.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentException("Name must have a value.");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentOutOfRangeException("weight", "Weight must be between 0 and 1000.");
                }

                this.weight = value;
            }
        }

        /// <summary>
        /// Gets or sets the horizontal position of the animal.
        /// </summary>
        public double XPosition { get; set; }

        /// <summary>
        /// Gets or sets the vertical position of the animal.
        /// </summary>
        public double YPosition { get; set; }

        /// <summary>
        /// Gets the maximum horizontal position of the animal.
        /// </summary>
        public double XPositionMax { get; } = 800;

        /// <summary>
        /// Gets the maximum vertical position of the animal.
        /// </summary>
        public double YPositionMax { get; } = 400;

        /// <summary>
        /// Gets or sets the horizontal direction of the animal.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the vertical direction of the animal.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets or sets the distance the animal moves per movement cycle.
        /// </summary>
        public double MoveDistance { get; set; }

        /// <summary>
        /// Gets the display size of the animal.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                return 1.0;
            }
        }

        /// <summary>
        /// Gets the resource key for the animal's image.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        protected double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        protected abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            // Increase animal's weight as a result of eating food.
            this.Weight += food.Weight * (this.WeightGainPercentage / 100);
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.isPregnant = true;
        }

        /// <summary>
        /// Moves about.
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public virtual IReproducer Reproduce()
        {
            // Create a baby reproducer.
            Animal baby = Activator.CreateInstance(this.GetType(), string.Empty, 0, this.Weight * (this.BabyWeightPercentage / 100), this.gender) as Animal;

            // Reduce mother's weight by 25 percent more than the value of the baby's weight.
            this.Weight -= baby.Weight * 1.25;

            // Make mother not pregnant after giving birth.
            this.isPregnant = false;

            return baby;
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            return this.name + ": " + this.GetType().Name + " (" + this.age + ", " + this.Weight + ")";
        }

        /// <summary>
        /// Handles the move timer elapsed event.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The arguments for the event.</param>
        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
#if DEBUG
            this.moveTimer.Stop();
#endif
            this.Move();
#if DEBUG
            this.moveTimer.Start();
#endif
        }
    }
}
