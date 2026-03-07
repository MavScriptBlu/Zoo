using System.Collections.Generic;
using Animals;
using CagedItems;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a cage.
    /// </summary>
    public class Cage
    {
        /// <summary>
        /// The list of items in the cage.
        /// </summary>
        private List<ICageable> cagedItems;

        /// <summary>
        /// Initializes a new instance of the Cage class.
        /// </summary>
        /// <param name="animalType">The type of animal in the cage.</param>
        /// <param name="width">The width of the cage.</param>
        /// <param name="height">The height of the cage.</param>
        public Cage(AnimalType animalType, int width, int height)
        {
            this.AnimalType = animalType;
            this.Width = width;
            this.Height = height;
            this.cagedItems = new List<ICageable>();
        }

        /// <summary>
        /// Gets the type of animal in the cage.
        /// </summary>
        public AnimalType AnimalType { get; private set; }

        /// <summary>
        /// Gets the width of the cage.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the cage.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the list of caged items.
        /// </summary>
        public IEnumerable<ICageable> CagedItems
        {
            get
            {
                return this.cagedItems;
            }
        }

        /// <summary>
        /// Adds an item to the cage.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(ICageable item)
        {
            this.cagedItems.Add(item);
        }

        /// <summary>
        /// Removes an item from the cage.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove(ICageable item)
        {
            this.cagedItems.Remove(item);
        }
    }
}
