//@QnSBaseCode
//MdStart
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuickNSmart.Contracts.Client
{
    /// <summary>
    /// This interface defines the basic properties and basic operations for accessing the controller.
    /// </summary>
    /// <typeparam name="T">Type, which the basic operations relate.</typeparam>
	public partial interface IControllerAccess<T> : IDisposable
        where T : IIdentifiable
    {
        /// <summary>
        /// Gets the max page size.
        /// </summary>
        int MaxPageSize { get; }
        /// <summary>
        /// Sets the authorization token.
        /// </summary>
        string SessionToken { set; }

        #region Async-Methods
        /// <summary>
        /// Gets the number of quantity in the collection.
        /// </summary>
        /// <returns>Number of entities in the collection.</returns>
        Task<int> CountAsync();
        /// <summary>
        /// Returns the number of quantity in the collection based on a predicate.
        /// </summary>
        /// <param name="predicate">A string to test each element for a condition.</param>
        /// <returns>Number of entities in the collection.</returns>
        Task<int> CountByAsync(string predicate);

        /// <summary>
        /// Returns the element of type T with the identification of id.
        /// </summary>
        /// <param name="id">The identification.</param>
        /// <returns>The element of the type T with the corresponding identification.</returns>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// Gets a subset of items from the repository.
        /// </summary>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>Subset in accordance with the parameters.</returns>
        Task<IQueryable<T>> GetPageListAsync(int pageIndex, int pageSize);
        /// <summary>
        /// Returns all interfaces of the entities in the collection.
        /// </summary>
        /// <returns>All interfaces of the entity collection.</returns>
        Task<IQueryable<T>> GetAllAsync();
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="pageIndex">0 based page index.</param>
        /// <param name="pageSize">The pagesize.</param>
        /// <returns>The filter result.</returns>
        Task<IQueryable<T>> QueryPageListAsync(string predicate, int pageIndex, int pageSize);
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The filter result.</returns>
        Task<IQueryable<T>> QueryAllAsync(string predicate);

        /// <summary>
        /// Creates a new element of type T.
        /// </summary>
        /// <returns>The new element.</returns>
        Task<T> CreateAsync();
        /// <summary>
        /// The entity is being tracked by the context but does not yet exist in the repository. 
        /// </summary>
        /// <param name="entity">The entity which is to be inserted.</param>
        /// <returns>The inserted entity.</returns>
        Task<T> InsertAsync(T entity);
        /// <summary>
        /// The entity is being tracked by the context and exists in the repository, and some or all of its property values have been modified.
        /// </summary>
        /// <param name="entity">The entity which is to be updated.</param>
        /// <returns>The the modified entity.</returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// Removes the entity from the repository with the appropriate identity.
        /// </summary>
        /// <param name="id">The identification.</param>
        Task DeleteAsync(int id);
        /// <summary>
        /// Saves any changes in the underlying persistence.
        /// </summary>
        Task SaveChangesAsync();
        /// <summary>
        /// Reject any changes in the underlying persistence.
        /// </summary>
        Task RejectChangesAsync();

        /// <summary>
        /// Invokes a custom action that represents a controller method.
        /// </summary>
        /// <param name="name">Action name</param>
        /// <param name="parameters">Action parameters</param>
        /// <returns>none</returns>
        Task InvokeActionAsync(string name, params object[] parameters);
        /// <summary>
        /// Invokes a custom function that represents a controller method.
        /// </summary>
        /// <param name="name">Function name</param>
        /// <param name="parameters">Function parameters</param>
        /// <returns>The result of the controller method</returns>
        Task<object> InvokeFunctionAsync(string name, params object[] parameters);
        #endregion Async-Methods
    }
}
//MdEnd
