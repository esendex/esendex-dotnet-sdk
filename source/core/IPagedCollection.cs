namespace com.esendex.sdk.core
{
    /// <summary>
    /// Defines the attributes of a paged result set.
    /// </summary>
    /// <typeparam name="T">The type of the items contained in the result.</typeparam>
    public interface IPagedCollection<T>
    {
        /// <summary>
        /// Gets the currently selected page.
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// Gets the number of items on a page.
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Gets the total number of items in the resource.
        /// </summary>
        int TotalItems { get; set; }
    }
}