namespace CagedItems
{
    /// <summary>
    /// The interface which is used to represent a cageable item.
    /// </summary>
    public interface ICageable
    {
        /// <summary>
        /// Gets or sets the horizontal position of the item.
        /// </summary>
        double XPosition { get; set; }

        /// <summary>
        /// Gets or sets the vertical position of the item.
        /// </summary>
        double YPosition { get; set; }

        /// <summary>
        /// Gets the display size of the item.
        /// </summary>
        double DisplaySize { get; }

        /// <summary>
        /// Gets the resource key for the item's image.
        /// </summary>
        string ResourceKey { get; }
    }
}
