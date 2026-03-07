using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class used to create animal objects.
    /// </summary>
    public static class AnimalFactory
    {
        /// <summary>
        /// Creates an animal of the specified type.
        /// </summary>
        /// <param name="type">The type of animal to create.</param>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The gender of the animal.</param>
        /// <returns>The created animal.</returns>
        public static Animal CreateAnimal(AnimalType type, string name, int age, double weight, Gender gender)
        {
            Animal result = null;

            switch (type)
            {
                case AnimalType.Chimpanzee:
                    result = new Chimpanzee(age, name, weight, gender);
                    break;

                case AnimalType.Dingo:
                    result = new Dingo(age, name, weight, gender);
                    break;

                case AnimalType.Eagle:
                    result = new Eagle(age, name, weight, gender);
                    break;

                case AnimalType.Hummingbird:
                    result = new Hummingbird(age, name, weight, gender);
                    break;

                case AnimalType.Kangaroo:
                    result = new Kangaroo(age, name, weight, gender);
                    break;

                case AnimalType.Ostrich:
                    result = new Ostrich(age, name, weight, gender);
                    break;

                case AnimalType.Platypus:
                    result = new Platypus(age, name, weight, gender);
                    break;

                case AnimalType.Shark:
                    result = new Shark(age, name, weight, gender);
                    break;

                case AnimalType.Squirrel:
                    result = new Squirrel(age, name, weight, gender);
                    break;
            }

            return result;
        }
    }
}
