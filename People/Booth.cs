using System.Collections.Generic;
using BoothItems;

namespace People
{
    /// <summary>
    /// The class which is used to represent a booth.
    /// </summary>
    public class Booth
    {
        /// <summary>
        /// The employee currently assigned to be the attendant of the booth.
        /// </summary>
        private Employee attendant;

        /// <summary>
        /// The list of items in the booth.
        /// </summary>
        private List<Item> items;

        /// <summary>
        /// Initializes a new instance of the Booth class.
        /// </summary>
        /// <param name="attendant">The employee to be the booth's attendant.</param>
        public Booth(Employee attendant)
        {
            this.attendant = attendant;
            this.items = new List<Item>();
        }

        /// <summary>
        /// Gets the booth's attendant.
        /// </summary>
        public Employee Attendant
        {
            get
            {
                return this.attendant;
            }
        }

        /// <summary>
        /// Gets the list of items in the booth.
        /// </summary>
        public List<Item> Items
        {
            get
            {
                return this.items;
            }
        }
    }
}
