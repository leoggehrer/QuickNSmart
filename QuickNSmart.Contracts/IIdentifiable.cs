//@QnSBaseCode
//MdStart
namespace QuickNSmart.Contracts
{
    /// <summary>
    /// Defines the basic properties of identifiable components.
    /// </summary>
    public partial interface IIdentifiable
    {
        /// <summary>
        /// Gets the identity of the component.
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Gets the row stamp.
        /// </summary>
        byte[] RowVersion { get; }
    }
}
//MdEnd
