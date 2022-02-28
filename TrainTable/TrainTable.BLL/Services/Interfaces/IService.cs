using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrainTable.BLL.Services.Interfaces
{
    /// <summary>
    /// This interface sets a set of methods that the proxy should implement.
    /// </summary>
    /// <typeparam name="T">The type of model that is the object representation of the table.</typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// The method validates and creates an object in the database using the repository.
        /// </summary>
        /// <param name="entity">The object to insert into the database.</param>
        Task<T> Create(T entity);

        /// <summary>
        /// The method reads an object from in the database using the repository.
        /// </summary>
        /// <param name="id">The object id to read from the database.</param>
        /// <returns>The read object from the database.</returns>
        Task<T> ReadById(int id);

        /// <summary>
        /// The method validates and updates an object in the database using the repository.
        /// </summary>
        /// <param name="entity">The object to update in the database.</param>
        Task<T> Update(T entity);

        /// <summary>
        /// The method validates and deletes an object in the database using the repository.
        /// </summary>
        /// <param name="id">The object id to remove from the database.</param>
        Task Delete(int id);

        /// <summary>
        /// The method reads all objects from in the database using the repository.
        /// </summary>
        /// <returns>The list of objects read from the database.</returns>
        IReadOnlyCollection<T> ReadAll();

        IReadOnlyCollection<T> ReadAll(Expression<Func<T, bool>> predicate);
    }
}
