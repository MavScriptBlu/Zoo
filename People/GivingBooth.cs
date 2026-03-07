using System;
using BoothItems;

namespace People
{
    /// <summary>
    /// The class which is used to represent a booth that gives away free items.
    /// </summary>
    public class GivingBooth : Booth
    {
        /// <summary>
        /// Initializes a new instance of the GivingBooth class.
        /// </summary>
        /// <param name="attendant">The employee to be the booth's attendant.</param>
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            // Create maps (10 total) and coupon books (5 total).
            for (int itemIndex = 0; itemIndex < 10; itemIndex++)
            {
                this.Items.Add(new Map(0.5, DateTime.Now));

                if (itemIndex < 5)
                {
                    this.Items.Add(new CouponBook(DateTime.Now, DateTime.Now.AddYears(1), 0.8));
                }
            }
        }

        /// <summary>
        /// Gives away a free coupon book.
        /// </summary>
        /// <returns>The coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            CouponBook couponBook = null;

            try
            {
                couponBook = this.Attendant.FindItem(this.Items, typeof(CouponBook)) as CouponBook;
            }
            catch (MissingItemException ex)
            {
                throw new MissingItemException("Coupon book could not be found.", ex);
            }

            return couponBook;
        }

        /// <summary>
        /// Gives away a free map.
        /// </summary>
        /// <returns>The map.</returns>
        public Map GiveFreeMap()
        {
            Map map = null;

            try
            {
                map = this.Attendant.FindItem(this.Items, typeof(Map)) as Map;
            }
            catch (MissingItemException ex)
            {
                throw new MissingItemException("Map could not be found.", ex);
            }

            return map;
        }
    }
}
